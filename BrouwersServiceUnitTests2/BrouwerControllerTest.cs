using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrouwerService.Controllers;
using BrouwerService.Models;
using BrouwerService.Repositories;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BrouwersServiceUnitTests2
{
    [TestClass]
    public class BrouwerControllerTest
    {
        private BrouwerController controller;
        private Mock<IBrouwerRepository> mock;
        private Brouwer brouwer7, brouwer9;

        [TestInitialize]
        public void Init()
        {
            mock = new Mock<IBrouwerRepository>();
            var repository = mock.Object;
            controller = new BrouwerController(repository);
            brouwer7 = new Brouwer { Id = 7, Naam = "7", Postcode = 7000, Gemeente = "7" };
            brouwer9 = new Brouwer { Id = 9, Naam = "9", Postcode = 9000, Gemeente = "9" };
        }
        [TestMethod]
        public void DeleteGeeftNotFoundBijOnbestaaneBrouwer()
        {
            Assert.IsInstanceOfType(controller.Delete(-1).Result,
                typeof(NotFoundResult));
            mock.Verify(repo => repo.DeleteAsync(It.IsAny<Brouwer>()),
                Times.Never);
        }
        [TestMethod]
        public void DeleteVerwijdertBestaandeBrouwer()
        {
            mock.Setup(repo => repo.FindByIdAsync(7)).Returns(Task.FromResult(brouwer7));
            Assert.IsInstanceOfType(controller.Delete(7).Result, typeof(OkResult));
            mock.Verify(repo => repo.DeleteAsync(brouwer7));
        }
        [TestMethod]
        public void FindbyIdGeeftNotFoundBijOnbestaandeBrouwer()
        {
            Assert.IsInstanceOfType(controller.FindById(7).Result, typeof(NotFoundResult));
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindByIdGeeftBestaandeBrouwer()
        {
            mock.Setup(repo => repo.FindByIdAsync(7)).Returns(Task.FromResult(brouwer7));
            var response = (OkObjectResult)controller.FindById(7).Result;
            var brouwer = (Brouwer)response.Value;
            Assert.AreEqual(7, brouwer.Id);
            mock.Verify(repo => repo.FindByIdAsync(7));
        }
        [TestMethod]
        public void FindAllGeeftAlleBrouwers()
        {
            var alleBrouwers = new List<Brouwer>() { brouwer7, brouwer9 };
            mock.Setup(repo => repo.FindAllAsync()).Returns(Task.FromResult(alleBrouwers));
            var response = (OkObjectResult)controller.FindAll().Result;
            var brouwers = (List<Brouwer>)response.Value;
            Assert.AreEqual(2, brouwers.Count);
            Assert.AreEqual(7, brouwers[0].Id);
            Assert.AreEqual(9, brouwers[1].Id);
            mock.Verify(repo => repo.FindAllAsync());
        }
        [TestMethod]
        public void FindByBeginNaamGeeftJuisteBrouwers()
        {
            var brouwersMetBeginNaam7 = new List<Brouwer> { brouwer7 };
            mock.Setup(repo => repo.FindByBeginNaamAsync("7")).Returns(Task.FromResult(brouwersMetBeginNaam7));
            var response = (OkObjectResult)controller.FindByBeginNaam("7").Result;
            var brouwers = (List<Brouwer>)response.Value;
            Assert.AreEqual(1, brouwers.Count);
            Assert.AreEqual(7, brouwers[0].Id);
            mock.Verify(repo => repo.FindByBeginNaamAsync("7"));
        }
        [TestMethod]
        public void PostVoegtBrouwerToe()
        {
            var nieuweBrouwer = new Brouwer() { Naam = "N", Postcode = 100, Gemeente = "N" };
            var response = (CreatedAtActionResult)controller.Post(nieuweBrouwer).Result;
            mock.Verify(repo => repo.InsertAsync(nieuweBrouwer));
        }
        [TestMethod]
        public void PutGeeftNotFoundBijOnbestaandeBrouwer()
        {
            mock.Setup(repo => repo.UpdateAsync(brouwer7)).Throws(new DbUpdateConcurrencyException());
            Assert.IsInstanceOfType(controller.Put(7, brouwer7).Result, typeof(NotFoundResult));
            mock.Verify(repo => repo.UpdateAsync(brouwer7));
        }
        [TestMethod]
        public void PutWijzigtBestaandeBrouwer()
        {
            Assert.IsInstanceOfType(controller.Put(7, brouwer7).Result, typeof(OkResult));
            mock.Verify(repo => repo.UpdateAsync(brouwer7));
        }
    }
}
