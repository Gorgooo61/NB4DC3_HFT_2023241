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
    public class OrderEditorViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Order> Orders { get; set; }

        private Order selectedOrder;

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (value != null)
                {
                    selectedOrder = new Order()
                    {
                        HowManyDays = value.HowManyDays,
                        OrderID = value.OrderID
                    };
                    OnPropertyChanged();
                    (DeleteOrderCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }
        public ICommand UpdateOrderCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public OrderEditorViewModel()
        {

            if (!IsInDesignMode)
            {
                Orders = new RestCollection<Order>("http://localhost:61242/", "order", "hub");
                CreateOrderCommand = new RelayCommand(() =>
                {
                    Orders.Add(new Order()
                    {
                        HowManyDays = SelectedOrder.HowManyDays
                    });
                });
                UpdateOrderCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Orders.Update(SelectedOrder);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });
                DeleteOrderCommand = new RelayCommand(() =>
                {
                    Orders.Delete(SelectedOrder.OrderID);
                },
                () =>
                {
                    return SelectedOrder != null;
                });
                SelectedOrder = new Order();
            }

        }
    }
}
