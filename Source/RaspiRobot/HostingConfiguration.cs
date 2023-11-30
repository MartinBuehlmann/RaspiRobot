namespace RaspiRobot;

internal class HostingConfiguration
{
    public HostingConfiguration(
        bool useHttps,
        int port,
        string? certificateFile,
        string? certificatePassword)
    {
        this.UseHttps = useHttps;
        this.Port = port;
        this.CertificateFile = certificateFile;
        this.CertificatePassword = certificatePassword;
    }

    public bool UseHttps { get; }

    public int Port { get; }

    public string? CertificateFile { get; }

    public string? CertificatePassword { get; }
}