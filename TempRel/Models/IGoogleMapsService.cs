using GoogleApi.Entities.Places.AutoComplete.Request;
using GoogleApi.Entities.Places.AutoComplete.Response;
using GoogleApi.Entities.Places.QueryAutoComplete.Request;
using GoogleApi.Entities.Places.QueryAutoComplete.Response;
using GoogleApi.Entities.Places.Search.Text.Response;
using static TempRel.Models.GoogleMapsModels;

namespace TempRel.Models
{
    public interface IGoogleMapsService
    {
        public Task<GeocodeResponse> GetCoordinates(string address);
        public Task<PlacesTextSearchResponse> SearchTextString(string text);
        public Task<PlacesAutoCompleteResponse> PlaceTextAutoComplete(string incompleteText);
        public Task<PlacesQueryAutoCompleteResponse> PlaceQueryAutoComplete(string incompleteQuery);
    }
}
