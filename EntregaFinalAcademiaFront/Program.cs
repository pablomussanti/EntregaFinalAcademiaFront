using Microsoft.AspNetCore.Authentication.Cookies;

namespace EntregaFinalAcademiaFront
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("useApi", config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["ServiceUrl:ApiUrl"]);
            });

			builder.Services.AddAuthentication(option =>
			{
				option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
			{
				config.Events.OnRedirectToLogin = context =>
				{
					context.Response.Redirect("https://localhost:7212");
					return Task.CompletedTask;
				};
			});

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("ADMINISTRADOR", policy =>
				{
					policy.RequireRole("Administrador");
				});

				options.AddPolicy("CONSULTOR", policy =>
				{
					policy.RequireRole("Consultor");
				});
			});


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}