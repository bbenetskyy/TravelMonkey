using System;
using TravelMonkey.ViewModels;
using Xamarin.Forms;

namespace TravelMonkey.Views
{
    public partial class PlanTripPage : ContentPage
    {
        private readonly PlanTripPageViewModel _viewModel = new PlanTripPageViewModel();

        public PlanTripPage()
        {
            InitializeComponent();
            BindingContext = _viewModel;
        }

        private void Close_Clicked(object sender, EventArgs e) => Navigation.PopModalAsync();
    }
}
