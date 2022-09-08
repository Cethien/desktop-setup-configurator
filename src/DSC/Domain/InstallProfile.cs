namespace DSC.Domain;

public record InstallProfile
{
    public int InstallProfileId { get; set; }
    public string? Name { get; set; }
    public bool IsDefault { get; set; }
}