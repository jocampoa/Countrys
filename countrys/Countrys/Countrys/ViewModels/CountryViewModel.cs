namespace Countrys.ViewModels
{
    using Models;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CountryViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Border> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;
        #endregion

        #region Properties
        public Country Country
        {
            get;
            set;
        }

        public ObservableCollection<Border> Borders
        {
            get { return this.borders; }
            set { this.SetValue(ref this.borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { this.SetValue(ref this.currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { this.SetValue(ref this.languages, value); }
        }
        #endregion

        #region Constructors
        public CountryViewModel(Country country)
        {
            this.Country = country;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Country.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Country.Languages);
        }
        #endregion

        #region Methods
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Border>();
            foreach (var border in this.Country.Borders)
            {
                var country = MainViewModel.GetInstance().CountrysList.
                                        Where(l => l.Alpha3Code == border).
                                        FirstOrDefault();
                if (country != null)
                {
                    this.Borders.Add(new Border
                    {
                        Code = country.Alpha3Code,
                        Name = country.Name,
                    });
                }
            }
        }
        #endregion
    }
}
