using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCourt
{
    public class ApiResponse<T>
    {
        #region Properties
        public bool Successful { get; set; }

        public T Result { get; set; }

        public int? ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public ApiResponse(bool successful)
        {
            Successful = successful;
        }

        public ApiResponse(T result)
        {
            Successful = true;
            Result = result;
        }

        public ApiResponse(int errorCode, string errorMessage)
        {
            Successful = false; //Check
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        #endregion

    }
}
