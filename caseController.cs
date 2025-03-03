using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CasesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CasesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/cases (Retrieve all cases)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Case>>> GetCases()
    {
        return await _context.Cases.ToListAsync();
    }

    // GET: api/cases/whatsapp (Retrieve WhatsApp-specific cases)
    [HttpGet("whatsapp")]
    public async Task<ActionResult<IEnumerable<Case>>> GetWhatsAppCases()
    {
        return await _context.Cases.Where(c => c.Channel == "WhatsApp").ToListAsync();
    }

    // GET: api/cases/5 (Retrieve a specific case by ID)
    [HttpGet("{id}")]
    public async Task<ActionResult<Case>> GetCase(int id)
    {
        var caseItem = await _context.Cases.FindAsync(id);
        if (caseItem == null)
        {
            return NotFound();
        }

        return caseItem;
    }

    // POST: api/cases (Create a new case)
    [HttpPost]
    public async Task<ActionResult<Case>> CreateCase(Case caseItem)
    {
        _context.Cases.Add(caseItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCase), new { id = caseItem.Id }, caseItem);
    }

    // POST: api/cases/whatsapp (Restrict WhatsApp team to adding their own cases only)
    [HttpPost("whatsapp")]
    public async Task<ActionResult<Case>> CreateWhatsAppCase(Case caseItem)
    {
        if (caseItem.Channel != "WhatsApp")
        {
            return BadRequest("Only WhatsApp cases can be added through this endpoint.");
        }

        _context.Cases.Add(caseItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCase), new { id = caseItem.Id }, caseItem);
    }

    // PUT: api/cases/5 (Update a case)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCase(int id, Case caseItem)
    {
        if (id != caseItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(caseItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Cases.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // PUT: api/cases/whatsapp/5 (Restrict WhatsApp team to updating only their own cases)
    [HttpPut("whatsapp/{id}")]
    public async Task<IActionResult> UpdateWhatsAppCase(int id, Case caseItem)
    {
        if (id != caseItem.Id || caseItem.Channel != "WhatsApp")
        {
            return BadRequest("WhatsApp team can only update their own cases.");
        }

        _context.Entry(caseItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/cases/5 (Delete a case)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCase(int id)
    {
        var caseItem = await _context.Cases.FindAsync(id);
        if (caseItem == null)
        {
            return NotFound();
        }

        _context.Cases.Remove(caseItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/cases/whatsapp/5 (Restrict WhatsApp team to deleting only their own cases)
    [HttpDelete("whatsapp/{id}")]
    public async Task<IActionResult> DeleteWhatsAppCase(int id)
    {
        var caseItem = await _context.Cases.FindAsync(id);
        if (caseItem == null || caseItem.Channel != "WhatsApp")
        {
            return NotFound("WhatsApp team can only delete their own cases.");
        }

        _context.Cases.Remove(caseItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}