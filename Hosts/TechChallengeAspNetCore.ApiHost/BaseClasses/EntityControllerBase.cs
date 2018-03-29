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
    public abstract class EntityControllerBase<TKey, T, TRepository> : ApiControllerBase
        where T : class, IEntityBase<TKey>, ISearchableName
        where TRepository : class, IDataRepositoryBase<TKey, T>
    {
        protected readonly TRepository repository;
        public EntityControllerBase(IMediator mediator, TRepository repository)
            : base(mediator)
        {
            this.repository = repository;
        }

        #region IDataRepositoryBase
        /// <summary>
        /// Override this if needs to handle softdeletes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        protected virtual async Task<T> DeleteItem(TKey id, string reason = "")
        {
            return await repository.DeleteAsync(id);
        }

        /// <summary>
        /// Override this if needs to handle parentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="search"></param>
        /// <param name="pagerNumber"></param>
        /// <returns></returns>
        protected virtual async Task<IEnumerable<T>> GetAll(int? parentId, string search = "", int pagerNumber = 1)
        {
            return await repository.GetAsync(r => r.OrderBy(x => x.SearchableName), pagerNumber);
        }

        /// <summary>
        /// Override this if needs to handle parentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        protected virtual async Task<List<string>> GetSuggestions(int? parentId, string search = "")
        {
            return await repository
                .GetAutoCompleteIntellisenseAsync(r => search == "" || r.SearchableName.Contains(search),
                    r => r.OrderBy(s => s.SearchableName),
                    r => r.SearchableName);
        }

        /// <summary>
        /// Override this if needs to handle finalization logic before Saving.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        protected virtual async Task UpdateAsync(T item, bool autoSave = true) => await repository.UpdateAsync(item, autoSave);

        /// <summary>
        /// Override this if needs to handle finalization logic before Saving.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        protected virtual async Task AddAsync(T item, bool autoSave = true) => await repository.AddAsync(item, autoSave);

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<T>> Get(int? parentId, string search = "", int? pagerNumber = 1)
        {
            return await GetAll(parentId, search, pagerNumber.Value);
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
        public async Task<IActionResult> Delete([FromRoute] TKey id, string reason = "")
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await DeleteItem(id, reason);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Suggestions(int? parentId, string search = "")
        {
            var suggestions = await GetSuggestions(parentId, search);

            return Ok(suggestions);
        }
        #endregion // IDataRepositoryBase

        protected override void RegisterIDisposable(List<IDisposable> disposables)
        {
            disposables.Add(repository);
        }
    }
}

