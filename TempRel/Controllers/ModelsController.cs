using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TempRel.Filters;
using TempRel.Models;

namespace TempRel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(ModifyApiResponseFilter<TestModel>))]
    public class ModelsController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly ITranslationService translationService;
        public static List<TestModel> testModels = new List<TestModel>();
        public static List<TestModel2> testModels2 = new List<TestModel2>();

        public ModelsController(AppDbContext _dbContext, ITranslationService translationService)
        {
            dbContext = _dbContext;
            this.translationService = translationService;
        }
        [HttpPost]
        public IActionResult CreateModel1(Model1 model)
        {
            dbContext.Model1s.Add(model);
            dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("Create Temp Model")]
        public async Task<IActionResult> CreateTempModel(TempModel model)
        {
            await dbContext.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(int id, string token)
        {
            var rok = await dbContext.TempModel.FirstOrDefaultAsync(e => e.Id == id);
            var exist = rok.TokenList.FirstOrDefault(e=>e.Equals(token));
            if (rok.TokenList.Contains(token))
            {
                return Ok($"It exists:  {exist}");
            }
            return NotFound();
        }

        [HttpPost("Create Test Model")]
        public async Task<IActionResult> GetTestName(TestModel model)
        {
            testModels.Add(model);
            return Ok(model);
        }

        //[TypeFilter(typeof(ModifyApiResponseFilter<TestModel2>))]
        [HttpPost("Create Test Model 2")]
        public async Task<IActionResult> GetTestAge(TestModel2 model)
        {
            testModels2.Add(model);
            return Ok(model);
        }

        [TypeFilter(typeof(ModidyResponseFilter<TestModel>))]
        [HttpPost("Create new fruit")]
        public async Task<IActionResult> CreateNewFruit(TestModel model)
        {
            testModels.Add(model);
            return Ok(model);
        }

        [HttpGet("Create Keys")]
        public async Task<IActionResult> CreateKeys()
        {
            var result = await translationService.CreateApiKeys();
            return Ok(result);
        }

        [TypeFilter(typeof(ModifyApiResponseFilter<TranslationTemp>))]
        [HttpPost("Some translation")]
        public async Task<IActionResult> GetTranslate( TranslationTemp jso, [FromHeader] string language)
        {
            if (language == "ar")
            {
                //var translation = await translationService.ReturnTranslation(jso);
                return Ok(jso);
            }
            return Ok("Cannot convert text");
        }

        [HttpGet("TranslateString")]
        public async Task<IActionResult> TranslateText(string toTranslate, [FromHeader] string header)
        {
            var translatedText = await translationService.ReturnTranslation(toTranslate);
            return Ok(translatedText);
        }

        [HttpPost("TestFormData")]
        public async Task<IActionResult> PostForm([FromForm] Temp model)
        {
            Console.WriteLine(model);
            return Ok(model);
        }
    }
}
