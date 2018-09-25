using Hangfire.Annotations;
using Hangfire.Dashboard;
using System.Web;

namespace LaptopWebSite
{
    internal class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public HangfireAuthorizationFilter()
        {
        }

        public bool Authorize([NotNull] DashboardContext context)
        {
            return HttpContext.Current.User.Identity.IsAuthenticated; 
        }
    }
}