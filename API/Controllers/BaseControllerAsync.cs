﻿using API.Interfaces;
using Core.Interfaces;
using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseControllerAsync<T> : ControllerBase, IAsyncRestBase<T> where T : class
{
    protected readonly IAsyncRepositoryBase<T> _repository;

    public BaseControllerAsync(IAsyncRepositoryBase<T> repository)
    {
        _repository = repository;
    }

    [HttpDelete]
    public virtual async Task<ActionResult<bool>> Delete(T entity, CancellationToken token) =>
        ModelState.IsValid ? Ok(await _repository.DeleteAsync(entity, token)) : BadRequest(ModelState.ValidationState);

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> Get([FromQuery] Pagination pag, CancellationToken token) =>
        ModelState.IsValid ? Ok(await _repository.ListAsync(pag, token)) : BadRequest(ModelState.ValidationState);

    [HttpPatch]
    public virtual async Task<ActionResult<bool>> Patch(T entity, CancellationToken token) =>
        ModelState.IsValid ? Ok(await _repository.UpdateAsync(entity, token)) : BadRequest(ModelState.ValidationState);

    [HttpPost]
    public virtual async Task<ActionResult<T>> Post(T entity, CancellationToken token) =>
        ModelState.IsValid ? Ok(await _repository.AddAsync(entity, token)) : BadRequest(ModelState.ValidationState);
}
