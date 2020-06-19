using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using FoodCourt.Strings;
using Microsoft.AspNetCore.Mvc;

namespace FoodCourt
{
    /// <summary>
    /// Class hỗ trợ các Controller trả về kết quả nhanh gọn hơn
    /// </summary>
    public static class ApiResultExtensions
    {
        public static IActionResult ErrorResult(this ControllerBase controller, int error, string errorMessage, HttpStatusCode statusCode)
        {
            return JsonResult(new ApiResponse<object>(error, errorMessage), statusCode);
        }
        public static IActionResult ErrorResult(this ControllerBase controller, int error, string errorMessage)
        {
            return JsonResult(new ApiResponse<object>(error, errorMessage), HttpStatusCode.BadRequest);
        }

        public static IActionResult ErrorResult(this ControllerBase controller, ErrorCode error)
        {
            return JsonResult(new ApiResponse<object>(
                (int)error, 
                ErrorResources.ResourceManager.GetString(error.ToString())), 
                HttpStatusCode.BadRequest);
        }

        public static IActionResult ErrorResult(this ControllerBase controller, ErrorCode error, HttpStatusCode statusCode)
        {
            return JsonResult(new ApiResponse<object>(
                (int)error,
                ErrorResources.ResourceManager.GetString(error.ToString())),
                statusCode);
        }

        public static IActionResult OkResult<T>(this ControllerBase controller, T result)
        {
            return JsonResult(new ApiResponse<T>(result));
        }
        public static IActionResult OkResult(this ControllerBase controller, object result)
        {
            return JsonResult(new ApiResponse<object>(result));
        }

        public static IActionResult OkResult(this ControllerBase controller)
        {
            return JsonResult(new ApiResponse<object>(true));
        }
        private static IActionResult JsonResult(object result, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ApiJsonResult(result, status);
        }
    }
}
