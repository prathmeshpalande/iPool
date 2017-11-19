using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace iPool
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        String currentUser;
		public HomePage ()
		{
			InitializeComponent();
		}

        public HomePage(String currentUser)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            Title = "iPool - Home";
            setCurrentLocationOnMap();
        }
        public async Task setCurrentLocationOnMap()
        {
            IGeolocator locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            Plugin.Geolocator.Abstractions.Position position = await locator.GetPositionAsync();
            iPoolMap.MoveToRegion( MapSpan.FromCenterAndRadius( new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));
        }

        private void EntryFrom_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new PlaceSearchPage("from"));
        }

        private void EntryTo_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new PlaceSearchPage("to"));
        }
    }
}