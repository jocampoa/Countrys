namespace Countrys
{
    using Xamarin.Forms;
    using Views;
    using ViewModels;
    using Helpers;
    using Service;
    using Models;
    using System;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            if (Settings.IsRemembered == "true")
            {
                var dataService = new DataService();

                var token = dataService.First<TokenResponse>(false);

                if (token != null && token.Expires > DateTime.Now)
                {
                    var user = dataService.First<UserLocal>(false);
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Countrys = new CountrysViewModel();
                    Application.Current.MainPage = new MasterPage();
                }
                else
                {
                    this.MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                Application.Current.MainPage = new MasterPage();
            }
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {
            if (profile == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();
            var dataService = new DataService();

            var urlAPI = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginFacebook(
                urlAPI,
                "/api",
                "/Users/LoginFacebook",
                profile);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var user = await apiService.GetUserByEmail(
                urlAPI,
                "/api",
                "/Users/GetUserByEmail",
                token.TokenType,
                token.AccessToken,
                token.UserName);

            UserLocal userLocal = null;
            if (user != null)
            {
                userLocal = Converter.ToUserLocal(user);
                dataService.DeleteAllAndInsert(userLocal);
                dataService.DeleteAllAndInsert(token);
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;
            mainViewModel.Countrys = new CountrysViewModel();
            Application.Current.MainPage = new MasterPage();
            Settings.IsRemembered = "true";

            mainViewModel.Countrys = new CountrysViewModel();
            Application.Current.MainPage = new MasterPage();
        }
        #endregion
    }
}
