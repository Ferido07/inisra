using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Inisra_Web_App_MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(string query,string documentType)
        {
            if (String.IsNullOrEmpty(query))
                return View();
            else
            {
                if (String.Equals(documentType, "CV")) {
                    var cvDocuments = await Inisra.Solr.Operations.SolrOperations.CVQueryAsync(query);
                    return View("CVsResult", cvDocuments);
                }
                else { 
                    var jdDocuments = await Inisra.Solr.Operations.SolrOperations.JobDescriptionQueryAsync(query);
                    return View("JDsResult", jdDocuments);
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}