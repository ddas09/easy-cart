using EasyCart.Shared.Constants;

namespace EasyCart.Shared.Exceptions;

public class ApiException : Exception
{
    public AppConstants.ErrorCodeEnum ErrorCode { get; set; }

    public ApiException(string message, AppConstants.ErrorCodeEnum errorCode) 
        : base(message) 
    {
        this.ErrorCode = errorCode;
    }
}
