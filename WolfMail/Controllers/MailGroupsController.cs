using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WolfMail.Data;
using WolfMail.DTOs.MailGroup;
using WolfMail.DTOs.MailGroupRequests;
using WolfMail.Extensions;
using WolfMail.Interfaces.Providers;
using WolfMail.Interfaces.Services;
using WolfMail.Models.DataModels;
using WolfMail.Services;

namespace WolfMail.Controllers;

/// <summary>
/// Controller for handling API requests related to mail groups.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class MailGroupsController : ControllerBase
{
    private readonly WolfMailContext _context;
    private readonly IRequestUserProvider _userProvider;
    private readonly IMailGroupService _service;

    /// <summary>
    /// Constructor for MailGroupsController
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="userProvider">The request user provider</param>
    /// <param name="service">The mail group service</param>
    public MailGroupsController(
        WolfMailContext context,
        IRequestUserProvider userProvider,
        IMailGroupService service
    )
    {
        _context = context;
        _userProvider = userProvider;
        _service = service;
    }

    /// <summary>
    /// Retrieves all mail groups
    /// </summary>
    /// <returns>A collection of mail groups</returns>
    // GET: api/MailGroups
    [HttpGet]
    public async Task<ActionResult<ICollection<MailGroup>>> GetMailGroups()
    {
        string userId = _userProvider.GetUserInfo()!.Id;
        HashSet<MailGroup>? mailGroups = await _service.GetMailGroups(userId);

        if (mailGroups is null)
            return NotFound();

        return this.SerializeIntoContent(mailGroups);
    }

    /// <summary>
    /// Retrieves a mail group by ID
    /// </summary>
    /// <param name="id">The ID of the mail group to retrieve</param>
    /// <returns>The mail group with the specified ID</returns>
    // GET: api/MailGroups/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MailGroup>> GetMailGroup(string id)
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        if (_context.MailGroups == null)
            return NotFound();

        var mailGroup = await _service.GetMailGroup(userId, id);

        if (mailGroup == null)
            return NotFound();

        return this.SerializeIntoContent(mailGroup);
    }

    /// <summary>
    /// Updates a mail group by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request">The request</param>
    /// <returns>An IActionResult indicating the success or failure of the operation</returns>
    // PUT: api/MailGroups/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMailGroup(string id, MailGroupUpdateRequest request)
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        await _service.UpdateMailGroup(
            userId,
            id,
            new MailGroup()
            {
                Name = request.Name,
                Mails = request.Mails ?? new HashSet<WolfMailAddress>()
            }
        );

        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Creates a new mail group
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns>The newly created mail group object</returns>
    // POST: api/MailGroups
    [HttpPost]
    public async Task<ActionResult<MailGroup>> PostMailGroup(MailGroupCreateRequest request)
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        if (_context.MailGroups == null)
            return Problem("Entity set 'WolfMailContext.MailGroups'  is null.");

        var mailGroup = await _service.AddMailGroup(
            userId,
            new MailGroup()
            {
                Id = IdGeneratorService.GetUniqueId(),
                Name = request.Name,
                Mails =
                    request.Mails
                        ?.Select(
                            m =>
                                new WolfMailAddress()
                                {
                                    Id = IdGeneratorService.GetUniqueId(),
                                    Name = m.Name,
                                    Email = m.Email,
                                    Password = m.Password,
                                    UserId = m.UserId,
                                }
                        )
                        .ToHashSet() ?? new()
            }
        );
        await _context.SaveChangesAsync();
        return this.SerializeIntoContent(mailGroup);
    }

    /// <summary>
    /// Adds an email address to a mail group
    /// </summary>
    /// <param name="request">The Request</param>
    /// <returns>The updated mail group object</returns>
    // DELETE: api/MailGroups/MailAddressees
    [HttpPost("AddMailAddress")]
    public async Task<ActionResult<bool>> AddMailToMailGroup(MailGroupAddMailRequest request)
    {
        string userId = _userProvider.GetUserInfo()!.Id;
        var wolfMail = await _context.MailAddresses.FirstOrDefaultAsync(
            m => m.Id.Equals(request.MailAddressId)
        );

        if (wolfMail is null)
            return NotFound("Email not found!");

        bool result = await _service.AddMail(userId, request.MailGroupId, wolfMail);

        return result;
    }

    /// <summary>
    /// Removes an email address from a mail group
    /// </summary>
    /// <param name="mailGroupId">The ID of the mail group to remove from</param>
    /// <param name="wolfMailAddressId">The ID of the email address to remove</param>
    /// <returns>The updated mail group object</returns>
    // DELETE: api/MailGroups/MailAddressees
    [HttpDelete("DeleteMailAddress")]
    public async Task<ActionResult<bool>> RemoveMailToMailGroup(
        string mailGroupId,
        string wolfMailAddressId
    )
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        bool result = await _service.RemoveMail(userId, mailGroupId, wolfMailAddressId);

        return result;
    }

    /// <summary>
    /// Deletes a mail group by ID
    /// </summary>
    /// <param name="id">The ID of the mail group to delete</param>
    /// <returns>An IActionResult indicating the success or failure of the operation</returns>
    // DELETE: api/MailGroups/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMailGroup(string id)
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        if (_context.MailGroups == null)
            return NotFound();

        await _service.DeleteMailGroup(userId, id);

        await _context.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> MailGroupExists(string id)
    {
        string userId = _userProvider.GetUserInfo()!.Id;

        return await _service.MailGroupExists(userId, id);
    }
}
