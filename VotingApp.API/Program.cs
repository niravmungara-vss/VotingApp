using Microsoft.EntityFrameworkCore;
using VotingApp.API.Helpers;
using VotingApp.API.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    //options.JsonSerializerOptions.Converters.Add(new DecimalFormatConverter());
});

var appConfig = builder.Configuration.Get<AppConfig>();

builder.Services.AddDbContext<DBAccess>(options => options.UseSqlServer(appConfig.ConnectionStrings.SQLdb));
builder.Services.AddTransient<IVoteCountRepository, VoteCountRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true)
                    .AllowAnyOrigin()
                    .Build());

app.MapControllers();

app.Run();


