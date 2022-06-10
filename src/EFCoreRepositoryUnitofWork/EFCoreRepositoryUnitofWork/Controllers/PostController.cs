﻿using EFCoreRepositoryUnitofWork.Entities;
using EFCoreRepositoryUnitofWork.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PublicApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class PostController : ControllerBase
{
    private readonly ICustomPostRepository _postCustomRepository;

    public PostController(ICustomPostRepository postCustomRepository)
    {
        _postCustomRepository = postCustomRepository;
    }

    [HttpGet]
    public ActionResult<Post> Get()
    {
        var results = _postCustomRepository.GetAll();

        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _postCustomRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }

    [HttpGet("{id:int}/comments")]
    public async Task<IActionResult> GetWithComments(int id)
    {
        var entity = await _postCustomRepository.GetPostWithCommentsAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }

    [HttpGet("{id:int}/WithMoreComments")]
    public async Task<IActionResult> GetPostWithMoreComments(int id)
    {
        var entity = await _postCustomRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Post([FromForm] Post entity)
    {
        if (entity is null)
            return BadRequest(ModelState);

        _postCustomRepository.Add(entity);

        var result = await _postCustomRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest("Your changes have no[t been saved.");

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] Post entity)
    {
        if (entity is null)
            return BadRequest(ModelState);

        if (id != entity.Id)
            return BadRequest("Identifier is not valid or Identifiers don't match.");

        var existEntity = await _postCustomRepository.GetByIdAsync(id);

        if (existEntity is null)
            return NotFound($"Entity with Id = {id} not found.");

        var result = await _postCustomRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest("Your changes have not been saved.");

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest(ModelState);

        var existEntity = await _postCustomRepository.GetByIdAsync(id);
        if (existEntity is null)
            return NotFound($"Entity with Id = {id} not found");

        patchDoc.ApplyTo(existEntity, ModelState);

        var isValid = TryValidateModel(existEntity);
        if (!isValid)
            return BadRequest(ModelState);

        try
        {
            await _postCustomRepository.UnitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _postCustomRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found");

        _postCustomRepository.Delete(entity);

        var result = await _postCustomRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest();

        return NoContent();
    }
}
