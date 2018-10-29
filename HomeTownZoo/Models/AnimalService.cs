using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeTownZoo.Models
{
    public static class AnimalService
    {
        /// <summary>
        /// Returns all Animals by Name sorted in Ascending order from database
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IEnumerable<Animal> GetAnimals(ApplicationDbContext db)
        {
            //query sntax
            return (from a in db.Animals
                    orderby a.AnimalName
                    select a).ToList();

            // method syntax
            //return db.Animals
            //        .OrderBy(a => a.AnimalName)
            //        .ToList();
        }

        // add a animaal
        public static void AddAnimal(Animal a, ApplicationDbContext db)
        {
            if(a is null)
            {
                throw new ArgumentNullException($"Parameter {nameof(a)} cannot be null");
            }

            // TO DO: Ensure duplicate names are disallowed


            db.Animals.Add(a);
            db.SaveChanges(); // saves changes

        }

    }
}