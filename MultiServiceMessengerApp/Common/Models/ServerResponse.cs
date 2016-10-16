namespace Common.Models
{
    // This object is returned by both the WCF and Web API services
    public class ServerResponse<T>
    {
        public bool Success { get; set; }
        public string ErrorDetails { get; set; }
        public T Content { get; set; }
    }

    // Default the generic type to an object
    public class ServerResponse : ServerResponse<object>
    {
    }
}
