using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Teched2018.Dema
{
    public class MyOkResult : ObjectResult
    {
        public MyOkResult(object value) : base(value)
        {
            this.StatusCode = StatusCodes.Status202Accepted;

            var result = new
            {
                ReqId = Guid.NewGuid(),
                Result = value
            };

            this.Value = result;
        }
    }

    [ApiController]
    public class ApiController : ControllerBase
    {
        [NonAction]
        public virtual MyOkResult MyOk(object value)
        {
            return new MyOkResult(value);
        }
    }
}
