using FlightSystemManagement.Data;
using FlightSystemManagement.DTO.Permission;
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
    public async Task<PermissionDto> GetPermissionAsync(int documentId, int groupId)
    {
        var permission = await _context.Permissions
            .Include(p => p.Document)          // Bao gồm thông tin tài liệu nếu cần thiết
            .Include(p => p.PermissionGroup)   // Bao gồm thông tin nhóm quyền nếu cần thiết
            .FirstOrDefaultAsync(p => p.DocumentID == documentId && p.PermissionGroupID == groupId);

        // Kiểm tra nếu không tìm thấy Permission
        if (permission == null)
        {
            return null; // Hoặc bạn có thể xử lý logic khác ở phía trên
        }

        // Chuyển đổi sang DTO trước khi trả về
        var permissionDto = new PermissionDto
        {
            PermissionID = permission.PermissionID,
            DocumentID = permission.DocumentID,
            PermissionGroupID = permission.PermissionGroupID,
            CanView = permission.CanView,
            CanEdit = permission.CanEdit,
            CanDownload = permission.CanDownload
        };

        return permissionDto;
    }

    // Get all permissions for a document
    public async Task<List<PermissionDto>> GetPermissionsByDocumentAsync(int documentId)
    {
        var permissions = await _context.Permissions
            .Where(p => p.DocumentID == documentId)
            .Include(p => p.Document)          // Bao gồm Document liên quan
            .Include(p => p.PermissionGroup)   // Bao gồm PermissionGroup liên quan
            .ToListAsync();

        // Chuyển đổi sang DTO trước khi trả về
        var permissionDtos = permissions.Select(p => new PermissionDto
        {
            PermissionID = p.PermissionID,
            DocumentID = p.DocumentID,
            PermissionGroupID = p.PermissionGroupID,
            CanView = p.CanView,
            CanEdit = p.CanEdit,
            CanDownload = p.CanDownload
        }).ToList();

        return permissionDtos;
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