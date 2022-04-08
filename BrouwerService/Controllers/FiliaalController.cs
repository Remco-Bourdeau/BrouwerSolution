using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using BrouwerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using BrouwerService.Models;
using BrouwerService.DTOs;

namespace BrouwerService.Controllers
{
    [Route("filialen")]
    [ApiController]
    public class FiliaalController : ControllerBase
    {
        private readonly IFiliaalRepository repository;
        public FiliaalController(IFiliaalRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        [SwaggerOperation("Alle filialen")]
        public async Task<IActionResult> FindAll()
        {
            var filialen = await repository.FindAllAsync();
            var filiaalDTOs = filialen.Select(filiaal => new FiliaalDTO
            {
                Id = filiaal.Id,
                Naam = filiaal.Naam,
                Postcode = filiaal.Woonplaats.Postcode,
                Woonplaats = filiaal.Woonplaats.Naam
            });
            return base.Ok(filiaalDTOs);
        }
    }
}
