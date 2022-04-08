using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrouwerService.Repositories;
using BrouwerService.Models;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;

namespace BrouwerService.Controllers
{
    [Route("brouwers")]
    [ApiController]
    public class BrouwerController : ControllerBase
    {
        private readonly IBrouwerRepository repository;
        public BrouwerController(IBrouwerRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        [SwaggerOperation("Alle brouwers")]
        public async Task<IActionResult> FindAll()
        {
            return base.Ok(await repository.FindAllAsync());
        }
        [HttpGet("{id}")]
        [SwaggerOperation("Brouwer waarvan je de id kent")]
        public async Task<IActionResult> FindById(int id)
        {
            var brouwer = await repository.FindByIdAsync(id);
            if (brouwer == null)
            {
                return base.NotFound();
            }
            return base.Ok(brouwer);
        }
        [HttpGet("naam")]
        [SwaggerOperation("Brouwers waarvan je het begin van de naam kent")]
        public async Task<IActionResult> FindByBeginNaam(string begin)
        {
            return base.Ok(await repository.FindByBeginNaamAsync(begin));
        }
        [HttpDelete("{id}")]
        [SwaggerOperation("Brouwer verwijderen")]
        public async Task<IActionResult> Delete(int id)
        {
            var brouwer = await repository .FindByIdAsync(id);
            if (brouwer == null)
            {
                return base.NotFound();
            }
            await repository .DeleteAsync(brouwer);
            return base.Ok();
        }
        [HttpPost]
        [SwaggerOperation("Brouwer toevoegen")]
        public async Task<IActionResult> Post(Brouwer brouwer)
        {
            if (this.ModelState.IsValid)
            {
                await repository.InsertAsync(brouwer);
                return base.CreatedAtAction(nameof(FindById),
                    new { id = brouwer.Id }, null);
            }
            return base.BadRequest(this.ModelState);
        }
        [HttpPut("{id}")]
        [SwaggerOperation("Brouwer wijzigen")]
        public async Task<IActionResult> Put(int id, Brouwer brouwer)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    brouwer.Id = id;
                    await repository.UpdateAsync(brouwer);
                    return base.Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return base.NotFound();
                }
                catch { return base.Problem(); }
            }
            return base.BadRequest(this.ModelState);
        }
        
    }
}
