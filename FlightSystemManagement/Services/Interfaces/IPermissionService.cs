using FlightSystemManagement.DTO;
using FlightSystemManagement.DTO.Permission;
using FlightSystemManagement.Entity;

namespace FlightSystemManagement.Services.Interfaces;

public interface IPermissionService
{
    Task<Permission> SetPermissionAsync(PermissionCreateDto dto);
    Task<PermissionDto> GetPermissionAsync(int documentId, int groupId);
    Task<List<PermissionDto>> GetPermissionsByDocumentAsync(int documentId);
    Task<bool> RemovePermissionAsync(int documentId, int groupId);
    Task<string> SavePermissionsAsync(PermissionDto model);
}