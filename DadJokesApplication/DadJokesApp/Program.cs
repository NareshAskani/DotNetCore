using DadJokesApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IDadJokesService, DadJokesService>(client =>
{
    var apiKey = builder.Configuration.GetSection("DadJokesService")["APIKey"];
    var host = builder.Configuration.GetSection("DadJokesService")["Host"];

    client.DefaultRequestHeaders.Add("x-rapidapi-host", host);
    client.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
