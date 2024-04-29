using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var detailsList = await App.Database.GetDetailsAsync();
            if (detailsList != null)
            {
                detailsD.ItemsSource = detailsList;
            }

            //collectionView.ItemsSource = await App.Database.GetDetailsAsync();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(meterN.Text) && typeOfReg.SelectedItem != null && !string.IsNullOrWhiteSpace(currentR.Text) && !string.IsNullOrWhiteSpace(previousR.Text))
            {

                string type = typeOfReg.SelectedItem.ToString();

                ////////////////////  CALCULATIONS  ////////////////////

                double ConsumptionReading, PresentReading1, PreviousReading1,
                    ElectricityCharge, PerKiloWatt, DemandCharge, ServiceCharge,
                    PrincipalAmount1, AmountPayable1, VAT;

                PresentReading1 = int.Parse(currentR.Text);
                PreviousReading1 = int.Parse(previousR.Text);

                ConsumptionReading = PresentReading1 - PreviousReading1;

                if (ConsumptionReading < 75)
                {
                    PerKiloWatt = 6.50;
                }
                else if (ConsumptionReading <= 150)
                {
                    PerKiloWatt = 9.50;
                }
                else if (ConsumptionReading <= 300)
                {
                    PerKiloWatt = 10.50;
                }
                else if (ConsumptionReading <= 400)
                {
                    PerKiloWatt = 12.50;
                }
                else if (ConsumptionReading <= 500)
                {
                    PerKiloWatt = 14.00;
                }
                else
                {
                    PerKiloWatt = 16.50;
                }

                ElectricityCharge = ConsumptionReading * PerKiloWatt;

                if (type == "H")
                {
                    DemandCharge = 200;
                    ServiceCharge = 50;
                }
                else if (type == "B")
                {
                    DemandCharge = 400;
                    ServiceCharge = 100;
                }
                else
                {
                    DemandCharge = 0;
                    ServiceCharge = 0;
                }

                PrincipalAmount1 = ElectricityCharge + DemandCharge + ServiceCharge;

                VAT = PrincipalAmount1 * 0.05;

                AmountPayable1 = PrincipalAmount1 + VAT;

                ///////////////////////////////////////////////////////

                if (type == "H" || type == "B")
                {
                    Details details = new Details()
                    {
                        MeterNo = int.Parse(meterN.Text),
                        PresentReading = PresentReading1,
                        PreviousReading = PreviousReading1,
                        TypeOfRegistration = type,
                        PrincipalAmount = PrincipalAmount1,
                        AmountPayable = AmountPayable1,
                    };

                    await App.Database.SaveDetailsAsync(details);
                    meterN.Text = currentR.Text = previousR.Text = string.Empty;
                    typeOfReg.SelectedItem = null;

                    await DisplayAlert("Success", "Details Added Successfully", "OK");

                    var detailsList = await App.Database.GetDetailsAsync();
                    if (detailsList != null)
                    {
                        detailsD.ItemsSource = detailsList;
                    }

                }
                else
                {
                    await DisplayAlert("Success", "Invalid Type Of Registration!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Check Your Details!", "OK");
            }
        }
        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meterN.Text))
            {
                var details = await App.Database.GetDetailAsync(int.Parse(meterN.Text));
                if (details != null)
                {

                    currentR.Text = details.PresentReading.ToString();
                    previousR.Text = details.PreviousReading.ToString();
                    typeOfReg.SelectedItem = details.TypeOfRegistration;

                    await DisplayAlert("Success", "Meter Number: " + details.MeterNo, "OK");

                    detailsD.ItemsSource = new List<Details> { details };

                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Meter Number", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(meterN.Text) && typeOfReg.SelectedItem != null && !string.IsNullOrWhiteSpace(currentR.Text) && !string.IsNullOrWhiteSpace(previousR.Text))
            {
                string type = typeOfReg.SelectedItem.ToString();
                ////////////////////  CALCULATIONS  ////////////////////

                double ConsumptionReading, PresentReading1, PreviousReading1,
                    ElectricityCharge, PerKiloWatt, DemandCharge, ServiceCharge,
                    PrincipalAmount1, AmountPayable1, VAT;

                PresentReading1 = int.Parse(currentR.Text);
                PreviousReading1 = int.Parse(previousR.Text);

                ConsumptionReading = PresentReading1 - PreviousReading1;

                if (ConsumptionReading < 75)
                {
                    PerKiloWatt = 6.50;
                }
                else if (ConsumptionReading <= 150)
                {
                    PerKiloWatt = 9.50;
                }
                else if (ConsumptionReading <= 300)
                {
                    PerKiloWatt = 10.50;
                }
                else if (ConsumptionReading <= 400)
                {
                    PerKiloWatt = 12.50;
                }
                else if (ConsumptionReading <= 500)
                {
                    PerKiloWatt = 14.00;
                }
                else
                {
                    PerKiloWatt = 16.50;
                }

                ElectricityCharge = ConsumptionReading * PerKiloWatt;

                if (type == "H")
                {
                    DemandCharge = 200;
                    ServiceCharge = 50;
                }
                else if (type == "B")
                {
                    DemandCharge = 400;
                    ServiceCharge = 100;
                }
                else
                {
                    DemandCharge = 0;
                    ServiceCharge = 0;
                }

                PrincipalAmount1 = ElectricityCharge + DemandCharge + ServiceCharge;

                VAT = PrincipalAmount1 * 0.05;

                AmountPayable1 = PrincipalAmount1 + VAT;

                ///////////////////////////////////////////////////////

                if (type == "H" || type == "B")
                {
                    Details detail = new Details()
                    {
                        MeterNo = int.Parse(meterN.Text),
                        TypeOfRegistration = type,
                        PresentReading = PresentReading1,
                        PreviousReading = PreviousReading1,
                        PrincipalAmount = PrincipalAmount1,
                        AmountPayable = AmountPayable1,
                    };

                    await App.Database.UpdateDetailsAsync(detail);

                    meterN.Text = currentR.Text = previousR.Text = string.Empty;
                    typeOfReg.SelectedItem = null;

                    await DisplayAlert("Success", "Details Updated Successfully", "OK");

                    var detailsList = await App.Database.GetDetailsAsync();
                    if (detailsList != null)
                    {
                        detailsD.ItemsSource = detailsList;
                    }
                }
                else
                {
                    await DisplayAlert("Success", "Invalid Type Of Registration!", "OK");
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Check Your Details", "OK");
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(meterN.Text))
            {
                var detail = await App.Database.GetDetailAsync(int.Parse(meterN.Text));
                if (detail != null)
                {
                    await App.Database.DeleteDetailsAsync(detail);
                    meterN.Text = currentR.Text = previousR.Text = string.Empty;
                    typeOfReg.SelectedItem = null;

                    await DisplayAlert("Success", "Details Deleted", "OK");

                    var detailsList = await App.Database.GetDetailsAsync();
                    if (detailsList != null)
                    {
                        detailsD.ItemsSource = detailsList;
                    }

                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter Meter Number", "OK");
            }
        }
    }
}