using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iPool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            LabelLogin.Text = "Please wait while you are being logged in";
            LabelLogin.IsVisible = true;
            await PerformLogin();
        }

        private async Task PerformLogin()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://103.210.200.74:9063/login");
            String jsonData = "{\n" +
                "\t\"username\": \"" + EntryUsername.Text + "\",\n" +
                "\t\"password\": \"" + EntryPassword.Text + "\",\n" +
                "}";

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            String result = await response.Content.ReadAsStringAsync();

            switch (result)
            {
                case "0":
                    {
                        LabelLogin.Text = "Error: Wrong Password!";
                        break;
                    }
                case "1":
                    {
                        LabelLogin.TextColor = Color.Green;
                        LabelLogin.Text = "Success: Logged In!";
                        Navigation.InsertPageBefore(new MainPage(EntryUsername.Text.ToString()), Navigation.NavigationStack[0]);
                        Navigation.PopToRootAsync();
                        break;
                    }
                case "2":
                    {
                        LabelLogin.Text = "Error: No such user exists!";
                        break;
                    }
                default:
                    {
                        LabelLogin.Text = "Error: Unknown!";
                        break;
                    }
            }

            LabelLogin.IsVisible = true;

        }

        private async void ButtonSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}