namespace MyCompany.MyProject.Models
{
    public class ApiRequest<T> where T : class
    {
        public T Data { get; set; }
    }
}
