namespace DSC.Domain;

public record ShellCommand
{
    public int CommandId { get; set; }
    public string? Command { get; set; }
    public InstallProfile? InstallProfile { get; set; }
    public Stage? Stage { get; set; }
}
