using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iPool
{
    public partial class MainPage : ContentPage
    {
        String CurrentUser = "";
        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(String CurrentUser)
        {
            InitializeComponent();
            this.CurrentUser = CurrentUser;
            LabelUsername.Text = "Welcome, " + CurrentUser + "!";
            LabelUsername.IsVisible = true;
        }

        public async void ButtonProvidePool_Clicked(object sender, EventArgs e)
        {
            LabelRequestResponse.TextColor = Color.Black;
            LabelRequestResponse.Text = "Please wait while your request is being processed";
            LabelRequestResponse.IsVisible = true;
            await ProvidePool();
        }

        private async Task ProvidePool()
        {
            if (ButtonProvidePool.Text.ToString().Equals("Add to Provider Pool"))
            {
                HttpClient client = new HttpClient();
                var uri = new Uri("http://103.210.200.74:9063/addprovidepool");
                String jsonData = "{\n" +
                    "\t\"username\": \"" + CurrentUser + "\",\n" +
                    "\t\"sourceLat\": \"" + EntrySourceCoor.Text.Split(',')[0] + "\",\n" +
                    "\t\"sourceLon\": \"" + EntrySourceCoor.Text.Split(',')[1] + "\",\n" +
                    "\t\"destLat\": \"" + EntryDestCoor.Text.Split(',')[0] + "\",\n" +
                    "\t\"destLon\": \"" + EntryDestCoor.Text.Split(',')[1] + "\"\n" +
                    "}";

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                String result = await response.Content.ReadAsStringAsync();

                switch (result)
                {
                    case "0":
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Failed to add to Provider Pool!";
                            break;
                        }
                    case "1":
                        {
                            LabelRequestResponse.TextColor = Color.Green;
                            LabelRequestResponse.Text = "Success: Added to Provider Pool!";
                            break;
                        }
                    case "2":
                        {
                            LabelRequestResponse.TextColor = Color.Orange;
                            LabelRequestResponse.Text = "Error: Already in Provider Pool!";
                            break;
                        }
                    default:
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Unknown!";
                            break;
                        }
                }

                LabelRequestResponse.IsVisible = true;

                ButtonProvidePool.TextColor = Color.Orange;
                ButtonProvidePool.Text = "Remove from Provider Pool";
            }
            else if(ButtonProvidePool.Text.ToString().Equals("Remove from Provider Pool"))
            {
                HttpClient client = new HttpClient();
                var uri = new Uri("http://103.210.200.74:9063/removeprovidepool");
                String jsonData = "{\n" +
                    "\t\"username\": \"" + CurrentUser + "\",\n" +
                    "}";

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                String result = await response.Content.ReadAsStringAsync();

                switch (result)
                {
                    case "0":
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Already not in Provider Pool!";
                            break;
                        }
                    case "1":
                        {
                            LabelRequestResponse.TextColor = Color.Orange;
                            LabelRequestResponse.Text = "Success: Removed from Provider Pool!";
                            break;
                        }
                    default:
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Unknown!";
                            break;
                        }
                }

                LabelRequestResponse.IsVisible = true;

                ButtonProvidePool.TextColor = Color.Green;
                ButtonProvidePool.Text = "Add to Provider Pool";
            }

        }

        private async void ButtonRequestPool_Clicked(object sender, EventArgs e)
        {
            LabelRequestResponse.TextColor = Color.Black;
            LabelRequestResponse.Text = "Please wait while your request is being processed";
            LabelRequestResponse.IsVisible = true;
            await RequestPool();
        }

        private async Task RequestPool()
        {
            if (ButtonRequestPool.Text.ToString().Equals("Add to Requester Pool"))
            {
                HttpClient client = new HttpClient();
                var uri = new Uri("http://103.210.200.74:9063/addrequestpool");
                String jsonData = "{\n" +
                    "\t\"username\": \"" + CurrentUser + "\",\n" +
                    "\t\"sourceLat\": \"" + EntrySourceCoor.Text.Split(',')[0] + "\",\n" +
                    "\t\"sourceLon\": \"" + EntrySourceCoor.Text.Split(',')[1] + "\",\n" +
                    "\t\"destLat\": \"" + EntryDestCoor.Text.Split(',')[0] + "\",\n" +
                    "\t\"destLon\": \"" + EntryDestCoor.Text.Split(',')[1] + "\"\n" +
                    "}";

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                String result = await response.Content.ReadAsStringAsync();

                switch (result)
                {
                    case "0":
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Failed to add to Requester Pool!";
                            break;
                        }
                    case "1":
                        {
                            LabelRequestResponse.TextColor = Color.Green;
                            LabelRequestResponse.Text = "Success: Added to Requester Pool!";
                            break;
                        }
                    case "2":
                        {
                            LabelRequestResponse.TextColor = Color.Orange;
                            LabelRequestResponse.Text = "Error: Already in Requester Pool!";
                            break;
                        }
                    default:
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Unknown!";
                            break;
                        }
                }
                LabelRequestResponse.IsVisible = true;

                ButtonRequestPool.TextColor = Color.Orange;
                ButtonRequestPool.Text = "Remove from Requester Pool";
            }
            else if(ButtonRequestPool.Text.ToString().Equals("Remove from Requester Pool"))
            {
                HttpClient client = new HttpClient();
                var uri = new Uri("http://103.210.200.74:9063/removerequestpool");
                String jsonData = "{\n" +
                    "\t\"username\": \"" + CurrentUser + "\",\n" +
                    "}";

                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                String result = await response.Content.ReadAsStringAsync();

                switch (result)
                {
                    case "0":
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Already not in Provider Pool!";
                            break;
                        }
                    case "1":
                        {
                            LabelRequestResponse.TextColor = Color.Orange;
                            LabelRequestResponse.Text = "Success: Removed from Requester Pool!";
                            break;
                        }
                    default:
                        {
                            LabelRequestResponse.TextColor = Color.Red;
                            LabelRequestResponse.Text = "Error: Unknown!";
                            break;
                        }
                }
                LabelRequestResponse.IsVisible = true;

                ButtonRequestPool.TextColor = Color.Green;
                ButtonRequestPool.Text = "Add to Requester Pool";
            }
        }
    }
}
