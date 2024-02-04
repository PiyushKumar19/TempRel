namespace TempRel.Models
{
    public interface ITranslationService
    {
        public Task<string> CreateApiKeys();
        public Task<string> ReturnTranslation(string jso);
        public Task<string> GoogleTranslation(string str);
    }
}
