using Microsoft.AspNetCore.Mvc;
using ASP_Lr11.Filters;

public class HomeController : Controller
{
    [ServiceFilter(typeof(ActionLoggingFilter))]
    [ServiceFilter(typeof(UniqueUserCounterFilter))]
    public IActionResult Index()
    {
        return View();
    }
}
