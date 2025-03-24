namespace Online_Shop_Final_Project_ITStep.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool success { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}
