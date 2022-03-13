﻿using API.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ControllerAsync<T, Search> : BaseControllerAsync<T, Search>, IAsyncRest<T> where T : Identifiable where Search : Pagination
{
    protected IAsyncRepository<T> Repository => (IAsyncRepository<T>)_repository;

    public ControllerAsync(IAsyncRepository<T> repo) : base(repo)
    {
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetAsync(int id, CancellationToken token = default) =>
        ModelState.IsValid ? Ok(await Repository.GetAsync(id, token)) : BadRequest(ModelState.ValidationState);

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<bool>> DeleteAsync(int id, CancellationToken token = default) =>
        ModelState.IsValid ? Ok(await Repository.DeleteAsync(id, token)) : BadRequest(ModelState.ValidationState);
}
