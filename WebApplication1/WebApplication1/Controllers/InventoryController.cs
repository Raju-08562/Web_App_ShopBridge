using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;
using System.Configuration;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        string URL = ConfigurationManager.AppSettings.Get("URL");

        // GET: Inventory
        public ActionResult Index()
        {
            //  string URL = ConfigurationManager.AppSettings.Get("URL");
            Uri uri = new Uri(URL);
            HttpClient httpClient = new HttpClient();
            IEnumerable<ViewModelinventory> vmi;
            httpClient.BaseAddress = uri;


            var Response = httpClient.GetAsync("SB");
            Response.Wait();
            var result = Response.Result;
            if (result.IsSuccessStatusCode)
            {
                var job = result.Content.ReadAsAsync<IEnumerable<ViewModelinventory>>();
                job.Wait();
                vmi = job.Result;
            }
            else
            {
                vmi = Enumerable.Empty<ViewModelinventory>();
                ModelState.AddModelError("", "not Valid");
            }
            return View(vmi);


        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ViewModelinventory modelinventory)
        {

            Uri uri = new Uri(URL);

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = uri;
                var response = httpClient.PostAsJsonAsync("SB", modelinventory);
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "nothing");
                }
            }
            return View(modelinventory);
        }

        public ActionResult Edit(int id)
        {
            Uri uri = new Uri(URL);
            ViewModelinventory vmi;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                var response = client.GetAsync("SB/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var job = result.Content.ReadAsAsync<ViewModelinventory>();
                    job.Wait();
                    vmi = job.Result;
                    return View(vmi);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ViewModelinventory viewModelinventory)
        {
            Uri uri = new Uri(URL);
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                var response = client.PutAsJsonAsync("SB", viewModelinventory);
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError("", "failed to edit");
                }

            }
            return View(viewModelinventory);
        }

        //[HttpDelete]
        public ActionResult Delete(int id)
        {
            Uri uri = new Uri(URL);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = uri;
                var response = client.DeleteAsync("SB/" + id.ToString());
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");

            }
        }
    }
}