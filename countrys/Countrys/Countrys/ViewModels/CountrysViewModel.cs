namespace Countrys.ViewModels
{
    using Service;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Models;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;

    public class CountrysViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<CountryItemViewModel> countrys;
        private bool isRefreshing;
        private string filter;
        private List<Country> countrysList;
        #endregion

        #region Properties
        public ObservableCollection<CountryItemViewModel> Countrys
        {
            get { return this.countrys; }
            set { SetValue(ref this.countrys, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public CountrysViewModel()
        {
            this.apiService = new ApiService();
            this.LoadCountrys();
        }
        #endregion

        #region Methods
        private async void LoadCountrys()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Country>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            MainViewModel.GetInstance().CountrysList = (List<Country>)response.Result;
            this.Countrys = new ObservableCollection<CountryItemViewModel>(
                this.ToCountryItemViewModel());
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        private IEnumerable<CountryItemViewModel> ToCountryItemViewModel()
        {
            return MainViewModel.GetInstance().CountrysList.Select(l => new CountryItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCountrys);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Countrys = new ObservableCollection<CountryItemViewModel>(
                    this.ToCountryItemViewModel());
            }
            else
            {
                this.Countrys = new ObservableCollection<CountryItemViewModel>(
                    this.ToCountryItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
