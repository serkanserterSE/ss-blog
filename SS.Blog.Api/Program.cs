using Microsoft.EntityFrameworkCore;
using SS.Blog.Cache.Abstractions;
using SS.Blog.Cache.Concretes;
using SS.Blog.DataAccesses.Contexts;
using SS.Blog.Middleware.Logging;
using SS.Blog.Models.Settings;
using SS.Blog.Queue.Abstractions;
using SS.Blog.Queue.Concretes;
using SS.Blog.Services.Abstractions;
using SS.Blog.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddDbContext<BlogDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString")); });
builder.Services.AddDbContext<LogDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnectionString")); });

builder.Services.AddScoped<ICategoryOperations, CategoryOperations>();
builder.Services.AddScoped<IProducer, RabbitMqProducer>();
builder.Services.AddScoped<IConsumer, RabbitMqConsumer>();
builder.Services.AddScoped<IBlogLoging, BlogLoging>();
builder.Services.AddScoped<ICache, RedisCache>();

builder.Services.AddStackExchangeRedisCache(p => { p.Configuration = builder.Configuration["RedisConnectionString"]; });

var app = builder.Build();

app.UseBlogAppLoging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) => {
    context.Request.EnableBuffering();
    await next();
});

app.Run();
