using Hangfire;
using LaptopWebSite.Areas.Admin.Controllers;
using LaptopWebSite.Controllers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaptopWebSite.Startup))]
namespace LaptopWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            AdminPanelController obj = new AdminPanelController();
            app.UseHangfireDashboard("/myJobDashboard", new DashboardOptions()
            {
                Authorization = new[] { new HangfireAuthorizationFilter()}
            });
            RecurringJob.AddOrUpdate(
                () => obj.ClearImage(), Cron.Daily(8,0));
            app.UseHangfireServer();
        }
    }
}
