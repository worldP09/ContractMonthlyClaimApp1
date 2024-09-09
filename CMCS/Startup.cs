using CMCS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure authentication with cookies
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Lecturer/Login";
                options.LogoutPath = "/Lecturer/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });

        // Configure authorization policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy("LecturerOnly", policy => policy.RequireRole("Lecturer"));
            options.AddPolicy("CoordinatorOnly", policy => policy.RequireRole("Coordinator"));
            options.AddPolicy("ManagerOnly", policy => policy.RequireRole("AcademicManager"));
        });

        // Add MVC support
        services.AddControllersWithViews();
        services.AddDistributedMemoryCache();

        // Register IHttpContextAccessor
        services.AddHttpContextAccessor();

        // Add session configuration
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Register the context and other services
        services.AddDbContext<CMCSContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CMCSDatabase")));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Add session and authentication middleware
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
                
    });
    }
}
