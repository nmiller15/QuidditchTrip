namespace QuidditchTrip.Models;

public class ServiceResponse<T> where T : new()
{
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public bool WasSuccessful { get; set; } = true;

    public static ServiceResponse<T> Success(T data)
    {
        return new ServiceResponse<T>
        {
            Data = data,
            WasSuccessful = true
        };
    }

    public static ServiceResponse<T> Failure(string errorMessage)
    {
        return new ServiceResponse<T>
        {
            Data = new T(),
            ErrorMessage = errorMessage,
            WasSuccessful = false
        };
    }
}
