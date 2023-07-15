using Metrik.Services.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.LoadMyServices(config);
builder.Services.AddControllers(opt => opt.Filters.Add(new AuthorizeFilter()));
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
