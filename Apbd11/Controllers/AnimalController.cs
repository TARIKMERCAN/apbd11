namespace Apbd11.Controllers;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Apbd11.Context;
using Apbd11.Models;

[Route("api/[controller]")]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly AnimalClinicContext _context;
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(AnimalClinicContext context, ILogger<AnimalController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Animals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals(string queryBy = "Name")
    {
        _logger.LogInformation("GetAnimals called with queryBy: {queryBy}", queryBy);
        return await _context.Animals
            .OrderBy(a => queryBy == "Description" ? a.Description : a.Name)
            .ToListAsync();
    }

    // GET: api/Animals/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        _logger.LogInformation("GetAnimal called with id: {id}", id);
        var animal = await _context.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    // POST: api/Animals
    [HttpPost]
    public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
    {
        var animalType = await _context.AnimalTypes.FirstOrDefaultAsync(at => at.Name == animal.AnimalTypes.Name);
        if (animalType == null)
        {
            return BadRequest();
        }

        animal.AnimalTypesId = animalType.Id;
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
        _logger.LogInformation("PostAnimal called with animal: {animal}", animal);
        return CreatedAtAction("GetAnimal", new { id = animal.Id }, animal);
    }

    // PUT: api/Animals/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAnimal(int id, Animal animal)
    {
        if (id != animal.Id)
        {
            return BadRequest();
        }

        _context.Entry(animal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnimalExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        _logger.LogInformation("PutAnimal called with id: {id}, animal: {animal}", id, animal);

        return NoContent();
    }

    // DELETE: api/Animals/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        var animal = await _context.Animals.FindAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        _context.Animals.Remove(animal);
        await _context.SaveChangesAsync();
        _logger.LogInformation("DeleteAnimal called with id: {id}", id);
        return NoContent();
    }

    private bool AnimalExists(int id)
    {
        return _context.Animals.Any(e => e.Id == id);
    }
}