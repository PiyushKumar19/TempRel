using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Response;
using GoogleApi.Entities.Places.QueryAutoComplete.Request;
using GoogleApi.Entities.Places.QueryAutoComplete.Response;
using GoogleApi.Entities.Places.Search.Text.Request;
using GoogleApi.Entities.Places.Search.Text.Response;
using Newtonsoft.Json;
using static TempRel.Models.GoogleMapsModels;

namespace TempRel.Models
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly GooglePlaces googlePlaces;

        public GoogleMapsService(GooglePlaces googlePlaces)
        {
            this.googlePlaces = googlePlaces;
        }
        public async Task<GeocodeResponse> GetCoordinates(string address)
        {
            string apiKey = "AIzaSyCvM2BPh4a3eJOO-VOPGAIp2XvVI6yoY2E"; // Replace with your actual key
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<GeocodeResponse>(json);
                }
                else
                {
                    // Handle error
                    throw new Exception("Failed to get coordinates");
                }
            }
        }

        public async Task<PlacesTextSearchResponse> SearchTextString(string text)
        {
            PlacesTextSearchRequest request = new PlacesTextSearchRequest
            {
                Key = "AIzaSyCvM2BPh4a3eJOO-VOPGAIp2XvVI6yoY2E", // Replace with your actual API key
                Query = text,
            };

            var httpClient = new HttpClient();
            var gService = new GooglePlaces.Search.TextSearchApi();
            PlacesTextSearchResponse response = await gService.QueryAsync(request);
            if(response.Status == Status.Ok)
            {
                return response;
            }
            return new PlacesTextSearchResponse();
        }

        public async Task<PlacesAutoCompleteResponse> PlaceTextAutoComplete(string incompleteText)
        {
            PlacesAutoCompleteRequest request = new PlacesAutoCompleteRequest
            {
                Key = "AIzaSyCvM2BPh4a3eJOO-VOPGAIp2XvVI6yoY2E",
                Input = incompleteText,
            };

            var mapService = new GooglePlaces.AutoCompleteApi();
            PlacesAutoCompleteResponse response = await mapService.QueryAsync(request);

            if (response.Status is Status.Ok)
            {
                return response;
            }
            return new PlacesAutoCompleteResponse();
        }

        public async Task<PlacesQueryAutoCompleteResponse> PlaceQueryAutoComplete(string incompleteQuery)
        {
            PlacesQueryAutoCompleteRequest request = new PlacesQueryAutoCompleteRequest
            {
                Key = "AIzaSyCvM2BPh4a3eJOO-VOPGAIp2XvVI6yoY2E",
                Input = incompleteQuery,
            };

            var gService = new GooglePlaces.QueryAutoCompleteApi();
            PlacesQueryAutoCompleteResponse response = await gService.QueryAsync(request);

            if (response.Status is Status.Ok)
            {
                return response;
            }
            return new PlacesQueryAutoCompleteResponse();
        }
    }
}
