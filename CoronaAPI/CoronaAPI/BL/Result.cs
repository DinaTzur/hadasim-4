namespace CoronaAPI.BL
{
    //Used to pass information or error from BL to api
    public class Result<T>
    {
        public ResultsEnum Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
