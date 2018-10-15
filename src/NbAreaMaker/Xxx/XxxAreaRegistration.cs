using System.Web.Mvc;

namespace Yyy.Web.Areas.Xxx
{
    public class XxxAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Xxx"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                AreaName + "_default",
                "{site}/" + AreaName + "/{controller}/{action}",
                new { area = AreaName },
                new[] { GetType().Namespace + ".Controllers" });
        }

        //public override void RegisterArea(AreaRegistrationContext context)
        //{
        //    context.MapRoute(
        //        AreaName + "_default",
        //        AreaName + "/{controller}/{action}",
        //        new { area = AreaName },
        //        new[] { GetType().Namespace + ".Controllers" });
        //}
    }
}