﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace FoodCourt
{
    public class ApiJsonResult : ActionResult
    {
        private HttpStatusCode _statusCode;
        private object _result;
        public ApiJsonResult(object result, HttpStatusCode code)
        {
            _result = result;
            _statusCode = code;
        }
        public ApiJsonResult(HttpStatusCode code)
        {
            _statusCode = code;
        }
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var httpContext = context.HttpContext;
            var request = httpContext.Request;
            var response = httpContext.Response;

            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = (int)_statusCode;

            // Dùng Json Serialize mặc định để Json cho body
            var writerFactory = httpContext.RequestServices.GetRequiredService<IHttpResponseStreamWriterFactory>();
            var options = httpContext.RequestServices.GetRequiredService<IOptions<MvcNewtonsoftJsonOptions>>().Value;
            var serializerSettings = options.SerializerSettings;

            await using (var writer = writerFactory.CreateWriter(response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.CloseOutput = false;
                    var jsonSerializer = JsonSerializer.Create(serializerSettings);
                    jsonSerializer.Serialize(jsonWriter, _result);
                }
            }
        }
    }
}
