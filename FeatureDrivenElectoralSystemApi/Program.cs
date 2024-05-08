using System.Reflection;
using BLL;
using BLL.Extension;
using BLL.Interfaces;
using BLL.Services;
using DAL.Data.Extension;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.AddConfigureApplication();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<ICharacteristicService, CharacteristicService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddCors();



//var mapperConfig = new MapperConfiguration(mc =>
//{
//    mc.AddProfile(new AutoMapperProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);
var app = builder.Build();
//app.UseCors(builder => builder.WithOrigins("http://127.0.0.1:4200"));
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");
app.UseDefaultFiles();
app.UseStaticFiles();
app.Run();
