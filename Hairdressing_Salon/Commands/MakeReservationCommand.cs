using Hairdressing_Salon.Exceptions;
using Hairdressing_Salon.Models;
using Hairdressing_Salon.Services;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hairdressing_Salon.Commands
{
    public class MakeReservationCommand<TViewModel> : AsyncCommandBase where TViewModel : ViewModelBase
    {
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly SalonStore _salonStore;
        private readonly NavigationService<TViewModel> _navigationService;


        public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel, SalonStore salonStore, NavigationService<TViewModel> navigationService)
        {
            _makeReservationViewModel = makeReservationViewModel;
            _salonStore = salonStore;

            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
                OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            DateTime date = _makeReservationViewModel.Date;
            TimeSpan time = _makeReservationViewModel.Time;
            bool first_sentence = (date.Date == DateTime.Now.Date) && (time >= DateTime.Now.TimeOfDay);
            bool second_sentence = (date.Date > DateTime.Now.Date);
            return !string.IsNullOrEmpty(_makeReservationViewModel.UserName) &&
                !string.IsNullOrEmpty(_makeReservationViewModel.PhoneNumber) &&
                !string.IsNullOrEmpty(_makeReservationViewModel.ServiceType) &&   
                (first_sentence || second_sentence);
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)    
        {
            Reservation reservation = new Reservation(
                _makeReservationViewModel.UserName,
                _makeReservationViewModel.PhoneNumber,
                _makeReservationViewModel.Date,
                _makeReservationViewModel.Time,
                new Service(_makeReservationViewModel.ServiceType, _makeReservationViewModel.Price));

            try
            {
                await _salonStore.MakeReservation(reservation);
                _navigationService.Navigate();
            }
            catch (ReservationConflictException)
            {
                MessageBox.Show("На это время уже есть запись");
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла ошибка при создании записи");
            }

        }
    }
}
