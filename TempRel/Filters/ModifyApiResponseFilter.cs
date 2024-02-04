using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using TempRel.Models;

namespace TempRel.Filters
{

    public class ModifyApiResponseFilter<T> : IResultFilter
    {
        private readonly ITranslationService translationService;

        public ModifyApiResponseFilter(ITranslationService translationService)
        {
            this.translationService = translationService;
        }
        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    // Executed before the action method
        //}

        public async void OnResultExecuting(ResultExecutingContext context)
        {
            // Executed after the action method
            //if (context.Result is ObjectResult objectResult)
            //{
            //    var temp = objectResult.Value;
            //    // Modify the response here
            //    if (objectResult.Value != null && objectResult.Value is T originalObject)
            //    {
            //        // Serialize the original object to a JSON string
            //        var jsonOriginal = JsonConvert.SerializeObject(originalObject);

            //        // Deserialize the JSON string to a JObject for manipulation
            //        JObject jsonObject = JObject.Parse(jsonOriginal);

            //        // Add or modify properties in the JObject
            //        jsonObject["language"] = "English";

            //        // Serialize the modified JObject back to a JSON string
            //        var jsonModified = jsonObject.ToString();

            //        // Deserialize the modified JSON string back to the original type T
            //        var modifiedObject = JsonConvert.DeserializeObject<T>(jsonModified);

            //        objectResult.Value = modifiedObject;
            //    }
            //}
            var language = context.HttpContext.Request.Headers["language"];

            if (language == "ar" && context.Result is ObjectResult objectResult)
            {
                var temp = objectResult.Value;

                if (temp != null && temp is T originalObject)
                {
                    // Serialize the original object to a JSON string
                    var jsonOriginal = JsonConvert.SerializeObject(originalObject);

                    // Deserialize the JSON string to a JObject for manipulation
                    JObject responseObject = JObject.Parse(jsonOriginal);
                    //string arabicString = "{\"المعرف\":1،\"الاسم\": \"سلسلة\"}".Replace("،", ",");
                    string translatedString = await translationService.GoogleTranslation(jsonOriginal);
                    //string translatedString = "{\"data\":{\"translations\":[{\"translatedText\":\"{\\\\\"str\\\\\":\\\\\"مرحبا\\\\\"}\"}]}}".Replace("\\\\", "\\");
                    JObject translatedObj = JObject.Parse(translatedString);

                    string arabicString = (string)translatedObj["data"]["translations"][0]["translatedText"];
                    JObject arabicObj = JObject.Parse(arabicString);

                    //string testString2 = "{\"المعرف\":0،\"العمر\":0}".Replace("،", ",");
                    //string modifiedArabicObj = WebUtility.HtmlDecode(arabicObj);

                    var newJObject = new JObject();
                    // Collect all property values
                    //foreach (var responseProperty in responseObject.Properties())
                    //{
                    //    foreach (var arabicResponseProperty in arabicObj.Properties())
                    //    {
                    //        newJObject.Add(responseProperty.Name, arabicResponseProperty.Value);
                    //        //var propertyName = property.Name;
                    //        //var propertyValue = property.Value;

                    //        // Process or print the property name and value as needed
                    //        Console.WriteLine($"{responseProperty.Name}: {arabicResponseProperty.Value}");


                    //    }
                    //}

                    // Get the properties from both JSON objects
                    var responseProperties = responseObject.Properties();
                    var arabicProperties = arabicObj.Properties();

                    // Zip the properties together
                    var zippedProperties = responseProperties.Zip(arabicProperties, (response, arabic) => new { Response = response, Arabic = arabic });

                    // Iterate through the zipped properties
                    foreach (var zippedProperty in zippedProperties)
                    {
                        var responseProperty = zippedProperty.Response;
                        var arabicResponseProperty = zippedProperty.Arabic;

                        // Check if the corresponding property in arabicObj exists
                        if (arabicResponseProperty != null)
                        {
                            // Add the property to the new JObject
                            newJObject.Add(responseProperty.Name, arabicResponseProperty.Value);
                        }
                        else
                        {
                            // If no corresponding property is found, add the original property
                            newJObject.Add(responseProperty.Name, responseProperty.Value);
                        }
                    }

                    Console.WriteLine(newJObject);
                    // Optionally, convert the modified JObject back to the original type T
                    var modifiedObject = newJObject.ToObject<T>();

                    objectResult.Value = modifiedObject;
                    Console.WriteLine(objectResult.Value);

                    // Update the context.Result to reflect the modified object
                    context.Result = new ObjectResult(objectResult.Value)
                    {
                        StatusCode = objectResult.StatusCode,
                        // You can copy other properties from objectResult if needed
                    };
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Optional: Implement this method if needed
        }
    }
}

