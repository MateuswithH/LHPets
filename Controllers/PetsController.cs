using LHPets.Models;
using Microsoft.AspNetCore.Mvc;

namespace LHPets.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private static readonly List<Pet> Pets = new();
    private static int _proximoId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Pet>> Get()
    {
        return Ok(Pets);
    }

    [HttpPost]
    public ActionResult<Pet> Post([FromBody] Pet pet)
    {
        bool clienteExiste = ClientesController.Clientes.Any(c => c.Id == pet.ClienteId);

        if (!clienteExiste)
        {
            return BadRequest($"Não existe cliente com Id {pet.ClienteId}.");
        }

        pet.Id = _proximoId++;
        Pets.Add(pet);
        return Created($"api/pets/{pet.Id}", pet);
    }

    [HttpGet("cliente/{clienteId}")]
    public ActionResult<IEnumerable<Pet>> GetPorCliente(int clienteId)
    {
        var petsDoCliente = Pets.Where(p => p.ClienteId == clienteId).ToList();
        return Ok(petsDoCliente);
    }
}