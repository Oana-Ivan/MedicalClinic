using System.Web;
using System.Web.Mvc;

namespace clinicaMedicala4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // toate actiunile pot fi accesate doar de utilizatorii inregistrati
            filters.Add(new AuthorizeAttribute());

            filters.Add(new HandleErrorAttribute());
        }
    }
}
