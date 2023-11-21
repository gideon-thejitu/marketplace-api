// Core
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;

// Service
using marketplace_api.Services.Auth;
using marketplace_api.Services.CategoryService;
using marketplace_api.Services.Pagination;
using marketplace_api.Services.ProductService;
using marketplace_api.Services.RegistrationsService;
using marketplace_api.Services.NotificationService;

// Data
using marketplace_api.Data;

// APM
using Elastic.Apm.NetCoreAll;


// Hangfire
using Hangfire;
using Hangfire.SqlServer;
using marketplace_api.Services.UsersService;

var developmentOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

var hangfireStorageOptions = new SqlServerStorageOptions()
{
    TryAutoDetectSchemaDependentOptions = true,
    PrepareSchemaIfNecessary = true
};

builder.Services.AddHangfire(options => options.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"), hangfireStorageOptions));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: developmentOrigins, policy =>
    {
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddRouting(opts => opts.LowercaseUrls = true);
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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

builder.Services.AddHangfireServer();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductStatusService, ProductStatusService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRegistrationService, RegistrationsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseAllElasticApm(builder.Configuration);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(developmentOrigins);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<NotificationService>(service => service.Job(), "0/2 * * * *");

app.Run();
