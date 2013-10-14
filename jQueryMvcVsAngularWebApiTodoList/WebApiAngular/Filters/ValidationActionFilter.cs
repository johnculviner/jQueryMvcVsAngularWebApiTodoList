using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Linq;

namespace jQueryMvcVsAngularWebApiTodoList.WebApiAngular.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
                actionContext.Response = actionContext.Request
                     .CreateResponse(HttpStatusCode.BadRequest,
                        modelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}