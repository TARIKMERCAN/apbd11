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
public class VisitController : ControllerBase
{
    private readonly AnimalClinicContext _context;
    private readonly ILogger<VisitController> _logger;

    public VisitController(AnimalClinicContext context, ILogger<VisitController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/Visits
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VisitDto>>> GetVisits()
    {
        _logger.LogInformation("Getting all visits");
        return await _context.Visits
            .OrderBy(v => v.Date)
            .Select(v => new VisitDto
            {
                Id = v.Id,
                EmployeeName = v.Employee.Name,
                AnimalName = v.Animal.Name,
                Date = v.Date
            })
            .ToListAsync();
    }

    // GET: api/Visits/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Visit>> GetVisit(int id)
    {
        var visit = await _context.Visits.FindAsync(id);

        if (visit == null)
        {
            return NotFound();
        }
        
        _logger.LogInformation($"Getting visit with id {id}");

        return new Visit
        {
            Id = visit.Id,
            EmployeeId = visit.EmployeeId,
            AnimalId = visit.AnimalId,
            Date = visit.Date
        };
    }

    // POST: api/Visits
    [HttpPost]
    public async Task<ActionResult<Visit>> PostVisit(Visit visit)
    {
        var employee = await _context.Employees.FindAsync(visit.EmployeeId);
        var animal = await _context.Animals.FindAsync(visit.AnimalId);

        if (employee == null || animal == null)
        {
            return BadRequest();
        }

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Adding visit with id {visit.Id}");
        
        return CreatedAtAction("GetVisit", new { id = visit.Id }, visit);
    }

    // PUT: api/Visits/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVisit(int id, Visit visit)
    {
        if (id != visit.Id)
        {
            return BadRequest();
        }

        var existingVisit = await _context.Visits.FindAsync(id);
        if (existingVisit == null)
        {
            return NotFound();
        }

        existingVisit.Date = visit.Date;
        _context.Entry(existingVisit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VisitExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
        _logger.LogInformation($"Updating visit with id {id}");

        return NoContent();
    }

    // DELETE: api/Visits/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVisit(int id)
    {
        var visit = await _context.Visits.FindAsync(id);
        if (visit == null)
        {
            return NotFound();
        }

        _context.Visits.Remove(visit);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation($"Deleting visit with id {id}");
        
        return NoContent();
    }

    private bool VisitExists(int id)
    {
        return _context.Visits.Any(e => e.Id == id);
    }
}

public class VisitDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string AnimalName { get; set; }
    public string Date { get; set; }
}