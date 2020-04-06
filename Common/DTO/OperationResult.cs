namespace DTO
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; } = false;
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set;}
    }
}
