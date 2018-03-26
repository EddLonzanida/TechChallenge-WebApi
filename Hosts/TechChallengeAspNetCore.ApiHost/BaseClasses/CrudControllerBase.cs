using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eml.Contracts.Entities;
using Eml.Contracts.Repositories.Bases;
using Eml.Mediator.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TechChallengeAspNetCore.ApiHost.BaseClasses
{
   public abstract class CrudControllerBase<TKey, T> : ApiControllerBase
        where T : class, IEntityBase<TKey>, ISearchableName
    {
        protected readonly IDataRepositoryBase<TKey, T> repository;

        protected CrudControllerBase(IMediator mediator, IDataRepositoryBase<TKey, T> repository)
            : base(mediator)
        {
            this.repository = repository;
        }

        protected virtual async Task<T> DeleteItem(TKey id)
        {
            return await repository.DeleteAsync(id);
        }

        protected virtual async Task<IEnumerable<T>> GetAll(int? pagerNumber = 1)
        {
            return await repository.GetAsync(r => r.OrderBy(x => x.SearchableName), pagerNumber.Value);
        }

        protected virtual async Task UpdateAsync(T item, bool autoSave = true) => await repository.UpdateAsync(item, autoSave);

        protected virtual async Task AddAsync(T item, bool autoSave = true) => await repository.AddAsync(item, autoSave);

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<T>> Get(int? pagerNumber = 1)
        {
            return await GetAll(pagerNumber);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] TKey id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await repository.GetAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] TKey id, [FromBody] T item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (item.Id.Equals(id)) return BadRequest();

            try
            {
                await UpdateAsync(item, true);
            }
            catch (DbUpdateConcurrencyException)
            {
                var isItemExists = await repository.ExistsAsync(id);

                if (!isItemExists) return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await AddAsync(item);

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] TKey id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await DeleteItem(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(repository);
        }
    }
}

