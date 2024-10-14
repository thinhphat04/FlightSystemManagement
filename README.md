# Flight Document Management System (FDMS)

<p align="center">
  <img src="Docs/Logo-Alta-Software.png" alt="ALTA SOFTWARE" width="250"/>
</p>

## Giới thiệu

Flight Document Management System (FDMS) là hệ thống quản lý tài liệu cho các chuyến bay. Hệ thống cung cấp các chức năng quản lý người dùng, quản lý chuyến bay, quản lý tài liệu và phân quyền truy cập, nhằm hỗ trợ quá trình vận hành và chuẩn bị cho các chuyến bay.

## Các chức năng chính

### 1. Quản lý Người dùng và Phân quyền
- **Xác thực người dùng**: Người dùng phải đăng nhập qua email công ty (@vietjetair.com).
- **Phân quyền người dùng**: Quản lý quyền hạn theo các nhóm người dùng: Admin, Phi công, Tiếp viên, và Nhân viên Back-Office. Mỗi nhóm có quyền hạn khác nhau:
  - **Admin**: Toàn quyền quản lý hệ thống.
  - **Nhân viên Back-Office**: Tạo, cập nhật, và tải lên tài liệu.
  - **Phi công/Tiếp viên**: Chỉ xem và tải xuống các tài liệu liên quan.

### 2. Quản lý Tài liệu Chuyến bay
- **Tạo và cập nhật tài liệu**: Nhân viên Back-Office có thể tạo mới và cập nhật các tài liệu như kế hoạch bay, hướng dẫn an toàn, báo cáo vận hành.
- **Phân quyền truy cập tài liệu**: Các tài liệu được quản lý thông qua phân quyền truy cập. Chỉ người dùng có quyền mới được xem, chỉnh sửa, và tải xuống tài liệu.
- **Tải xuống tài liệu**: Phi công và tiếp viên có thể tải xuống các tài liệu cần thiết để chuẩn bị cho chuyến bay.

### 3. Quản lý Chuyến bay
- **Quản lý thông tin chuyến bay**: Quản lý số hiệu chuyến bay, ngày khởi hành, nơi đi và nơi đến.
- **Phân công phi hành đoàn**: Phân công phi công và tiếp viên vào các chuyến bay cụ thể, liên kết với các tài liệu liên quan.

## Các thành phần chính

### Bảng dữ liệu
- **Flights**: Lưu trữ thông tin về các chuyến bay.
- **Documents**: Lưu trữ thông tin về tài liệu, bao gồm loại tài liệu, người tạo, đường dẫn file, và ngày tạo.
- **FlightDocuments**: Liên kết các tài liệu với chuyến bay cụ thể.
- **Permissions**: Quản lý quyền truy cập tài liệu (xem, tạo, cập nhật, xóa) dựa trên vai trò người dùng.
- **PermissionGroups**: Quản lý nhóm quyền như Admin, Phi công, và Tiếp viên.
- **FlightCrew**: Lưu trữ thông tin phân công phi hành đoàn cho các chuyến bay.

## Yêu cầu hệ thống
- Hệ thống xác thực người dùng dựa trên email công ty và phân quyền qua bảng Roles và UserRoles.
- Tài liệu cần được phân loại và liên kết với chuyến bay, quản lý qua bảng Documents và FlightDocuments.
- Phi hành đoàn có thể truy cập tài liệu theo thời gian thực thông qua các hệ thống liên kết qua API.

## Các quy trình nghiệp vụ
1. Người dùng truy cập hệ thống và đăng nhập.
2. Hệ thống xác thực và phân quyền người dùng dựa trên vai trò.
3. Nhân viên Back-Office tạo hoặc cập nhật tài liệu liên quan đến chuyến bay.
4. Phi hành đoàn truy cập và xem tài liệu được phân công thông qua hệ thống.

## Sơ đồ quan hệ thực thể (ERD)
Hệ thống sử dụng các bảng chính như Users, Roles, Documents, Flights, FlightDocuments, và Permissions để quản lý dữ liệu và phân quyền người dùng.
