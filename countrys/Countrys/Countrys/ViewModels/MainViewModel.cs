namespace Countrys.ViewModels
{
    using System.Collections.Generic;
    using Models;
    public class MainViewModel
    {
        #region Properties
        public List<Country> CountrysList
        {
            get;
            set;
        }

        public TokenResponse Token
        {
            get;
            set;
        }
        #endregion

        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public CountrysViewModel Countrys
        {
            get;
            set;
        }

        public CountryViewModel Country
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
