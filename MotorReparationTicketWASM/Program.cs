using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MotorReparationTicketWASM;
using MotorReparationTicketWASM.Service;
using MotorReparationTicketWASM.Service.IService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });
builder.Services.AddScoped<ITicketHttpService, TicketHttpService>();

await builder.Build().RunAsync();
