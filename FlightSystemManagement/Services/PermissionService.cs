using FlightSystemManagement.Data;
using FlightSystemManagement.DTO.Permission;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FlightSystemManagement.Services
{
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
                .Include(p => p.Document)          
                .Include(p => p.PermissionGroup)   
                .FirstOrDefaultAsync(p => p.DocumentID == documentId && p.PermissionGroupID == groupId);

            if (permission == null)
            {
                return null;
            }

            return new PermissionDto
            {
                PermissionID = permission.PermissionID,
                DocumentID = permission.DocumentID,
                PermissionGroupID = permission.PermissionGroupID,
                CanView = permission.CanView,
                CanEdit = permission.CanEdit,
                CanDownload = permission.CanDownload,
                NoPermission = permission.NoPermission
            };
        }

        // Get all permissions for a document
        public async Task<List<PermissionDto>> GetPermissionsByDocumentAsync(int documentId)
        {
            var permissions = await _context.Permissions
                .Where(p => p.DocumentID == documentId)
                .Include(p => p.Document)
                .Include(p => p.PermissionGroup)
                .ToListAsync();

            return permissions.Select(p => new PermissionDto
            {
                PermissionID = p.PermissionID,
                DocumentID = p.DocumentID,
                PermissionGroupID = p.PermissionGroupID,
                CanView = p.CanView,
                CanEdit = p.CanEdit,
                CanDownload = p.CanDownload,
                NoPermission = p.NoPermission
            }).ToList();
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

        public async Task<string> SavePermissionsAsync(PermissionDto model)
        {
            var document = await _context.Documents.FindAsync(model.DocumentID);
            if (document == null)
            {
                return "Document not found";
            }

            // Lưu quyền cho nhóm Pilot
            var pilotPermission = await _context.Permissions.FirstOrDefaultAsync(p => p.DocumentID == model.DocumentID && p.PermissionGroupID == 1); // 1 là Pilot
            if (pilotPermission == null)
            {
                pilotPermission = new Permission
                {
                    DocumentID = model.DocumentID,
                    PermissionGroupID = 1, // Pilot
                    CanView = model.CanView,
                    CanEdit = model.CanEdit,
                    NoPermission = model.NoPermission
                };
                _context.Permissions.Add(pilotPermission);
            }
            else
            {
                pilotPermission.CanView = model.CanView;
                pilotPermission.CanEdit = model.CanEdit;
                pilotPermission.NoPermission = model.NoPermission;
            }

            // Tương tự cho nhóm Crew
            var crewPermission = await _context.Permissions.FirstOrDefaultAsync(p => p.DocumentID == model.DocumentID && p.PermissionGroupID == 2); // 2 là Crew
            if (crewPermission == null)
            {
                crewPermission = new Permission
                {
                    DocumentID = model.DocumentID,
                    PermissionGroupID = 2, // Crew
                    CanView = model.CanView,
                    CanEdit = model.CanEdit,
                    NoPermission = model.NoPermission
                };
                _context.Permissions.Add(crewPermission);
            }
            else
            {
                crewPermission.CanView = model.CanView;
                crewPermission.CanEdit = model.CanEdit;
                crewPermission.NoPermission = model.NoPermission;
            }

            await _context.SaveChangesAsync();
            return "Permissions saved successfully";
        }
    }
}