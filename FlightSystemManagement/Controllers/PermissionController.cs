using FlightSystemManagement.DTO;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlightSystemManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // Set a new permission for a document and group
    [HttpPost]
    public async Task<IActionResult> SetPermission(PermissionCreateDto dto)
    {
        var permission = await _permissionService.SetPermissionAsync(dto);
        return CreatedAtAction(nameof(GetPermission), new { documentId = permission.DocumentID, groupId = permission.PermissionGroupID }, permission);
    }

    // Get permission for a document and group
    [HttpGet("{documentId}/{groupId}")]
    public async Task<IActionResult> GetPermission(int documentId, int groupId)
    {
        var permission = await _permissionService.GetPermissionAsync(documentId, groupId);
        if (permission == null)
            return NotFound();
        return Ok(permission);
    }

    // Get all permissions for a document
    [HttpGet("document/{documentId}")]
    public async Task<IActionResult> GetPermissionsByDocument(int documentId)
    {
        var permissions = await _permissionService.GetPermissionsByDocumentAsync(documentId);
        return Ok(permissions);
    }

    // Remove permission for a document and group
    [HttpDelete("{documentId}/{groupId}")]
    public async Task<IActionResult> RemovePermission(int documentId, int groupId)
    {
        var success = await _permissionService.RemovePermissionAsync(documentId, groupId);
        if (!success)
            return NotFound();
        return NoContent();
    }
}