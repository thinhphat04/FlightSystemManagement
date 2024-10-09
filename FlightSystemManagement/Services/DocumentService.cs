using FlightSystemManagement.Data;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightSystemManagement.DTO;

namespace FlightSystemManagement.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightSystemContext _context;

        public DocumentService(FlightSystemContext context)
        {
            _context = context;
        }

        // Thêm tài liệu vào chuyến bay
        public async Task<Document> CreateDocumentAsync(int flightId, Document document, List<int> permissionGroupIds)
        {
            // Kiểm tra trạng thái chuyến bay trước khi thêm tài liệu (đã có logic này ở trên)
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null || flight.IsFlightCompleted)
            {
                throw new Exception("Chuyến bay đã kết thúc hoặc không tồn tại.");
            }

            // Tạo tài liệu cho chuyến bay
            var flightDocument = new FlightDocument
            {
                FlightID = flightId,
                Document = document,
                CreatedDate = DateTime.Now
            };

            _context.FlightDocuments.Add(flightDocument);

            // Cập nhật quyền truy cập tài liệu cho các nhóm quyền
            foreach (var groupId in permissionGroupIds)
            {
                var permissionGroupAssignment = new PermissionGroupAssignment
                {
                    PermissionGroupID = groupId,
                    RoleID = document.UploadedBy // Hoặc sử dụng Role nếu có
                };
                _context.PermissionGroupAssignments.Add(permissionGroupAssignment);
            }

            await _context.SaveChangesAsync();

            return document;
        }
        
        public async Task<Document> UpdateDocumentAsync(int documentId, Document updatedDocument)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
            {
                throw new Exception("Document not found");
            }

            // Tăng version dựa trên phiên bản hiện tại
            var versionParts = document.Version.Split('.');
            int majorVersion = int.Parse(versionParts[0]);
            int minorVersion = int.Parse(versionParts[1]);
            minorVersion++; // Tăng phần nhỏ của version

            // Cập nhật version mới
            document.Version = $"{majorVersion}.{minorVersion}";

            // Cập nhật các trường khác nếu cần thiết
            document.DocumentName = updatedDocument.DocumentName;
            document.Note = updatedDocument.Note;
            document.UploadedBy = updatedDocument.UploadedBy;

            _context.Documents.Update(document);
            await _context.SaveChangesAsync();

            return document;
        }

}
    
    
}