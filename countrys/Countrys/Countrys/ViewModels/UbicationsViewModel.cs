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

    public class UbicationsViewModel
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
        public UbicationsViewModel()
        {
            instance = this;

            apiService = new ApiService();
        }
        #endregion

        #region Sigleton
        static UbicationsViewModel instance;

        public static UbicationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new UbicationsViewModel();
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

            var response = await apiService.GetList<Ubication>(
                "http://productszuluapi.azurewebsites.net",
                "/api",
                "/Ubications",
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

            var ubications = (List<Ubication>)response.Result;
            Pins = new ObservableCollection<Pin>();
            foreach (var ubication in ubications)
            {
                Pins.Add(new Pin
                {
                    Address = ubication.Address,
                    Label = ubication.Description,
                    Position = new Position(
                        ubication.Latitude,
                        ubication.Longitude),
                    Type = PinType.Place,
                });
            }
        }
        #endregion
    }
}
