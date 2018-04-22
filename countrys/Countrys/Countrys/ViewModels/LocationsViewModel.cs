namespace Countrys.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Countrys.Helpers;
    using Models;
    using Service;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public class LocationsViewModel
    {
        #region Services
        ApiService apiService;
        #endregion

        #region Properties
        public ObservableCollection<Pin> Pins
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public LocationsViewModel()
        {
            instance = this;

            apiService = new ApiService();
        }
        #endregion

        #region Sigleton
        static LocationsViewModel instance;

        public static LocationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new LocationsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public async Task LoadPins()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                connection.Message,
                Languages.Accept);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();

            var response = await apiService.GetList<Location>(
                "http://productszuluapi.azurewebsites.net",
                "/api",
                "/Locations",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   response.Message,
                   Languages.Accept);
                return;
            }

            var locations = (List<Location>)response.Result;
            Pins = new ObservableCollection<Pin>();
            foreach (var location in locations)
            {
                Pins.Add(new Pin
                {
                    Address = location.Address,
                    Label = location.Description,
                    Position = new Position(
                        location.Latitude,
                        location.Longitude),
                    Type = PinType.Place,
                });
            }
        }
        #endregion
    }
}
