using Microsoft.AspNetCore.HttpOverrides;

public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    services.AddCors(
#if DEBUG
      options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin())
#endif
    ).AddSignalR();
  }

  public void Configure(IApplicationBuilder app)
  {
#if DEBUG
    app.UseCors();
#endif
    app
    .UseRouting()
    .UseForwardedHeaders(new ForwardedHeadersOptions
    {
      ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    }).UseEndpoints(endpoints => endpoints.MapHub<SignalRHub>("/share"));
  }
}
