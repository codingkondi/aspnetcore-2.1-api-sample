namespace MyCompany.MyProject.Models
{
    public interface IConfigurationSettings
    {
        EnvironmentSettings EnvironmentSettings { get; set; }
        TokenSettings TokenSettings { get; set; }
    }
}
