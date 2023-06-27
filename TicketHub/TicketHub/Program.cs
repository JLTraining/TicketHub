using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketHub.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketHubContext") ?? throw new InvalidOperationException("Connection string 'TicketHubContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
	.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options =>
{
	options.ResourcesPath = "Resources";
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	var supportedCultures = new[]
	{
        new CultureInfo("lv-LV"),
        new CultureInfo("en-US")
	};

	//options.DefaultRequestCulture = new RequestCulture("lv-LV");
	options.SupportedUICultures = supportedCultures;
	options.SupportedCultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseRequestLocalization();
app.Use(async (context, next) =>
{
    // Set the current culture based on the value stored in the session
    var culture = context.Session.GetString("lv-LV");
    if (!string.IsNullOrEmpty(culture))
    {
        var cultureInfo = new CultureInfo(culture);
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
    }

    await next.Invoke();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
DatabaseInitializer.Seed(app);
app.MapRazorPages();
app.Run();
