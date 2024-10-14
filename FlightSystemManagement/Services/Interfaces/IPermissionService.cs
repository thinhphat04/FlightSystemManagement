using FlightSystemManagement.DTO;
using FlightSystemManagement.Entity;

namespace FlightSystemManagement.Services.Interfaces;

public interface IPermissionService
{
    Task<Permission> SetPermissionAsync(PermissionCreateDto dto);
    Task<Permission> GetPermissionAsync(int documentId, int groupId);
    Task<List<Permission>> GetPermissionsByDocumentAsync(int documentId);
    Task<bool> RemovePermissionAsync(int documentId, int groupId);
}