using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MediaCloud.Web {

  public class Program {

    public static void Main(string[] args) {

      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
          .UseKestrel()
          .UseStartup<Startup>()
          .UseUrls("http://localhost:5000/")
          .Build();
  }
}
