using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AzureKVSample.Core;

namespace AzureKVSample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost().Run();
		}

		public static IWebHost BuildWebHost() =>
			new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>()
				.UseShutdownTimeout(TimeSpan.FromSeconds(15))
				.ConfigureAppConfiguration(Configure)
				.Build();


		private static void Configure(WebHostBuilderContext ctx, IConfigurationBuilder builder)
		{
			var env = ctx.HostingEnvironment;
			builder.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
			builder.AddEnvironmentVariables();

			var conf = builder.Build();
			if (conf.GetValue("KeyVault:IsEnabled", false))
				builder.AddAzureKeyVault(
					$"https://{conf["KeyVault:Vault"]}.vault.azure.net/",
					conf["KeyVault:AppId"],
					new CertificateHelper(StoreName.My, StoreLocation.CurrentUser).GetByType(
						X509FindType.FindByThumbprint,
						conf["KeyVault:Certificate:ThumbPrint"])
				);
		}
	}
}