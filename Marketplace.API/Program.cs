// Core
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

// Service
using Marketplace.Services.AuthService;
using Marketplace.Services.CategoryService;
using Marketplace.Services.Pagination;
using Marketplace.Services.ProductService;
using Marketplace.Services.RegistrationService;
using Marketplace.Services.NotificationService;

// APM
using Elastic.Apm.NetCoreAll;

// Hangfire
using Hangfire;
using Hangfire.SqlServer;

using Marketplace.Infrastructure.Authorization;
using Marketplace.Extensions;
using Marketplace.Infrastructure.Cerbos;
using Marketplace.Infrastructure.ConfigurationOptions;
using Marketplace.Services;
using Marketplace.Services.UserService;
using Marketplace.Infrastructure.Data;
using Marketplace.Services.AuthServiceService;

// logging
using Serilog;

var developmentOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var hangfireStorageOptions = new SqlServerStorageOptions()
{
    TryAutoDetectSchemaDependentOptions = true,
    PrepareSchemaIfNecessary = true
};

var conn = builder.Configuration.GetConnectionString("HangfireConnection");
Console.WriteLine($"=======================================");
Console.WriteLine($"Connn = {conn}");
Console.WriteLine($"=======================================");

// builder.Services.AddHangfire(options => options.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
//     .UseSimpleAssemblyNameTypeSerializer()
//     .UseRecommendedSerializerSettings()
//     .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), hangfireStorageOptions));

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Authorization Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: developmentOrigins, policy =>
    {
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddRouting(opts => opts.LowercaseUrls = true);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction =>
        {
            sqlServerOptionsAction.MigrationsAssembly("Marketplace.Infrastructure");
            sqlServerOptionsAction.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null);
        }));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true
    };
});

builder.Services.AddHttpContextAccessor();
// builder.Services.AddHangfireServer();
builder.Services.AddAuthorization();

builder.Services.AddSingleton<ICerbosProvider, CerbosProvider>();
builder.Services.AddScoped<ICerbosHandler, CerbosHandler>();
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductStatusService, ProductStatusService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRegistrationService, RegistrationsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRefreshTokenCleanupService, RefreshTokenCleanupService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRequestLogService, RequestLogService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

// IOptions
builder.Services.Configure<CerbosOptions>(
    builder.Configuration.GetSection(CerbosOptions.Name));

var app = builder.Build();

app.UseForwardedHeaders();

if (app.Environment.IsProduction())
{
    app.UseAllElasticApm(builder.Configuration);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(developmentOrigins);
}
else
{
    app.ConfigureExceptionHandler();
}

app.UseSerilogRequestLogging();

// app.UseMarketplaceRequestResponseLogger();


app.UseHttpsRedirection();

// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<DataContext>();
//     db.Database.GetPendingMigrations();
//     db.Database.Migrate();
// }

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var db = serviceScope.ServiceProvider.GetRequiredService<DataContext>().Database;

    logger.LogInformation("Migrating database...");

    try
    {
        var can = db.CanConnect();
        logger.LogInformation(can ? "aaaa" : "eeeee");
    }
    catch (Exception e)
    {
        logger.LogError(e.Message);
        logger.LogError(e.ToString());
        throw;
    }

    while (!db.CanConnect())
    {
        logger.LogInformation("Database not ready yet; waiting...");
        Thread.Sleep(1000);
        db = serviceScope.ServiceProvider.GetRequiredService<DataContext>().Database;
    }

    try
    {
        serviceScope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// app.UseHangfireDashboard();

// RecurringJob.AddOrUpdate<NotificationService>(service => service.Job(), "0/2 * * * *");
// RecurringJob.AddOrUpdate<RefreshTokenCleanupService>(service => service.Execute(), "0/1 * * * *");

app.Run();
