using System.Web.Mvc;

namespace ImageControllerSample.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return this.View("Index");
        }
    }
}