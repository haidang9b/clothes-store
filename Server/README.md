# Server side

Được viết bằng .NET Core + Entity Framework (MSSQL)

## Run Server side

Bước 1: Set lại biến ClothingDatabase tại file `/Server/ClothingStore/appsettings.json` phù hợp với database của máy.

Bước 2: Chạy file database `../script.sql` tại SQL Server Management Studio (SSMS) để khởi tạo database và thêm dữ liệu.

Bước 3: Mở file sln `/Server/ClothingStore.sln `để chạy project trên Visual Studio

Bước 4: Add migrations :

- `enable-migrations`
- `add-migration "init"`
- `update-database`
