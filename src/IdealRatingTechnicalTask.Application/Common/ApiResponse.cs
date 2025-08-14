namespace IdealRatingTechnicalTask.Application.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }

        public ApiResponse(T results)
        {
            Result = results;
            IsSuccess = true;
        }

        public ApiResponse(string error)
        {
            Error = error;
            IsSuccess = false;
        }
    }
}
