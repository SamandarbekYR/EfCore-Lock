using Microsoft.AspNetCore.RateLimiting;
using MyProj.WebApi.Configurations;
using MyProj.WebApi.Interfaces;
using MyProj.WebApi.Mapper;
using MyProj.WebApi.Repository;
using MyProj.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductServcie>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddRateLimiter(x =>
{
    x.RejectionStatusCode = 429;
    x.AddSlidingWindowLimiter("sliding", options =>
    {
        options.Window = TimeSpan.FromSeconds(1);
        options.SegmentsPerWindow = 3;
        options.PermitLimit = 5;
        options.QueueLimit = 3;
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("sliding"); 

app.Run();
