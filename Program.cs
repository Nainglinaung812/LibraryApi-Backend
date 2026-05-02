
using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// OpenAPI အစား Swagger သုံးဖို့ ဒါလေးတွေ ထည့်ပါ
builder.Services.AddEndpointsApiExplorer(); // [New]
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   // [New]
    app.UseSwaggerUI();  // [New]
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
