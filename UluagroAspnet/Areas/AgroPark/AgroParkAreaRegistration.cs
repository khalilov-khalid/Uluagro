using System.Web.Mvc;

namespace UluagroAspnet.Areas.AgroPark
{
    public class AgroParkAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AgroPark";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AgroPark_default",
                "AgroPark/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}