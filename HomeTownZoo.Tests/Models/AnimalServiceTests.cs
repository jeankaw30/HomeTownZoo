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
                new Animal() {AnimalID = 1, AnimalName = "Zebra"},
                new Animal() {AnimalID = 2, AnimalName = "Bat"}
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
            // Mock<DbSet<Animal>> mockAnimals = new Mock<DbSet<Animal>>();     
            // change var to use explicit data type in lightbulb to avaoid double variables
            // var mockAnimals = new Mock<DbSet<Animal>>();

            // CREATE MOCK ANIMALS FOR  COLLECTION OF FAKE ANIMALS 

            // var mockAnimals = new Mock<DbSet<Animal>>();
            // options to query fake animals :
            // mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Provider).Returns(animals.Provider);
            // mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Expression).Returns(animals.Expression);
            // mockAnimals.As<IQueryable<Animal>>().Setup(m => m.GetEnumerator()).Returns(animals.GetEnumerator());

            // ARANGE   
            // var mockDB = GetMockDB(mockAnimals);
            // Mock<DbSet<Animal>> mockAnimals = GetMockAnimalsDbSet();// iterate over a collection
            // REFACTORED = 
            Mock<ApplicationDbContext> mockDB = GetMockDBWithAnimals();

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

        private Mock<ApplicationDbContext> GetMockDBWithAnimals()
        {
            Mock<DbSet<Animal>> mockAnimals = GetMockAnimalsDbSet();// iterate over a collection

            //Gets Nock DB method call dbset mockAnimals
            var mockDB = GetMockDB(mockAnimals);
            return mockDB;
        }

        [TestMethod] //#2
        public void AddAnimal_NewAnimalShouldCall_AddAnimal_And_SaveChanges()
        {

            // ARRANGE REFACTORED TO extract method from a variable originally
            Mock<DbSet<Animal>> mockAnimals = GetMockAnimalsDbSet();
            Mock<ApplicationDbContext> mockDb = GetMockDB(mockAnimals);
            // WITH VARIABLE 
            //var mockDb = new Mock<ApplicationDbContext>();
            //mockDb.Setup(db => db.Animals).Returns(mockAnimals.Object);

            Animal a = new Animal() { AnimalName = "Elephant" };

            // ACT
            AnimalService.AddAnimal(a, mockDb.Object);

            // ASSERT
            mockAnimals.Verify(m => m.Add(a), Times.Once);
            mockDb.Verify(m => m.SaveChanges(), Times.Once);

        }
        // reactored method created from mock db variable 
        private static Mock<ApplicationDbContext> GetMockDB(Mock<DbSet<Animal>> mockAnimals)
        {
            var mockDb = new Mock<ApplicationDbContext>();
            mockDb.Setup(db => db.Animals).Returns(mockAnimals.Object);
            return mockDb;
        }


        [TestMethod] // #3 when animal passed in is null     
        public void AddAnimal_NullAnimal_ShouldThrowArgumentException()
        {
            Animal a = null;
            var mockDb = GetMockDBWithAnimals();

            // ASSERT => ACT
            Assert.ThrowsException<ArgumentNullException>(() => AnimalService.AddAnimal(a, mockDb.Object));
        }
        // created method when refactored extract method lightbulb used
        private Mock<DbSet<Animal>> GetMockAnimalsDbSet()
        {
            var mockAnimals = new Mock<DbSet<Animal>>();
            // options to query fake animals :
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Provider).Returns(animals.Provider);
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.Expression).Returns(animals.Expression);
            mockAnimals.As<IQueryable<Animal>>().Setup(m => m.GetEnumerator()).Returns(animals.GetEnumerator());
            return mockAnimals;
        }
    }
}