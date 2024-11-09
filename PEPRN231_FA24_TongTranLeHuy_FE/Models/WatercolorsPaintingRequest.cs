namespace PEPRN231_FA24_TongTranLeHuy_FE.Models
{
	public class WatercolorsPaintingRequest
	{
		public string? PaintingName { get; set; }
		public string? PaintingDescription { get; set; }
		public string? PaintingAuthor { get; set; }
		public decimal? Price { get; set; } = 0;
		public int? PublishYear { get; set; } = 0;
		public string? StyleId { get; set; }
	}

	public class WatercolorsPaintingError
	{
		public string? PaintingName { get; set; }
		public string? PaintingDescription { get; set; }
		public string? PaintingAuthor { get; set; }
		public string? Price { get; set; }
		public string? PublishYear { get; set; }
		public string? StyleId { get; set; }
	}
}
