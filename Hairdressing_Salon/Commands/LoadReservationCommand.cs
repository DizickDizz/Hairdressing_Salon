using Hairdressing_Salon.Models;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hairdressing_Salon.Commands
{
    public class LoadReservationCommand : AsyncCommandBase
    {
        private readonly SalonStore _salonStore;
        private readonly ReservationListingViewModel _viewModel;

        public LoadReservationCommand(SalonStore salonStore, ReservationListingViewModel viewModel)
        {
            _salonStore = salonStore;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _salonStore.Load();

                _viewModel.UpdateReservations(_salonStore.Reservations);
            }
            catch (Exception) { MessageBox.Show("Произшла ошибка при загрузке записей в таблицу"); }
        }
    }
}
