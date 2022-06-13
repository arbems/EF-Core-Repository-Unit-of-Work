using EFCoreRepositoryUnitofWork.Entities;
using EFCoreRepositoryUnitofWork.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitofWork.Controllers;

[ApiController]
[Route("[Controller]")]
public class CommentController : ControllerBase
{
    private readonly IRepository<Comment> _commentRepository;

    public CommentController(IRepository<Comment> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public ActionResult<Comment> Get()
    {
        var results = _commentRepository.GetAll();

        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _commentRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> Comment([FromForm] Comment entity)
    {
        if (entity is null)
            return BadRequest(ModelState);

        _commentRepository.Add(entity);

        var result = await _commentRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest("Your changes have no[t been saved.");

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] Comment entity)
    {
        if (entity is null)
            return BadRequest(ModelState);

        if (id != entity.Id)
            return BadRequest("Identifier is not valid or Identifiers don't match.");

        var existEntity = await _commentRepository.GetByIdAsync(id);

        if (existEntity is null)
            return NotFound($"Entity with Id = {id} not found.");

        var result = await _commentRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest("Your changes have not been saved.");

        return NoContent();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest(ModelState);

        var existEntity = await _commentRepository.GetByIdAsync(id);
        if (existEntity is null)
            return NotFound($"Entity with Id = {id} not found");

        patchDoc.ApplyTo(existEntity, ModelState);

        var isValid = TryValidateModel(existEntity);
        if (!isValid)
            return BadRequest(ModelState);

        try
        {
            await _commentRepository.UnitOfWork.SaveChangesAsync();
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
        var entity = await _commentRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found");

        _commentRepository.Delete(entity);

        var result = await _commentRepository.UnitOfWork.SaveChangesAsync();
        if (result <= 0)
            return BadRequest();

        return NoContent();
    }
}
