

namespace LanguageForum.Classes
{
	public class ModelLocation
	{
		public string LblAltitude { get; set;}
		public string LblLatitude { get; set;}
		public string LblLongitude { get; set;}
		public string LblCourse { get; set;}
		public string LblMagneticHeading { get; set;}
		public string LblSpeed { get; set;}
		public string LblTrueHeading { get; set;}
		public string LblDistanceToParis { get; set;}

		public static ModelLocation instance = null;

		public static ModelLocation currenLocation()
		{
			if (instance == null)
			{
				instance = new ModelLocation();
			}

			return instance;
		}
	}
}