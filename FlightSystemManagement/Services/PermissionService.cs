using FlightSystemManagement.Data;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightSystemManagement.Services;
public class PermissionService : IPermissionService
{
    private readonly FlightSystemContext _context;

    public PermissionService(FlightSystemContext context)
    {
        _context = context;
    }

    // Set permission for a document and group
    public async Task<Permission> SetPermissionAsync(PermissionCreateDto dto)
    {
        var permission = new Permission
        {
            DocumentID = dto.DocumentID,
            PermissionGroupID = dto.PermissionGroupID,
            CanView = dto.CanView,
            CanEdit = dto.CanEdit,
            CanDownload = dto.CanDownload
        };

        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    // Get permissions for a specific document and group
    public async Task<Permission> GetPermissionAsync(int documentId, int groupId)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.DocumentID == documentId && p.PermissionGroupID == groupId);
    }

    // Get all permissions for a document
    public async Task<List<Permission>> GetPermissionsByDocumentAsync(int documentId)
    {
        return await _context.Permissions
            .Where(p => p.DocumentID == documentId)
            .ToListAsync();
    }

    // Remove a permission for a document and group
    public async Task<bool> RemovePermissionAsync(int documentId, int groupId)
    {
        var permission = await _context.Permissions
            .FirstOrDefaultAsync(p => p.DocumentID == documentId && p.PermissionGroupID == groupId);
        if (permission == null)
            return false;

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();
        return true;
    }
}