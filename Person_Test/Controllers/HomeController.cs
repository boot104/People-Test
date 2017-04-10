using Person_Test.Data;
using Person_Test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Person_Test.Controllers
{
    public class HomeController : Controller
    {

        private PersonContext db = new PersonContext();

        public ActionResult Index(string status)
        {
            ViewBag.Status = status;
            return View(db.Persons.ToList());
        }

        [HttpPost]
        public ActionResult GetPerson(string id)
        {
            if (String.IsNullOrEmpty(id) == false)
            {
                int id_ = Convert.ToInt32(id.Replace("cb", ""));
                //var pd = new ModelView(id_);
                //if (pd.Person != null)
                //{
                //    return PartialView("EditPerson", pd);
                //}
                var person = db.Persons.Where(p => p.PersonId == id_).FirstOrDefault();
                IEnumerable<SelectListItem> items = from d in db.States
                                                    select new SelectListItem
                                                    {
                                                        Text = d.StateName,
                                                        Value = d.StateId.ToString(),
                                                        Selected = d.StateId == person.StateId

                                                    };
                ViewBag.States = from d in db.States
                                 select new SelectListItem
                                 {
                                     Text = d.StateName,
                                     Value = d.StateId.ToString(),
                                     Selected = d.StateId == person.StateId

                                 };
                //new SelectList( db.States, "StateId", "StateName", db.States.Where(s => s.StateId == person.StateId).FirstOrDefault().StateName);
                ViewBag.Cities = from d in db.Cities
                                 select new SelectListItem
                                 {
                                     Text = d.CityName,
                                     Value = d.CityId.ToString(),
                                     Selected = d.CityId == person.CityId

                                 };
                return PartialView("EditPerson", person);
            }

            return Content("");
        }

        [HttpPost]
        public ActionResult Save(FormCollection form)
        {
            string message = String.Empty;
            try
            {
                int personId = Convert.ToInt32(form["PersonId"]);
                string firstName = form["FirstName"].Trim();
                string lastName = form["LastName"].Trim();
                DateTime dob = Convert.ToDateTime(form["DOB"]);
                int cityId = Convert.ToInt32(form["Cities"]);
                int stateId = Convert.ToInt32(form["States"]);
                var person = db.Persons.SingleOrDefault(p => p.PersonId == personId);

                if (person != null)
                {
                    bool modified = false;
                    foreach (PropertyInfo pi in typeof(Person).GetProperties())
                    {
                        if (pi.Name == "CityId")
                        {
                            int cityId_ = Convert.ToInt32(pi.GetValue(person));
                            if (cityId_ != Convert.ToInt32(form["Cities"]))
                            {
                                var stateExists = db.Cities.Where(c => c.CityId == cityId && c.StateId == stateId).Any();
                                if (stateExists)
                                {
                                    pi.SetValue(person, Convert.ToInt32(form["Cities"]), null);
                                    modified = true;
                                }
                                else
                                {
                                    message = "Unable to save. No city records for this state";
                                    modified = false;
                                    db.Entry(person).State = EntityState.Unchanged;
                                    break;
                                }
                            }
                        }
                        else if (pi.Name == "StateId")
                        {
                            if (Convert.ToInt32(pi.GetValue(person)) != Convert.ToInt32(form["States"]))
                            {
                                pi.SetValue(person, Convert.ToInt32(form["States"]), null);
                                modified = true;
                            }
                        }
                        else if (form.AllKeys.Contains(pi.Name))
                        {
                            Type pt = pi.PropertyType;
                            if (pt == typeof(String))
                            {
                                if (pi.GetValue(person).ToString() != form[pi.Name])
                                {
                                    pi.SetValue(person, form[pi.Name], null);
                                    modified = true;
                                }
                            }
                            else if (pt == typeof(DateTime))
                            {
                                if (Convert.ToDateTime(pi.GetValue(person)) != Convert.ToDateTime(form[pi.Name]))
                                {
                                    pi.SetValue(person, Convert.ToDateTime(form[pi.Name]), null);
                                    modified = true;
                                }
                            }
                            else if (pt == typeof(Int32))
                            {
                                if (Convert.ToInt32(pi.GetValue(person)) != Convert.ToInt32(form[pi.Name]))
                                {
                                    pi.SetValue(person, Convert.ToInt32(form[pi.Name]), null);
                                    modified = true;
                                }
                            }
                        }
                    }
                    if (modified == true)
                    {
                        db.Entry(person).State = EntityState.Modified;
                        db.SaveChanges();
                        if (String.IsNullOrEmpty(message) == true)
                        {
                            message = "RECORD SAVED";
                        }
                    }
                }
            }
            catch { message = "Unable to save. Data is not in correct format"; }
            return RedirectToAction("Index", new { status = message });
        }
    }
}