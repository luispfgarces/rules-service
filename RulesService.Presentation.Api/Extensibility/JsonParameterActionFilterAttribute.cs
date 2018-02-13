using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace RulesService.Presentation.Api.Extensibility
{
    public class JsonParameterActionFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                IEnumerable<ParameterInfo> parametersInfo = controllerActionDescriptor.MethodInfo.GetParameters();

                foreach (ParameterInfo parameterInfo in parametersInfo)
                {
                    if (parameterInfo.GetCustomAttributes<JsonParameterAttribute>().Any())
                    {
                        KeyValuePair<string, StringValues> keyValuePair = context.HttpContext.Request.Query.Single(kvp => string.Equals(kvp.Key, parameterInfo.Name));
                        object deserializedObject = JsonConvert.DeserializeObject(keyValuePair.Value, parameterInfo.ParameterType);
                        context.ActionArguments[parameterInfo.Name] = deserializedObject;
                    }
                }
            }
        }
    }
}