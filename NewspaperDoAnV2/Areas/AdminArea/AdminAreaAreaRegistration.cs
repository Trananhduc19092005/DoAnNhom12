using System.Web.Mvc;

namespace NewspaperDoAnV2.Areas.AdminArea
{
    public class AdminAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminArea_default",
                "AdminArea/{controller}/{action}/{id}",
                new { controller = "Users" ,action = "LogIn", id = "" }
            );

            
        }
    }
}