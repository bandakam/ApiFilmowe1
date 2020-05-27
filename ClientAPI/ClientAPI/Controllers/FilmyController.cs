using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientAPI.Models;
using ClientAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientAPI.Controllers
{
    public class FilmyController : Controller
    {
        private IAPIService _service;
        public FilmyController(IAPIService service)
        {
            _service = service;
        }
        // GET: Filmy
        public async Task<ActionResult>  Index()
        {
            List<Film> filmy = new List<Film>();
            var response = await _service.Client.GetAsync("/api/Film");
            if (response.IsSuccessStatusCode)
            {
                var pobraneFilmy = response.Content.ReadAsStringAsync().Result;
                filmy = JsonConvert.DeserializeObject<List<Film>>(pobraneFilmy);
            }
            return View(filmy);
        }

        // GET: Filmy/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Film film = new Film();
            var response = await _service.Client.GetAsync($"/api/Film/{id}");
            if (response.IsSuccessStatusCode)
            {
                var pobranyFilm = response.Content.ReadAsStringAsync().Result;
                film = JsonConvert.DeserializeObject<Film>(pobranyFilm);
            }
            return View(film);
        }

        // GET: Filmy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filmy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Film film = new Film();
                film.Id = Convert.ToInt32(collection["id"].ToString());
                film.Nazwa = collection["Nazwa"].ToString();
                film.RokProdukcji = Convert.ToInt32(collection["RokProdukcji"].ToString());
                film.RezyserId = Convert.ToInt32(collection["RezyserId"].ToString());

                string jsonFilm = JsonConvert.SerializeObject(film, Formatting.Indented);
                var httpContent = new StringContent(jsonFilm, Encoding.UTF8, "application/json");
               
                var request = await _service.Client.PostAsync($"/api/Film/", httpContent);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Filmy/Edit/5
        public async  Task<ActionResult> Edit(int id)
        {
            Film film = new Film();
            var response = await _service.Client.GetAsync($"/api/Film/{id}");
            if (response.IsSuccessStatusCode)
            {
                var pobranyFilm = response.Content.ReadAsStringAsync().Result;
                film = JsonConvert.DeserializeObject<Film>(pobranyFilm);
            }
            return View(film);
        }

        // POST: Filmy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                Film film = new Film();
                film.Id = id;
                film.Nazwa = collection["Nazwa"].ToString();
                film.RokProdukcji = Convert.ToInt32(collection["RokProdukcji"].ToString());
                film.RezyserId = Convert.ToInt32(collection["RezyserId"].ToString());

                string jsonFilm = JsonConvert.SerializeObject(film, Formatting.Indented);
                var httpContent = new StringContent(jsonFilm, Encoding.UTF8, "application/json");

                var request = await _service.Client.PutAsync($"/api/Film/{id}", httpContent);

                return RedirectToAction(nameof(Index));
                                
            }
            catch
            {
                return View();
            }
        }

        // GET: Filmy/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Film film = new Film();
            var response = await _service.Client.GetAsync($"/api/Film/{id}");
            if (response.IsSuccessStatusCode)
            {
                var pobranyFilm = response.Content.ReadAsStringAsync().Result;
                film = JsonConvert.DeserializeObject<Film>(pobranyFilm);
            }
            return View(film);
        }

        // POST: Filmy/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var request = await _service.Client.DeleteAsync($"/api/Film/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}