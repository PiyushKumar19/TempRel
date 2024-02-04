using Microsoft.EntityFrameworkCore;
using TempRel.Filters;
using TempRel.Filters;
using TempRel.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer("server = MAMTA\\SQLEXPRESS; database = TempRelDb; trusted_connection=true; trustservercertificate = true;"));

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ModifyApiResponseFilter<>>();
//});
builder.Services.AddScoped(typeof(ModifyApiResponseFilter<>));
builder.Services.AddScoped(typeof(ModidyResponseFilter<>));
builder.Services.AddScoped<ITranslationService, TranslationService>();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
