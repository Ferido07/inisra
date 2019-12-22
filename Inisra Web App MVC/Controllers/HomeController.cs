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
        public async Task<ActionResult> Index(string query, string documentType)
        {

            if (string.Equals(documentType, "CV"))
            {
                if (string.IsNullOrEmpty(query))
                    return View("CVsResult");
                var cvDocuments = await Inisra.Solr.Operations.SolrOperations.CVQueryAsync(query);
                return View("CVsResult", cvDocuments);
            }
            else if (string.Equals(documentType, "JD"))
            {
                if (string.IsNullOrEmpty(query))
                    return View("JDsResult");
                var jdDocuments = await Inisra.Solr.Operations.SolrOperations.JobDescriptionQueryAsync(query);
                return View("JDsResult", jdDocuments);
            }
            else
                return View();
            
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