using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iPool
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceSearchPage : ContentPage
    {
        String CallerEntryName = "";
        public PlaceSearchPage()
        {
            InitializeComponent();
        }

        public PlaceSearchPage(String CallerEntryName)
        {
            InitializeComponent();
            this.CallerEntryName = CallerEntryName;
            //var template = new DataTemplate(typeof(TextCell));
            //template.SetValue(TextCell.TextColorProperty, Color.Black);
            //ListViewSearchPlace.ItemTemplate = template;
            //EntrySearchPlace.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            EntrySearchPlace.Focus();
        }

        private async void EntrySearchPlace_Completed(object sender, EventArgs e)
        {
            String PlaceName = EntrySearchPlace.Text.ToString();
            await SearchPlace(PlaceName);
        }

        public async Task SearchPlace(String PlaceName)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://103.210.200.74:9063/searchplace");
            String jsonData = "{\n" +
                "\t\"placename\": \"" + PlaceName + "\",\n" +
                "}";

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            String result = await response.Content.ReadAsStringAsync();
            
            ObservableCollection<Prediction> SearchResultsList = new ObservableCollection<Prediction>();
            ListViewSearchPlace.ItemsSource = SearchResultsList;

            dynamic jsonResult = JsonConvert.DeserializeObject(result);

            foreach(var prediction in jsonResult.predictions)
            {
                SearchResultsList.Add(new Prediction { description = prediction.description.ToString() });
            }

            ListViewSearchPlace.IsVisible = true;
        }
    }
    public class Prediction
    {
        public String description { get; set; }
    }
}