namespace Countrys.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Models;
    using Views;
    public class CountryItemViewModel : Country
    {
        #region Commands
        public ICommand SelectCountryCommand
        {
            get
            {
                return new RelayCommand(SelectCountry);
            }
        }

        private async void SelectCountry()
        {
            MainViewModel.GetInstance().Country = new CountryViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new CountryTabbedPage());
        }
        #endregion
    }
}
