using Hairdressing_Salon.Commands;
using Hairdressing_Salon.Models;
using Hairdressing_Salon.Services;
using Hairdressing_Salon.Stores;
using Hairdressing_Salon.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hairdressing_Salon.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private string _username;
        public string UserName 
        { 
            get 
            { 
                return _username; 
            } 
            set 
            { 
                _username = value; 
                OnPropertyChanged(nameof(UserName));
            } 
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        private string _serviceType;
        public string ServiceType
        {
            get
            {
                return _serviceType;
            }
            set
            {
                _serviceType = value;
                OnPropertyChanged(nameof(ServiceType));
            }
        }
        private DateTime _date = DateTime.Now   ;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        private int _price;
        public int Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private TimeSpan _time = DateTime.Now.TimeOfDay;


        public TimeSpan Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }


        public MakeReservationViewModel(SalonStore salonStore, NavigationService<ReservationListingViewModel> reservationViewNavigationService)
        {
            SubmitCommand = new MakeReservationCommand<ReservationListingViewModel>(this, salonStore, reservationViewNavigationService);
            CancelCommand = new NavigateCommand<ReservationListingViewModel>(reservationViewNavigationService);
        }
    }
}
