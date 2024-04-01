namespace Application.Dto
{
    public class ResultRequestDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
