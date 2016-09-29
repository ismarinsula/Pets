using Newtonsoft.Json;
using PetsSorted.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PetsSorted.Controllers
{
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {     
            //get the list of owners and thier pets      
            List<PetOwner> model = null;
            var client = new HttpClient();
            var task = client.GetAsync("http://agl-developer-test.azurewebsites.net/people.json")
              .ContinueWith((taskwithresponse) =>
              {
                  var response = taskwithresponse.Result;
                  var jsonString = response.Content.ReadAsStringAsync();
                  jsonString.Wait();
                  model = JsonConvert.DeserializeObject<List<PetOwner>>(jsonString.Result);

              });
            task.Wait();

            //group by owner type
            var gpm = model.GroupBy(x => x.Gender)
                .Select(grp => new { Gender = grp.Key, Pets = grp.Select(pts => pts.Pets).ToList() }).Where(x => x != null)
                .ToList();

            //var groupedOwnersList = model.GroupBy(po => po.Gender)
            //                                .Select(grp => new { Gender = grp.Key, Pets = grp.Select(pts => pts.Pets.GroupBy(pn => pn.Name).Select(grp2 => new { Pets = grp2.Key}).ToList()).Where(x => x != null) })
            //                                .ToList();

            
            //this is a bit of a hack as I could not figure out how to aggregate Pets while grouping by owner gender using LINQ
            List<PetOwnerGender> pogs = new List<PetOwnerGender>();
            foreach (var gol in gpm)
            {
                var orderedPets = new List<Pet>();

                var pog = new PetOwnerGender
                    {
                        Gender = gol.Gender
                    };

                foreach (var pets in gol.Pets)
                {
                    if (pets != null)
                    {
                        foreach (var pet in pets)
                        {
                            orderedPets.Add(pet);
                        }
                    }
                }

                pog.Pets = orderedPets.OrderBy(pn => pn.Name).ToList();
                pogs.Add(pog);
            }


            return View("PetOwners", pogs);
        }

        
    }
}
