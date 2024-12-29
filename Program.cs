using Habbit_Api.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = "mongodb+srv://user:mPvN3s4bQsoSrHCm@habbitcluster.koshg.mongodb.net/?retryWrites=true&w=majority&appName=HabbitCluster"; // Заміни на свій рядок підключення
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
