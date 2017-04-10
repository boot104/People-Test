using Person_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Test.Data
{
    public class PersonContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PersonContext>
    {
        protected override void Seed(PersonContext context)
        {
            var states = new List<State>
            {
                new State() { StateId = 1, StateName ="MI"},
                new State() { StateId = 2, StateName ="NY"},
                new State() { StateId = 3, StateName ="PA"},
            };
            states.ForEach(s => context.States.Add(s));
            context.SaveChanges();
            var cities = new List<City>
            {
                new City() { CityId = 1, StateId = 1, CityName = "Detroit"},
                new City() { CityId = 2, StateId = 1, CityName = "Flint"},
                new City() { CityId = 3, StateId = 2, CityName = "New York"},
            };
            cities.ForEach(c => context.Cities.Add(c));
            context.SaveChanges();
            var persons = new List<Person>
            {
                    new Person() {PersonId = 1, FirstName = "John", LastName = "Reed", DOB = new DateTime(1980,3,6), StateId = 1, CityId = 1 },
                    new Person() {PersonId = 1, FirstName = "James", LastName = "Larst", DOB = new DateTime(1970,8,3), StateId = 1, CityId = 2 },
                    new Person() {PersonId = 1, FirstName = "Ted", LastName = "Brown", DOB = new DateTime(1976,5,4), StateId = 2, CityId = 3 },
            };
            persons.ForEach(p => context.Persons.Add(p));
            context.SaveChanges();
        }
    }
}