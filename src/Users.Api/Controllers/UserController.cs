using Microsoft.AspNetCore.Mvc;
using Users.Application.Interfaces;
using static Users.Application.Dtos.UserDtos;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _service;


    public UsersController(IUserService service) => _service = service;


    /// <summary>
    /// Crea un usuario
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var id = await _service.CreateAsync(request, ct);
        return Ok(new CreateUserResponse(id));
    }


    /// <summary>
    /// Lista a los usuarios creados
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("list")]
    public async Task<ActionResult<IReadOnlyList<UserItem>>> List([FromBody] UserListRequest request, CancellationToken ct)
    {
        var result = await _service.ListAsync(request, ct);
        return Ok(result);
    }


     /// <summary>
     /// Obtiene un usuario
     /// </summary>
     /// <param name="request"></param>
     /// <param name="ct"></param>
     /// <returns></returns>
    [HttpPost("get")]
    public async Task<ActionResult<UserItem?>> Get([FromBody] GetUserRequest request, CancellationToken ct)
    {
        var result = await _service.GetAsync(request, ct);
        if (result is null) return NotFound();
        return Ok(result);
    }


    /// <summary>
    /// Actualiza un usuario
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<ActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        var ok = await _service.UpdateAsync(request, ct);
        return ok ? Ok() : NotFound();
    }


   /// <summary>
   /// Elimina (logicamente) un usuario
   /// </summary>
   /// <param name="request"></param>
   /// <param name="ct"></param>
   /// <returns></returns>
    [HttpPost("delete")]
    public async Task<ActionResult> Delete([FromBody] DeleteUserRequest request, CancellationToken ct)
    {
        var ok = await _service.DeleteAsync(request, ct);
        return ok ? Ok() : NotFound();
    }
}