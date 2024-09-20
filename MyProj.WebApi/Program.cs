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
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
