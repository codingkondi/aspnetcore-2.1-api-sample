using System.Collections.Generic;

namespace MyCompany.MyProject.Models
{
    public interface IApiResponse<T> where T : class
    {
        int Status { get; set; }
        T Data { get; set; }
        List<Error> Error { get; set; }
        void SetFailedError(Error error);
        void SetExceptionError(Error error);
    }


}
