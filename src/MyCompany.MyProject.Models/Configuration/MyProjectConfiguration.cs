namespace MyCompany.MyProject.Models
{
    public class MyProjectConfiguration : IConfigurationSettings
    {
        public EnvironmentSettings EnvironmentSettings { get; set; }
        public TokenSettings TokenSettings { get; set; }
    }
}
