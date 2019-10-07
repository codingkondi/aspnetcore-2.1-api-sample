using System.Collections.Generic;

namespace MyCompany.MyProject.Models
{
    public class CallApiRequest
    {
        public string Url { get; set; }
        public EnumHttpMethod ApiMethod { get; set; }
        public object Request { get; set; }
        public Dictionary<string,string> Params { get; set; }
    }
}
