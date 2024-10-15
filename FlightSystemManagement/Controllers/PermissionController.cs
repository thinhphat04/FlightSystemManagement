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
        try
        {
            var permission = await _permissionService.SetPermissionAsync(dto);
            return CreatedAtAction(nameof(GetPermission), new { documentId = permission.DocumentID, groupId = permission.PermissionGroupID }, permission);
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần
            return StatusCode(500, new { message = "An error occurred while setting permission.", details = ex.Message });
        }
    }

    // Get permission for a document and group
    [HttpGet("{documentId}/{groupId}")]
    public async Task<IActionResult> GetPermission(int documentId, int groupId)
    {
        try
        {
            var permission = await _permissionService.GetPermissionAsync(documentId, groupId);
            if (permission == null)
                return NotFound();
            return Ok(permission);
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần
            return StatusCode(500, new { message = "An error occurred while retrieving permission.", details = ex.Message });
        }
    }

    // Get all permissions for a document
    [HttpGet("document/{documentId}")]
    public async Task<IActionResult> GetPermissionsByDocument(int documentId)
    {
        try
        {
            var permissions = await _permissionService.GetPermissionsByDocumentAsync(documentId);
            return Ok(permissions);
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần
            return StatusCode(500, new { message = "An error occurred while retrieving permissions for the document.", details = ex.Message });
        }
    }

    // Remove permission for a document and group
    [HttpDelete("{documentId}/{groupId}")]
    public async Task<IActionResult> RemovePermission(int documentId, int groupId)
    {
        try
        {
            var success = await _permissionService.RemovePermissionAsync(documentId, groupId);
            if (!success)
                return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            // Ghi log lỗi nếu cần
            return StatusCode(500, new { message = "An error occurred while removing permission.", details = ex.Message });
        }
    }
}