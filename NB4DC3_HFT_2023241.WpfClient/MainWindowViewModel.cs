using NB4DC3_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace NB4DC3_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Car> Cars { get; set; }

        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
                {
                    selectedCar = new Car()
                    {
                        Model = value.Model,
                        CarID = value.CarID
                    };
                    OnPropertyChanged();
                    (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateCarCommand { get; set; }
        public ICommand DeleteCarCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Cars = new RestCollection<Car>("http://localhost:61242/", "car", "hub");
                CreateCarCommand = new RelayCommand(() =>
                {
                    Cars.Add(new Car()
                    {
                        Model = SelectedCar.Model
                    });
                });
                UpdateCarCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Cars.Update(SelectedCar);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteCarCommand = new RelayCommand(() =>
                {
                    Cars.Delete(SelectedCar.CarID);
                },
                () =>
                {
                    return SelectedCar != null;
                });
                SelectedCar = new Car();
            }

        }
    }
}
