namespace CoronaAPI.BL
{
    public class Result<T>
    {
        public ResultsEnum Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
