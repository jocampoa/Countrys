[assembly: Xamarin.Forms.ExportRenderer(
    typeof(Countrys.Views.LoginFacebookPage),
    typeof(Countrys.iOS.Implementations.LoginPageRenderer))]

namespace Countrys.iOS.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Service;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.iOS;

    public class LoginPageRenderer : PageRenderer
    {
        bool done = false;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (done)
            {
                return;
            }

            var auth = new OAuth2Authenticator(
                clientId: "162358324447361",
                scope: "",
                authorizeUrl: new Uri("https://www.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                DismissViewController(true, null);
                App.HideLoginView();

                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    await App.NavigateToProfile(profile);
                }
                else
                {
                    await App.NavigateToProfile(null);
                }
            };

            done = true;
            PresentViewController(auth.GetUI(), true, null);
        }

        private async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var apiService = new ApiService();
            return await apiService.GetFacebook(accessToken);
        }
    }
}