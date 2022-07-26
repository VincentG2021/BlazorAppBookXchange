using BlazorAppBookXchange;
using BlazorAppBookXchange.Services;
using BlazorAppBookXchange.Tools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7144/BookXchangeAPI/") });
builder.Services.AddScoped(sp => new ApiRequester("https://localhost:7144/BookXchangeAPI/"));
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<AccountManager>();
builder.Services.AddScoped<XchangeService>();

await builder.Build().RunAsync();
