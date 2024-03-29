using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
namespace ASP_Lr11.Filters
{
    public class ActionLoggingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var methodName = context.ActionDescriptor.DisplayName;
            var currentTime = DateTime.Now.ToString();
            File.AppendAllText("ActionLog.txt", $"{methodName} called at {currentTime}\n");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}