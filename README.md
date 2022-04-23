# Clothes Store

Dự án trang web bán hàng được viết bằng ASP.NET Core + Angular + Entity Framewwork

## Tính năng

- Quản lý sản phẩm, loại sản phẩm
- Quản lý đơn hàng, tracking đơn hàng
- Quản lý user, đăng ký đăng nhập
- Phân vùng User - Admin & Seller

## Run

### Server side

Bước 1: Set lại biến `ClothingDatabase` tại file `./Server/ClothingStore/appsettings.json` phù hợp với database của máy.

Bước 2: Chạy file database `./script.sql` tại SQL Server Management Studio (SSMS) để khởi tạo database và thêm dữ liệu.

Bước 3: Mở file sln `./Server/ClothingStore.sln` để chạy project trên Visual Studio

Bước 4: Add migrations : 
 - `enable-migrations`
 - `add-migration "init"`
 - `update-database` 

Bước 5: Nhấn `Start` để chạy project server

### Client side

Bước 1: Mở CMD tại thư mục `./Client`

Bước 2: Tại cmd gõ `npm install` & nhấn ENTER để cài các package của project

Bước 3: Sau khi cài đặt xong ở bước 2, tại cmd gõ `ng serve` để chạy project

Bước 4(nếu thay đổi port ở server): chỉnh sửa file `./Client/src/environments/environment.ts` với ` urlApi ` đã thay đổi port

## Account
Tài khoản trong hệ thống:
