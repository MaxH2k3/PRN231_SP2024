using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System;
using WatercolorsPaintingRepository.Entity;

namespace PEPRN231_FA24_TongTranLeHuy_BE.Configuration
{
	public static class ConfigOdata
	{
		public static IEdmModel GetEdmModel()
		{
			var builder = new ODataConventionModelBuilder();

			// Register entity sets
			builder.EntitySet<Style>("Style");
			builder.EntitySet<UserAccount>("UserAccount");
			builder.EntitySet<WatercolorsPainting>("WatercolorsPainting");

			var styleEntity = builder.EntityType<Style>();
			styleEntity.HasKey(s => s.StyleId);

			var userAccountEntity = builder.EntityType<UserAccount>();
			userAccountEntity.HasKey(u => u.UserAccountId);

			var watercolorsPaintingEntity = builder.EntityType<WatercolorsPainting>();
			watercolorsPaintingEntity.HasKey(w => w.PaintingId);

			return builder.GetEdmModel();
		}
	}
}
