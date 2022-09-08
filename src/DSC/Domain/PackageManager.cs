namespace DSC.Domain;

public record PackageManager(string Name, string FileName)
{
    public string Name { get; set; } = Name;

    public string FileName { get; set; } = FileName;

    public string? InstallCommand { get; set; }
}
