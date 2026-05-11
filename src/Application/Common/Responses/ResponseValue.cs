namespace Application.Common.Responses;
public enum ResponseStatus
{
    Success,
    Error,
    NotFound,
    BadRequest,
    Conflict,
    Unauthorized
}

public class ResponseValue<T>
{
    public T? Content { get; set; }
    public ResponseStatus Status { get; set; }
    public string? Message { get; set; }

    public ResponseValue(T? content, ResponseStatus status, string? message)
    {
        Content = content;
        Status = status;
        Message = message;
    }

    public static ResponseValue<T> Success(T? content, string? message = null)
        => new(content, ResponseStatus.Success, message);

    public static ResponseValue<T> Error(string? message = null)
        => new(default, ResponseStatus.Error, message);

    public static ResponseValue<T> NotFound(string? message = null)
        => new(default, ResponseStatus.NotFound, message);

    public static ResponseValue<T> BadRequest(string? message = null)
        => new(default, ResponseStatus.BadRequest, message);
    public static ResponseValue<T> Conflict(string? message = null)
        => new(default, ResponseStatus.Conflict, message);
    public static ResponseValue<T> Unauthorized(string? message = null)
        => new(default, ResponseStatus.Unauthorized, message);
}

