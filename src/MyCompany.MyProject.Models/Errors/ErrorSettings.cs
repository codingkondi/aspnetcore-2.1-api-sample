using MyCompany.MyProject.Extensions.NLog;
using MyCompany.MyProject.Extensions.SlackMannager;
using System;
using System.Collections.Generic;

namespace MyCompany.MyProject.Models
{
    public class ErrorSettings
    {
        private readonly Dictionary<int, string> _errorMessages;
        private SlackMannager _slackMannager;
        private readonly NlogLogger _logger;
        private string env;
        public ErrorSettings(Dictionary<int, string> errorMessages, NlogLogger logger, SlackMannager slackMannager, MyProjectConfiguration configuration)
        {
            _errorMessages = errorMessages;
            _logger = logger;
            _slackMannager = slackMannager;
            env = configuration.EnvironmentSettings.CurrentSetting;
        }

        public Error SetError(string key, EnumMasterErrorCode mastererrorcode, EnumSeqMessage seqmessage)
        {
            Error errors = new Error()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)seqmessage).ToString(),
                Message = SetMessage((int)seqmessage).Replace("{Key}", key)
            };
            return errors;
        }
        public Error SetError(string key, EnumMasterErrorCode mastererrorcode, EnumMyProjectCode apicode, EnumSeqMessage seqmessage)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {

            }
            Error errors = new Error()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)apicode).ToString().PadLeft(3, '0') + ((int)seqmessage).ToString(),
                Message = SetMessage((int)seqmessage).Replace("{Key}", key)
            };
            return errors;
        }
        public Error SetError(string key, EnumMasterErrorCode mastererrorcode, EnumMyProjectCode apicode, EnumSeqMessage seqmessage, string Message)
        {
            if (mastererrorcode == EnumMasterErrorCode.Internal_System_Error)
            {

            }
            Error errors = new Error()
            {
                Key = key ?? "",
                Code = ((int)mastererrorcode).ToString() + ((int)apicode).ToString().PadLeft(3, '0') + ((int)seqmessage).ToString(),
                Message = Message
            };
            return errors;
        }
        public Error SetExceptionError(string request, string key)
        {
            _logger.LogException(request, key);

            _slackMannager.SetMessage(SetSlackMessage(request, key), "danger");
            _slackMannager.SendMessage();
            key = string.Empty;
            Error errors = new Error()
            {
                Key = key ?? "",
                Code = ((int)EnumMasterErrorCode.Internal_System_Error).ToString() + ((int)EnumSeqMessage.Exception_Problem).ToString(),
                Message = SetMessage((int)EnumSeqMessage.Exception_Problem)
            };
            return errors;
        }
        private string SetMessage(int seq)
        {
            return _errorMessages[seq];
        }

        private Field[] SetSlackMessage(string request, string key)
        {
            Field[] fields = new Field[]
          {
                new Field(){Title="Environment",Value=env},
                  new Field(){Title="Request",Value=request,Short=true},
                    new Field(){Title="Time",Value=DateTime.UtcNow.ToString(),Short=true},
                      new Field(){Title="Message",Value=key},
          };

            return fields;
        }
    }

}

