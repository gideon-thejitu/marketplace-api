namespace Marketplace.Infrastructure.ConfigurationOptions;

public class CerbosOptions : ICerbosOptions
{
    public const string Name = "Cerbos";

    public string PolicyVersion { get; set; } = String.Empty;
}
