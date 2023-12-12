using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace WinFormsApp1
{
    internal static class Program
    {
        // Dependency Injection configuration
        public static class DependencyInjectionConfig
        {
            public static IServiceProvider Configure()
            {
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<Calculator>()
                    .AddSingleton<IDbConnectionFactory,DbConnectionFactory>()
                    .AddScoped<IRecentDataAccessor,RecentDataAccessor>()
                    .AddScoped<ICommand, CalculatorCommand>()
                    .AddScoped<Form1>()

                    .BuildServiceProvider();


                return serviceProvider;
            }
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // InitDependency();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configure DI
            var serviceProvider = DependencyInjectionConfig.Configure();
            {
                // Resolve and run the main form
                Application.Run(serviceProvider.GetRequiredService<Form1>());
            }
        }

 
    }
}