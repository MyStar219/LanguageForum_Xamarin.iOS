using System;
using CoreLocation;
using UIKit;
using LanguageForum.Global;
using System.Threading.Tasks;

namespace LanguageForum.Classes
{
	public class LocationManager
	{

		ModelLocation mainScreen = null;

		CLLocationManager iPhoneLocationManager = null;
        LocationDelegate locationDelegate = null;

		public LocationManager()
		{
			// initialize our location manager and callback handler
			iPhoneLocationManager = new CLLocationManager();

			// uncomment this if you want to use the delegate pattern:
			//locationDelegate = new LocationDelegate (mainScreen);
			//iPhoneLocationManager.Delegate = locationDelegate;

			// you can set the update threshold and accuracy if you want:
			iPhoneLocationManager.DistanceFilter = 100; // move ten meters before updating
			//iPhoneLocationManager.HeadingFilter = 3; // move 3 degrees before updating

			// you can also set the desired accuracy:
			iPhoneLocationManager.DesiredAccuracy = 50; // 1000 meters/1 kilometer
														  // you can also use presets, which simply evalute to a double value:
														  //iPhoneLocationManager.DesiredAccuracy = CLLocation.AccuracyNearestTenMeters;

			// handle the updated location method and update the UI
			if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
			{
				iPhoneLocationManager.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) =>
				{
					UpdateLocationAsync(mainScreen, e.Locations[e.Locations.Length - 1]);
				};
			}
			else {
				#pragma warning disable 618
				// this won't be called on iOS 6 (deprecated)
				iPhoneLocationManager.UpdatedLocation += (object sender, CLLocationUpdatedEventArgs e) =>
				{
					UpdateLocationAsync(mainScreen, e.NewLocation);
				};
				#pragma warning restore 618
			}

            iPhoneLocationManager.ShouldDisplayHeadingCalibration += (CLLocationManager manager) =>
            {
                return false;
            };

			//iOS 8 requires you to manually request authorization now - Note the Info.plist file has a new key called requestWhenInUseAuthorization added to.
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				iPhoneLocationManager.RequestWhenInUseAuthorization();
			}

			// handle the updated heading method and update the UI
			iPhoneLocationManager.UpdatedHeading += (object sender, CLHeadingUpdatedEventArgs e) =>
			{
				//mainScreen.LblMagneticHeading = e.NewHeading.MagneticHeading.ToString() + "º";
				//mainScreen.LblTrueHeading = e.NewHeading.TrueHeading.ToString() + "º";
			};

			// start updating our location, et. al.
			if (CLLocationManager.LocationServicesEnabled)
				iPhoneLocationManager.StartUpdatingLocation();
			if (CLLocationManager.HeadingAvailable)
				iPhoneLocationManager.StartUpdatingHeading();
			
		}

        private
        static async Task UpdateLocationAsync(ModelLocation ms, CLLocation newLocation)
        {
            if (ms == null)
            {
                ms = ModelLocation.currenLocation();
            }

            if (newLocation == null) return;

            ms.LblAltitude = newLocation.Altitude.ToString() + " meters";
            ms.LblLongitude = newLocation.Coordinate.Longitude.ToString() + "º";
            ms.LblLatitude = newLocation.Coordinate.Latitude.ToString() + "º";
            ms.LblCourse = newLocation.Course.ToString() + "º";
            ms.LblSpeed = newLocation.Speed.ToString() + " meters/s";


            SingleData.getInstance().currentLat = newLocation.Coordinate.Latitude;
            SingleData.getInstance().currentLng = newLocation.Coordinate.Longitude;

            await ReverseGeocodeToConsoleAsync(newLocation);

            //ViewCtrl_Location.locationUpdateNotifyHandler(null, null);

            // get the distance from here to paris
            //ms.LblDistanceToParis = (newLocation.DistanceFrom(new CLLocation(48.857, 2.351)) / 1000).ToString() + " km";

            // Update Current Location
            //if (SingleData.SingleData.getInstance().currentGPSLocation == null)
            //{
            //	SingleData.SingleData.getInstance().currentGPSLocation = new AccountSavedLocation("", "");
            //}
            //SingleData.SingleData.getInstance().currentGPSLocation.Lat = newLocation.Coordinate.Latitude;
            //SingleData.SingleData.getInstance().currentGPSLocation.Long = newLocation.Coordinate.Longitude;

            //getAddressFromGeoCoord(newLocation).ContinueWith((t) => { });
            //getAddressFromCoord(newLocation).ContinueWith((t) => { });
        }

        static async Task ReverseGeocodeToConsoleAsync(CLLocation location)
        {
            var geoCoder = new CLGeocoder();
            var placemarks = await geoCoder.ReverseGeocodeLocationAsync(location);
            foreach (var placemark in placemarks)
            {
                Console.WriteLine(placemark);

                SingleData.getInstance().locationDesc = placemark.ToString();

            }
        }

        static async Task getAddressFromGeoCoord(CLLocation _location)
        {
            CLGeocoder geo = new CLGeocoder();

            //CLPlacemark[] arrayList =geo.ReverseGeocodeLocationAsync(_location);

            var arrayList = geo.ReverseGeocodeLocationAsync(_location).ContinueWith((t) => { });
            //Android.Locations.Location location
            //IList<Address> arrayList = await geo.GetFromLocationAsync(location.Latitude, location.Longitude, 1);

            //         for (int i = 0; i < arrayList.Length; i++)
            //{
            //	CLPlacemark address = arrayList[i];


            //             SingleData.SingleData.getInstance().currentGPSLocation.Description = address.Description;


            //             string country = address.Country;
            //             string adminArea = address.AdministrativeArea;
            //             string subAdminArea = address.SubAdministrativeArea;
            //	string localty = address.Locality;
            //	string subLocalty = address.SubLocality;
            //	string thoroughfare = address.Thoroughfare;
            //	string subThoroughfare = address.SubThoroughfare;

            //             SingleData.SingleData.getInstance().currentGPSLocation.Name = address.AddressDictionary.ToString();
            //             SingleData.SingleData.getInstance().currentGPSLocation.Address = address.Description;

            //}
        }


		// If you don't want to use events, you could define a delegate to do the
		// updates as well, as shown here.
		public class LocationDelegate : CLLocationManagerDelegate
		{
			ModelLocation ms;

			public LocationDelegate(ModelLocation mainScreen) : base()
			{
				ms = mainScreen;
			}

			// called for iOS6 and later
			public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
			{
				LocationManager.UpdateLocationAsync(ms, locations[locations.Length - 1]);
			}

            public override bool ShouldDisplayHeadingCalibration(CLLocationManager manager)
            {
                return false;
            }

			public override void UpdatedHeading(CLLocationManager manager, CLHeading newHeading)
			{
				//ms.LblMagneticHeading = newHeading.MagneticHeading.ToString() + "º";
				//ms.LblTrueHeading = newHeading.TrueHeading.ToString() + "º";
			}
		}

		public CLLocationManager LocMgr
		{
			get { return this.iPhoneLocationManager; }
		}
	}
}