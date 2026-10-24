using LHPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace LHPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    public static readonly List<Cliente> Clientes = new();
    private static int _proximoId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> Get()
    {
        return Ok(Clientes);
    }

    [HttpPost]
    public ActionResult<Cliente> Post([FromBody] Cliente cliente)
    {
        cliente.Id = _proximoId++;
        Clientes.Add(cliente);
        return Created($"api/clientes/{cliente.Id}", cliente);
    }
}