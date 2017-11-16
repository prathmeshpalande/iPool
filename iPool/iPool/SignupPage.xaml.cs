using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iPool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void ButtonSignup_Clicked(object sender, EventArgs e)
        {
            LabelSignup.Text = "Please wait while you are being logged in";
            LabelSignup.IsVisible = true;
            await PerformSignup();
        }

        private async Task PerformSignup()
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://103.210.200.74:9063/signup");
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
                        LabelSignup.Text = "Error: Already a registered user!";
                        break;
                    }
                case "1":
                    {
                        LabelSignup.Text = "Success: Signed Up!";
                        Navigation.InsertPageBefore(new MainPage(EntryUsername.Text.ToString()), Navigation.NavigationStack[0]);
                        Navigation.PopToRootAsync();

                        break;
                    }
                default:
                    {
                        LabelSignup.Text = "Error: Unknown!";
                        break;
                    }
            }

            LabelSignup.IsVisible = true;

        }
    }
}