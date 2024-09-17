namespace SubuProtokol.WEBUI.Models
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string[] messages { get; set; }
        public T data { get; set; }
      
    }

}
