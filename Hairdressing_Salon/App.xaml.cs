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
        private readonly Salon _salon;
        private readonly NavigationStore _navigationStore;
        private readonly SalonStore _salonStore;
        private readonly SalonDBContextFactory _salonDesignTimeDbContextFactory;
        private const string CONNECTION_STRING = "Data Source=salon.db";
        public App()
        {
            _salonDesignTimeDbContextFactory = new SalonDBContextFactory(CONNECTION_STRING);
            IReservationPrivider reservationPrivider = new DataBaseReservationProdiver(_salonDesignTimeDbContextFactory);
            IReservationCreator reservationCreator = new DataBaseReservationCreater(_salonDesignTimeDbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DataBaseReservationConflictValidator(_salonDesignTimeDbContextFactory);
            ReservationBook reservationBook = new ReservationBook(reservationPrivider, reservationCreator, reservationConflictValidator);
            _salon = new Salon(reservationBook);
            _salonStore = new(_salon);
            _navigationStore = new NavigationStore(); 
        }

        protected override void OnStartup(StartupEventArgs e)
            {
            using (SalonDbContext dbContext = _salonDesignTimeDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();

            }


            _navigationStore.CurrentViewModel = CreateReservationViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_salonStore, new NavigationService(_navigationStore, CreateReservationViewModel));
        }

        private ReservationListingViewModel CreateReservationViewModel()
        {
            return ReservationListingViewModel.LoadingViewModel(_salonStore, new NavigationService(_navigationStore, CreateMakeReservationViewModel)  );
        }
    }
}
