# Hệ thống Quản lý Nhóm Dự án và Nhân sự

Chương trình cài đặt hướng đối tượng (OOP) bằng C# (.NET), hiện thực hóa các nguyên lý kế thừa, đa hình, quan hệ kết tập không sở hữu và các bất biến nghiệp vụ theo yêu cầu.

---

## 1. Kiến trúc và Các nguyên lý OOP

1. **Quan hệ Kế thừa (Inheritance):**
   * Lớp `SoftwareEngineer` kế thừa công khai từ `Employee`, mở rộng các thuộc tính ngôn ngữ lập trình chính (`primaryLanguage`) và phụ cấp kỹ thuật (`technicalAllowance`).
2. **Tính Đa hình (Polymorphism):**
   * Ghi đè phương thức tính chi phí hàng tháng `CalculateMonthlyCost()` và phương thức xuất thông tin `DisplayInfo()` ở thời điểm thực thi (runtime).
3. **Quan hệ Kết tập không sở hữu (Aggregation):**
   * Lớp `ProjectTeam` duy trì liên kết dạng tham chiếu không sở hữu đến các đối tượng `Employee`. Vòng đời của nhân sự độc lập hoàn toàn với nhóm dự án.
4. **Tính Đóng gói và Bất biến mô hình (Invariants):**
   * Tính duy nhất của thành viên: Không cho phép trùng mã nhân sự trong cùng một nhóm (`Contains()`).
   * Toàn vẹn vai trò trưởng nhóm: Trưởng nhóm bắt buộc phải thuộc danh sách thành viên ($Leader \neq null \implies Leader \in Members$).
   * Bảo vệ vai trò trưởng nhóm: Không cho phép xóa nhân sự đang đảm nhiệm trưởng nhóm khi chưa chỉ định người thay thế.

---

## 2. Cấu trúc thư mục

```text
.
├── Classes
│   ├── Employee.cs
│   ├── ProjectTeam.cs
│   └── SoftwareEngineer.cs
├── OOP_Exercise_03.csproj
├── README.md
└── Test.cs
```

---

## 3. Hướng dẫn biên dịch và chạy chương trình

### Yêu cầu môi trường
* .NET SDK 6.0 trở lên (hoặc quản lý thông qua môi trường Pixi).

### Lệnh thực thi
Sử dụng công cụ dòng lệnh .NET chuẩn:
```bash
dotnet run
```
Hoặc thông qua môi trường Pixi:
```bash
pixi run dotnet run
```

---

## 4. Kịch bản kiểm thử (15 bước)
Chương trình trong `Test.cs` thực thi tự động toàn bộ 15 bước kiểm thử theo đặc tả: kiểm tra nạp chồng constructor, tăng lương (số tiền cố định và phần trăm), từ chối thêm trùng lặp, cơ chế bảo vệ trưởng nhóm, kết tập đa nhóm và kiểm chứng sự tồn tại của nhân sự sau khi nhóm dự án bị tiêu hủy.