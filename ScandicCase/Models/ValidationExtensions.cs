namespace ScandicCase.Models
{
	internal static class ValidationExtensions
	{
		public static bool IsGuestValidForCountry(this Guest guest, Country countryCode)
		{
			// First just checking that the guest have a name
			var nameValid = !string.IsNullOrWhiteSpace(guest.FirstName)  && !string.IsNullOrWhiteSpace(guest.LastName);

			// Then checking if we must have a title and if so it it is given
			var titleValid = countryCode != Country.DE || (countryCode == Country.DE && !string.IsNullOrWhiteSpace(guest.Title));

			return nameValid && titleValid;
		}
	}
}
