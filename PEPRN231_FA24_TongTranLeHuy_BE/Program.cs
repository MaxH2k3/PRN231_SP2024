using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using PEPRN231_FA24_TongTranLeHuy_BE.Configuration;
using Services;
using WatercolorsPaintingRepository.Entity;
using WatercolorsPaintingRepository.Repositories.StyleRepo;
using WatercolorsPaintingRepository.Repositories.UserAccountRepo;
using WatercolorsPaintingRepository.Repositories.WaterColorsPaintingRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStyleRepository, StyleRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IWaterColorsPaintingRepository, WaterColorsPaintingRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddDbContext<WatercolorsPainting2024DBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	options.EnableDetailedErrors();
	options.EnableSensitiveDataLogging();
});

builder.Services.AddControllers().AddOData(opt =>
{
	opt.Filter().Select().Expand().OrderBy().Count().SetMaxTop(100)
	.AddRouteComponents("odata", ConfigOdata.GetEdmModel());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(builder =>
	builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
