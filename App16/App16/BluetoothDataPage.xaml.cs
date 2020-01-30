using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App16
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BluetoothDataPage : ContentPage
    {
        private readonly IDevice _connectedDevice;
        public BluetoothDataPage(IDevice connectedDevice)
        {
            InitializeComponent();
            _connectedDevice = connectedDevice;
        }



        private async void SendMessageButton_Clicked(object sender, EventArgs e)
        { 
            try
            {
                var service = await _connectedDevice.GetServiceAsync(GattCharacteristicIdentifiers_WOOIN.ServiceId);
                if (service != null)
                {
                    var characteristic = await service.GetCharacteristicAsync(GattCharacteristicIdentifiers_WOOIN.WriteData);
                    if (characteristic != null)
                    {
                        byte[] senddata = Encoding.UTF8.GetBytes(string.IsNullOrEmpty(SendMessageLabel.Text) ? "jenx.si was here" : SendMessageLabel.Text);
                        var bytes = await characteristic.WriteAsync(senddata);
                    }
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Notice", ex.Message.ToString(), "Error!");

            }
        }

        private async void GetManufacturerDataButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var service = await _connectedDevice.GetServiceAsync(GattCharacteristicIdentifiers_WOOIN.ServiceId);
                if (service != null)
                {
                    var characteristic = await service.GetCharacteristicAsync(GattCharacteristicIdentifiers_WOOIN.ReadData);
                    if (characteristic != null)
                    {
                        var bytes = await characteristic.ReadAsync();
                        var str = Encoding.UTF8.GetString(bytes);
                        ManufacturerLabel.Text = str;
                    }
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Notice", ex.Message.ToString(), "Error !!");
            }
        }
    }
}