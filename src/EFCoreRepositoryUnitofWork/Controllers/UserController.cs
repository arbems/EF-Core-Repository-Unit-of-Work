using EFCoreRepositoryUnitofWork.Entities;
using EFCoreRepositoryUnitofWork.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepositoryUnitofWork.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private readonly IReadRepository<User> _userReadRepository;

    public UserController(IReadRepository<User> userReadRepository)
    {
        _userReadRepository = userReadRepository;
    }

    [HttpGet]
    public ActionResult<Post> Get()
    {
        var results = _userReadRepository.GetAll();

        return Ok(results);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _userReadRepository.GetByIdAsync(id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }

    [HttpGet("{id:int}/posts")]
    public async Task<IActionResult> GetUserWithPosts(int id)
    {
        var entity = await _userReadRepository.GetAllIncluding(a => a.Posts).FirstOrDefaultAsync(a => a.Id == id);

        if (entity is null)
            return NotFound($"Entity with Id = {id} not found.");

        return Ok(entity);
    }
}
