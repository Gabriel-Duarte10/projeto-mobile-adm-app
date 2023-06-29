using projeto_mobile_adm_app.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_mobile_adm_app.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        private INavigation _navigation;

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }

                return _instance;
            }
        }

        public void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void NavigateToPasswordResetPage(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            
            var passwordResetPage = new PasswordResetPage(token);
            _navigation.PushModalAsync(passwordResetPage);
        }
    }

}
