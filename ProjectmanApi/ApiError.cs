using ProjectmanApi.Abstraction;

namespace ProjectmanApi;

public class ApiError : ApiReturnedObject
{
    public int ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ApiError(int errCode, string errMessage) : base("errtok")
    {
        ErrorCode = errCode;
        ErrorMessage = errMessage;
    }
}
