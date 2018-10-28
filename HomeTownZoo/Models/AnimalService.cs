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

    }
}