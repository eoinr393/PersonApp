using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PersonWebApp.Models;

namespace PersonWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private IHttpClientFactory _clientFactory;

        public List<PersonModel> _people = new List<PersonModel>();

        public string searchName { get; set; }

        public List<PersonModel> People()
        {
            List<PersonModel> people = new List<PersonModel>();

            return people;
        }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/person");

            var client = _clientFactory.CreateClient("personApp");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _people = await System.Text.Json.JsonSerializer.DeserializeAsync<List<PersonModel>>(responseStream);
            }
            else
            {
                _people = new List<PersonModel>();
            }

            client.Dispose();
        }

        public void OnPostSearch(int id)
        {
            using (var client = _clientFactory.CreateClient("personApp"))
            {
                var response = client.GetAsync($"api/person/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseStream = response.Content.ReadAsStringAsync().Result;
                    
                    PersonModel searchPerson = JsonConvert.DeserializeObject<PersonModel>(responseStream);

                    this.searchName = searchPerson.firstName + " " + searchPerson.lastName;

                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");

                }
            }
        }

        public ActionResult OnPostAdd(string firstName, string lastName)
        {
            PersonModel person = new PersonModel() { firstName = firstName, lastName = lastName };

            using (var client = _clientFactory.CreateClient("personApp"))
            {
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8,"application/json");

                var response = client.PostAsync("/api/person", contentPost).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");

                }
            }

            return RedirectToPage("./Index");
        }

        public ActionResult OnPostUpdate(int id, string firstName, string lastName)
        {
            PersonModel person = new PersonModel() { id=id, firstName = firstName, lastName = lastName };           

            string test = JsonConvert.SerializeObject(person);

            using (var client = _clientFactory.CreateClient("personApp"))
            {
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");

                var response = client.PutAsync("/api/person", contentPost).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");
                }
            }

            return RedirectToPage("./Index");
        }

        public ActionResult OnPostDelete(int id)
        {
            using (var client = _clientFactory.CreateClient("personApp"))
            {
                var response = client.DeleteAsync($"api/person/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");

                }
            }

            return RedirectToPage("./Index");
        }
    }
}
