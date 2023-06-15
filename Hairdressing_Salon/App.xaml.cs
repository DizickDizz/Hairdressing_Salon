using Hairdressing_Salon.DbContexts;
using Hairdressing_Salon.Exceptions;
using Hairdressing_Salon.Models;
using Hairdressing_Salon.Services;
using Hairdressing_Salon.Services.ReservationConflictValidators;
using Hairdressing_Salon.Services.ReservationCreators;
using Hairdressing_Salon.Services.ReservationProviders;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows;

namespace Hairdressing_Salon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((c) =>
                {
                    c.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");

                    services.AddSingleton(new SalonDBContextFactory(connectionString));
                    services.AddSingleton<IReservationPrivider, DataBaseReservationProdiver>();
                    services.AddSingleton<IReservationCreator, DataBaseReservationCreater>();
                    services.AddSingleton<IReservationConflictValidator, DataBaseReservationConflictValidator>();

                    services.AddSingleton<ReservationBook>();
                    services.AddSingleton<Salon>();

                    services.AddTransient((s) => CreateReservationListingViewModel(s));
                    services.AddSingleton<Func<ReservationListingViewModel>>((s) => () => s.GetRequiredService<ReservationListingViewModel>());
                    services.AddSingleton<NavigationService<ReservationListingViewModel>>();

                    services.AddTransient<MakeReservationViewModel>();
                    services.AddSingleton<Func<MakeReservationViewModel>>((s) => () => s.GetRequiredService<MakeReservationViewModel>());
                    services.AddSingleton<NavigationService<MakeReservationViewModel>>();

                    services.AddTransient<ReservationHistoryViewModel>();
                    services.AddSingleton<Func<ReservationHistoryViewModel>>((s) => () => s.GetRequiredService<ReservationHistoryViewModel>());
                    services.AddSingleton<NavigationService<ReservationHistoryViewModel>>();

                    services.AddSingleton<SalonStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        private ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider s)
        {
            return ReservationListingViewModel.LoadViewModel(
                s.GetRequiredService<SalonStore>(),
                s.GetRequiredService<NavigationService<MakeReservationViewModel>>(),
                s.GetRequiredService<NavigationService<ReservationHistoryViewModel>>()); 
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            SalonDBContextFactory salonDBContextFactory = _host.Services.GetRequiredService<SalonDBContextFactory>();

            using (SalonDbContext dbContext = salonDBContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();

            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
             
            base.OnExit(e);
        }
    }
}
