using ScratchPayRepository.IRepository;
using ScratchPayRepository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IClinicsRepository, ClinicRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//We can enable this if we add azure ad authentication
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
