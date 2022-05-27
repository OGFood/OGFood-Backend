using DbAccess.Database;
using DbAccess.Interfaces;
using OGFoodAPI.DbService;
using OGFoodAPI.RecipeService;
using OGFoodAPI.RecipeService.Strategies;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("*");
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cnnString = builder.Configuration.GetConnectionString("CUSTOMCONNSTR_ttmongodb");
if (cnnString == "test") cnnString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_ttmongodb");
        
if (cnnString != "test" && !String.IsNullOrEmpty(cnnString))
{
    builder.Services.AddSingleton<MongoDbAccess>(new MongoDbAccess(cnnString));
}
else
{
    //builder.Services.AddSingleton<IConnectionStringHelper>(instance => DbAccess.Helpers.ConnectionStringHelper.Instance);
    builder.Services.AddSingleton<MongoDbAccess>(new MongoDbAccess(DbAccess.Factory.GetConnectionStringHelper()));
}
builder.Services.AddSingleton<IIngredientCrud, MongoIngredientCrud>();
builder.Services.AddSingleton<IRecipeCrud, MongoRecipeCrud>();

//var csh = new ConnectionStringHelper();
//builder.Services.AddSingleton<MongoDbContext>(new MongoDbContext(csh.ConnectionString));
//builder.Services.AddSingleton<IRecipeContext>(new DbStorage(new MongoDbContext(csh.ConnectionString)));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "My API - V1",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "OGFoodAPI.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
