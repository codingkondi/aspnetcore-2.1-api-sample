using Microsoft.Extensions.Logging;


namespace MyCompany.MyProject.Extensions.NLog
{
    public class NlogLogger
    {
        private readonly ILogger _logger;

        public NlogLogger(ILogger<NlogLogger> logger)
        {
            _logger = logger;
        }

        public void LogException(string request, string key)
        {
            _logger.LogError(request + ":" + key);
        }
    }
}
