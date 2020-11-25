using APP_Demo__WebAPI_.Models;
using APP_Demo__WebAPI_.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_API.Controllers
{
    [Route("[controller]")]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly MyDbContext db;
        private readonly DataDotGov dataDotGov = new DataDotGov();
        private static int currentPage;
        private Metadata latestRecord;

        public HomeController(MyDbContext myDbContext)
        {
            db = myDbContext;
        }

        public IActionResult Index()
        {
            var lastRecord = db.DrugInfos.Where(d => d.PageNumber > 0).OrderBy(p => p).LastOrDefault();

            currentPage = lastRecord == null ? 0 : lastRecord.PageNumber;
            
            ViewBag.LastPageStored = currentPage;

            latestRecord = db.Metadatas.Where(m => m.Id > 0).OrderBy(r=> r.Id).LastOrDefault();

            ViewBag.TotalPages = latestRecord == null ? 0: latestRecord.total_pages;
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> MakeRequestToApi(string nextPage)
        {

            if (!string.IsNullOrEmpty(nextPage))
            {
                var apiData = await dataDotGov.GetDrugNames(currentPage + 1);
                
                if(apiData != null)
                {
                    
                    foreach(var data in apiData.data)
                    {
                        data.PageNumber = currentPage + 1;
                        await db.DrugInfos.AddAsync(data);
                    }

                    await db.Metadatas.AddAsync(apiData.metadata);
                   
                    await db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }

   
}
