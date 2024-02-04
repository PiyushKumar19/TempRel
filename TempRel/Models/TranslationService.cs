using Google.Api.Gax.ResourceNames;
using Google.Cloud.Translate.V3;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace TempRel.Models
{
    public class TranslationService : ITranslationService
    {
        public async Task<string> ReturnTranslation(string jso)
        {
            var serializedJson = JsonConvert.SerializeObject(jso);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
                {
                    { "X-RapidAPI-Key", "2dd252b690msh02087544b03a6bfp1d0596jsna1deefdec93f" },
                    { "X-RapidAPI-Host", "google-translate1.p.rapidapi.com" },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", $"{serializedJson}" },
                    { "target", "ar" },
                    { "source", "en" },
                }),
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                string decodedBody = body.Replace("&quot;", "\"");
                string jsonString = decodedBody.Replace("\"\"", "\"").Replace("\\\\" ,"\\");
                return jsonString;
            }
        }

        public async Task<string> GoogleTranslation(string str)
        {
            
            TranslationServiceClient client = TranslationServiceClient.Create();
            TranslateTextRequest request = new TranslateTextRequest
            {
                Contents = { str },
                TargetLanguageCode = "ar",
                Parent = new ProjectName("").ToString()
            };
            TranslateTextResponse response = client.TranslateText(request);
            // response.Translations will have one entry, because request.Contents has one entry.
            Translation translation = response.Translations[0];
            var serialisedString = JsonConvert.SerializeObject(translation);

            return serialisedString;
        }

        public async Task<string> CreateApiKeys()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://translated-mymemory---translation-memory.p.rapidapi.com/createkey"),
                Headers =
                {
                    { "X-RapidAPI-Key", "2dd252b690msh02087544b03a6bfp1d0596jsna1deefdec93f" },
                    { "X-RapidAPI-Host", "translated-mymemory---translation-memory.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }
    }
}
