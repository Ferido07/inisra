using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Inisra_Web_App_MVC.Models;
using Inisra_Web_App_MVC.BLL;
using Inisra_Web_App_MVC.DTOs;

namespace Inisra_Web_App_MVC.Controllers
{
    public class CompaniesController : Controller
    {
        private CompanyBLL comBLL = new CompanyBLL();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            var companydtos = await comBLL.GetCompaniesAsync();
            return View(companydtos);
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var company = await comBLL.GetCompanyByIdAsync(((int)id));
            if (company == null)
            {
                return HttpNotFound();
            }
            var dto = AutoMapper.Mapper.Map<Company, CompanyDto>(company);
            return View(dto);
        }
        /*
        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Email,PhoneNo,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(company);
        }
        */




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                comBLL.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
