using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WatercolorsPaintingRepository.Repositories.StyleRepo;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Models
{
	public class WatercolorsPaintingRequest
	{
		public string PaintingName { get; set; } = null!;
		public string PaintingDescription { get; set; } = null!;
		public string PaintingAuthor { get; set; } = null!;
		public decimal Price { get; set; }
		public int PublishYear { get; set; }
		public string StyleId { get; set; } = null!;

		[JsonIgnore]
		public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>()
		{
			{ nameof(PaintingName), string.Empty },
			{ nameof(PaintingDescription), string.Empty },
			{ nameof(PaintingAuthor), string.Empty },
			{ nameof(Price), string.Empty },
			{ nameof(PublishYear), string.Empty },
			{ nameof(StyleId), string.Empty }
		};

		public bool ValidateData(IStyleRepository styleRepository)
		{

			// Kiểm tra PaintingName
			if (string.IsNullOrEmpty(PaintingName) ||
				!System.Text.RegularExpressions.Regex.IsMatch(PaintingName, "^([A-Z][a-z0-9]*\\s)*[A-Z][a-z0-9]*$"))
			{
				Errors[nameof(PaintingName)] = "Each word of the name must begin with a capital letter and include only letters a-z, A-Z, space, and digits 0-9.";
			}

			// Kiểm tra PaintingDescription
			if (string.IsNullOrEmpty(PaintingDescription))
			{
				Errors[nameof(PaintingDescription)] = "PaintingDescription is required.";
			}

			// Kiểm tra PaintingAuthor
			if (string.IsNullOrEmpty(PaintingAuthor))
			{
				Errors[nameof(PaintingAuthor)] = "PaintingAuthor is required.";
			}

			// Kiểm tra Price
			if (Price < 0)
			{
				Errors[nameof(Price)] = "Price must be equal to or greater than 0.";
			}

			// Kiểm tra PublishYear
			if (PublishYear < 1000)
			{
				Errors[nameof(PublishYear)] = "PublishYear must be equal to or greater than 1000.";
			}

			// Kiểm tra StyleId
			if (string.IsNullOrEmpty(StyleId))
			{
				Errors[nameof(StyleId)] = "StyleId is required.";
			}

			CheckExistStyleId(styleRepository);

			return Errors.All(err => string.IsNullOrEmpty(err.Value));
		}

		public void CheckExistStyleId(IStyleRepository styleRepository)
		{
			if(!styleRepository.IsExist(StyleId))
			{
				Errors[nameof(StyleId)] = "StyleId does not exist.";
			}
		}
	}


}
