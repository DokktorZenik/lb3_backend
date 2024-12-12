using _3_laba.src.controllers;
using _3_laba.src.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCors();


// Добавляем поддержку контроллеров.
builder.Services.AddControllers();

// Регистрируем сервис.
builder.Services.AddTransient<MainService>();

var app = builder.Build();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Добавляем маппинг контроллеров.
app.MapControllers();

app.MapRazorPages();

app.Run();
