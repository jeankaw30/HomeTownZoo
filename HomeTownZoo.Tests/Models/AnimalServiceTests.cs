using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeTownZoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace HomeTownZoo.Models.Tests
{
    [TestClass()]
    public class AnimalServiceTests
    {
        private IQueryable<Animal> animals;

        [TestInitialize] // runs before each test
        public void BeforeTest()
        {
            animals = new List<Animal>()
            {
                new Animal() { AnimalID = 1, AnimalName ="Zebra"},
                new Animal()  {AnimalID = 2, AnimalName = "Bat"}
            }.AsQueryable();
        }

        [TestMethod] // #1
        public void GetAnimals_ShouldReturnAllAnimalsSortedByAnimalName()
        {
            // set up Mock Database and mock animal table

            // Moq = package  and Mock = class
            // Top of NUGet Package Maanager console -> Select HometownZoo.Tests in default project drop box 
            // In console :  PM > Install-Package EntityFramework 
            // then lightbulb select using System.Data.Entity;
            //  Mock<DbSet<Animal>> mockAnimals = new Mock<DbSet<Animal>>();     // change var to use explicit data type in lightbulb to avaoid double variables
            // var mockAnimals = new Mock<DbSet<Animal>>();

            // CREATE MOCK ANIMALS FOR  COLLECTION OF FAKE ANIMALS

            Mock<DbSet<Animal>> mockAnimals = new Mock<DbSet<Animal>>();

            // options to query fake animals :
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Provider).Returns(animals.Provider);
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Expression).Returns(animals.Expression);
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.GetEnumerator()).Returns(animals.GetEnumerator()); // iterate over a collection

            //CREATE MOCK DATABASE FOR MOCK ANIMALS
            var mockDB = new Mock<ApplicationDbContext>();

            mockDB.Setup(db => db.Animals).Returns(mockAnimals.Object);

            // ACT 
            IEnumerable<Animal> allAnimals = AnimalService.GetAnimals(mockDB.Object);

            // ASSERT all animals are returned
            Assert.AreEqual(2, allAnimals.Count());

            // ASSERT Animals are sorted by name in ascending order (default)
            // bat then zebra is correct order  
            Assert.AreEqual("Bat", allAnimals.ElementAt(0).AnimalName);     // Animal name at element 0 in object
            Assert.AreEqual("Zebra", allAnimals.ElementAt(1).AnimalName);   // or allAnimals.Last since only 2 elements
            // ALTERNATELY with created variable
            // Animal bat = allAnimals.ElementsAt(0); -- or--
            // Animal bat = allAnimals.First() to return first element of a sequence
            // Assert.AreEqual("Bat", bat.AnimalName );
        }
    }
}