namespace FlightSystemManagement.DTO;

public class PagedResultDto<T>
{
    public int TotalRecords { get; set; } // Tổng số bản ghi
    public int PageNumber { get; set; }   // Số trang hiện tại
    public int PageSize { get; set; }     // Kích thước trang
    public List<T> Data { get; set; }     // Danh sách dữ liệu trên trang hiện tại

    public PagedResultDto(int totalRecords, int pageNumber, int pageSize, List<T> data)
    {
        TotalRecords = totalRecords;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
    }
}
