using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.DTOs.WolfMailAddressRequests;
using WolfMail.Models.DataModels;
using WolfMail.Services;

namespace WolfMail.Controllers;

/// <summary>
/// Controller for handling API requests related to wolf mail address.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class WolfMailAddressesController : ControllerBase
{
    private readonly WolfMailContext _context;

    /// <summary>
    /// Constructor for <see cref="WolfMailAddress"/>
    /// </summary>
    /// <param name="context"></param>
    public WolfMailAddressesController(WolfMailContext context) => _context = context;

    /// <summary>
    /// Retrieve the mail address in the given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/WolfMailAddresses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WolfMailAddress>> GetWolfMailAddress(string id)
    {
        if (_context.MailAddresses is null)
            return NotFound();

        var wolfMailAddress = await _context.MailAddresses.FindAsync(id);

        if (wolfMailAddress is null)
            return NotFound();

        return wolfMailAddress;
    }

    /// <summary>
    /// Updates the mail address in the given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    // PUT: api/WolfMailAddresses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWolfMailAddress(
        string id,
        WolfMailAddressUpdateRequest request
    )
    {
        if (_context.MailAddresses is null)
            return NotFound();

        var wolfMailAddress = await _context.MailAddresses.FindAsync(id);

        if (wolfMailAddress is null)
            return NotFound();

        request.Apply(ref wolfMailAddress);

        _context.Entry(wolfMailAddress).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!WolfMailAddressExists(id))
                return NotFound();
            else
                throw;
        }

        return Ok();
    }

    /// <summary>
    /// Create a new wolf mail address
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    // POST: api/WolfMailAddresses
    [HttpPost]
    public async Task<ActionResult<WolfMailAddress>> PostWolfMailAddress(
        WolfMailAddressCreateRequest request
    )
    {
        if (_context.MailAddresses is null)
            return Problem("Entity set 'WolfMailContext.MailAddresses'  is null.");

        WolfMailAddress wolfMailAddress =
            new()
            {
                Id = IdGeneratorService.GetUniqueId(),
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                UserId = request.UserId,
            };

        _context.MailAddresses.Add(wolfMailAddress);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (WolfMailAddressExists(wolfMailAddress.Id))
                return Conflict();
            else
                throw;
        }

        return CreatedAtAction(
            "GetWolfMailAddress",
            new { id = wolfMailAddress.Id },
            wolfMailAddress
        );
    }

    /// <summary>
    /// Removes the user given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/WolfMailAddresses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWolfMailAddress(string id)
    {
        if (_context.MailAddresses is null)
            return NotFound();
        var wolfMailAddress = await _context.MailAddresses.FindAsync(id);
        if (wolfMailAddress is null)
            return NotFound();

        _context.MailAddresses.Remove(wolfMailAddress);
        await _context.SaveChangesAsync();

        return Ok();
    }

    private bool WolfMailAddressExists(string id) =>
        (_context.MailAddresses?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
}
