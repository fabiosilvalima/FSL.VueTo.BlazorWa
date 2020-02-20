using FSL.Framework.Core.Models;

namespace FSL.Framework.Core.Extensions
{
    public static class GenericExtension
    {
        public static BaseResult<T> ToResult<T>(
            this T data)
        {
            var result = new BaseResult<T>();

            if (data == null)
            {
                result.Message = "data está nulo";
            }

            result.Data = data;
            result.Success = true;

            return result;
        }
    }
}
