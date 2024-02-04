using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TempRel.Filters
{
    public class ModidyResponseFilter<T> : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var temp = objectResult.Value;

                Console.WriteLine(temp);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context.Result is ObjectResult objectResult)
            {
                var temp = objectResult.Value;

                if (temp != null && temp is T originalObject)
                {
                    var jsonOriginal = JsonConvert.SerializeObject(originalObject);

                    JObject jsonObject = JObject.Parse(jsonOriginal);

                    jsonObject["Fruit"] = "Apples";

                    var modifiedValue = jsonObject.GetValue("Fruit");

                    var modifiedObject = jsonObject.ToObject<T>();

                    objectResult.Value = modifiedObject;
                }
            }
        }
    }
}
