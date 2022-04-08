using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using BrouwerService.Repositories;
using BrouwerService.Models;
using BrouwerService.Controllers;
using BrouwerService.DTOs;
using Moq;
using Microsoft.AspNetCore.TestHost;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BrouwersServiceIntegrationTests
{
    [TestClass]
    public class BrouwerControllerTest
    {
        private HttpClient client;
        private Mock<IBrouwerRepository> mock;
        private Brouwer brouwer1;
        [TestInitialize]
        public void init()
        {
            mock = new Mock<IBrouwerRepository>();
            var repo = mock.Object;
            var factory = new WebApplicationFactory<BrouwerService.Startup>();
            client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(
                services => services.AddScoped<IBrouwerRepository>(_ => repo))).CreateClient();
            brouwer1 = new Brouwer { Id = 1, Naam = "1", Postcode = 1000, Gemeente = "1" };
        }
        /*
        public void init()
        {
            var factory = new WebApplicationFactory<BrouwerService.Startup>();
            client = factory.CreateDefaultClient();
        }*/
        [TestMethod]
        public void DeleteMetBestaandeBrouwerGeeftOK()
        {
            mock.Setup(repo => repo.FindByIdAsync(1)).Returns(Task.FromResult(brouwer1));
            var response = client.DeleteAsync("brouwers/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(repo => repo.DeleteAsync(brouwer1));
            /*
            var response = client.DeleteAsync("brouwers/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            */
        }
        [TestMethod]
        public void DeleteMetOnbestaandeBrouwerGeeftNotFound()
        {
            var response = client.DeleteAsync("brouwers/-1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.DeleteAsync(It.IsAny<Brouwer>()), Times.Never);
        }
        [TestMethod]
        public void GetMetOnbestaandeBrouwerGeeftNotFound()
        {
            var response = client.GetAsync("brouwers/-1").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(-1));
        }
        [TestMethod]
        public void GetMetBestaandeBrouwerGeeftOK()
        {
            mock.Setup(repo => repo.FindByIdAsync(1)).Returns(Task.FromResult(brouwer1));
            var response = client.GetAsync("brouwers/1").Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            mock.Verify(repo => repo.FindByIdAsync(1));
            var body = response.Content.ReadAsStringAsync().Result;
            var document = JsonDocument.Parse(body);
            Assert.AreEqual(1, document.RootElement.GetProperty("id").GetInt32());
            Assert.AreEqual("1", document.RootElement.GetProperty("naam").GetString());
        }
        [TestMethod]
        public void PostMetCorrecteBrouwerGeeftCreated()
        {
            var response = client.PostAsJsonAsync("brouwers", brouwer1).Result;
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            mock.Verify(repo => repo.InsertAsync(It.Is<Brouwer>(brouwer => brouwer.Id == 1)));
            mock.Verify(repo => repo.InsertAsync(It.Is<Brouwer>(brouwer => brouwer.Naam == "1")));
        }
        [TestMethod]
        public void PostMetVerkeerdeBrouwerGeeftbadRequest()
        {
            var response = client.PostAsJsonAsync("brouwers", new Brouwer() { Id = 1, Naam = "1", Postcode = -1, Gemeente = "1" }).Result;
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            mock.Verify(repo => repo.InsertAsync(It.IsAny<Brouwer>()), Times.Never);
        }
    }
}
