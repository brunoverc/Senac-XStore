namespace XStore.Core.Model
{
    public class Response
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public ICollection<string> Message { get; set; } = new List<string>();
    }
}
