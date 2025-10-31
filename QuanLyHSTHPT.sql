--T?o c? s? d? li?u
CREATE DATABASE QL_HOCSINH_THPT;
GO
USE QL_HOCSINH_THPT;
GO

-- 2. Bang bo mon
CREATE TABLE DM_TO (
    ToBoMon NVARCHAR(20) PRIMARY KEY,
    TenToBoMon NVARCHAR(100)
);

-- 3. Bang lop hoc
CREATE TABLE DM_LOP (
    Lop NVARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(100),
    NamHoc NVARCHAR(20)
);

-- 4. Bang hoc sinh
CREATE TABLE DM_HOCSINH (
    MaHS CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(200),
    Lop NVARCHAR(10),
    SDT VARCHAR(15),
    Email VARCHAR(100),
    FOREIGN KEY (Lop) REFERENCES DM_LOP(Lop)
);

-- 5. Bang mon hoc
CREATE TABLE DM_MONHOC (
    MaMon CHAR(10) PRIMARY KEY,
    TenMon NVARCHAR(100),
    SoTiet INT,
    ToBoMon NVARCHAR(20),
    FOREIGN KEY (ToBoMon) REFERENCES DM_TO(ToBoMon)
);

-- 6. Bang  giao vien
CREATE TABLE DM_GIAOVIEN (
    MaGV CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SDT VARCHAR(15),
    ChucVu NVARCHAR(50),
    ToBoMon NVARCHAR(20),
    FOREIGN KEY (ToBoMon) REFERENCES DM_TO(ToBoMon)
);

-- 7. Bang giang day
CREATE TABLE DM_GIANGDAY (
    MaHS CHAR(10),
    MaMon CHAR(10),
    HocKy NVARCHAR(10),
    NamHoc NVARCHAR(20),
    DiemMieng FLOAT,
    Diem15p FLOAT,
    DiemHK FLOAT,
    DiemTB AS (ROUND((DiemMieng + Diem15p + DiemHK * 2) / 4.0, 2)),
    GhiChu NVARCHAR(200),
    PRIMARY KEY (MaHS, MaMon, HocKy, NamHoc),
    FOREIGN KEY (MaHS) REFERENCES DM_HOCSINH(MaHS),
    FOREIGN KEY (MaMon) REFERENCES DM_MONHOC(MaMon)
);

-- 8. bang phan cong giang day
CREATE TABLE PHANCONG (
    MaGV CHAR(10),
    Lop NVARCHAR(10),
    MaMon CHAR(10),
    NamHoc NVARCHAR(20),
    PRIMARY KEY (MaGV, Lop, MaMon, NamHoc),
    FOREIGN KEY (MaGV) REFERENCES DM_GIAOVIEN(MaGV),
    FOREIGN KEY (Lop) REFERENCES DM_LOP(Lop),
    FOREIGN KEY (MaMon) REFERENCES DM_MONHOC(MaMon)
);

-- 9. bang tai khoan dang nhap
CREATE TABLE DANGNHAP (
    TenDN VARCHAR(50) PRIMARY KEY,
    MatKhau VARBINARY(256),
    MaGV CHAR(10) NULL,
    MaHS CHAR(10) NULL,
    FOREIGN KEY (MaGV) REFERENCES DM_GIAOVIEN(MaGV),
    FOREIGN KEY (MaHS) REFERENCES DM_HOCSINH(MaHS)
);

-- 10. bang lich su dang nhap
CREATE TABLE LICHSU_DANGNHAP (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDN VARCHAR(50),
    ThoiGianDangNhap DATETIME DEFAULT GETDATE(),
    DiaChiIP VARCHAR(50),
    TrangThai NVARCHAR(50),
    FOREIGN KEY (TenDN) REFERENCES DANGNHAP(TenDN)
);
--DL TO
INSERT INTO DM_TO (ToBoMon, TenToBoMon)
VALUES
('TOAN', N'T? Toán - Tin'),
('VAN', N'T? Ng? V?n'),
('LY', N'T? V?t Lý'),
('HOA', N'T? Hóa H?c'),
('SINH', N'T? Sinh H?c'),
('ANH', N'T? Ngo?i Ng?');

--DL LOP
INSERT INTO DM_LOP (Lop, TenLop, NamHoc)
VALUES
('10A1', N'L?p 10A1', '2024-2025'),
('10A2', N'L?p 10A2', '2024-2025'),
('11A1', N'L?p 11A1', '2024-2025');

--Dl Giao vien

INSERT INTO DM_GIAOVIEN (MaGV, HoTen, NgaySinh, GioiTinh, SDT, ChucVu, ToBoMon)
VALUES
('GV001', N'Nguy?n Th? Lan', '1980-03-12', N'N?', '0905123456', N'Giáo viên', 'TOAN'),
('GV002', N'Tr?n V?n Minh', '1978-06-25', N'Nam', '0906234567', N'Giáo viên', 'ANH'),
('GV003', N'Lê Th? Hoa', '1982-11-02', N'N?', '0907345678', N'Giáo viên', 'LY'),
('GV004', N'Ph?m H?u Phong', '1979-09-10', N'Nam', '0908456789', N'T? tr??ng', 'HOA');

--
INSERT INTO DM_HOCSINH (MaHS, HoTen, NgaySinh, GioiTinh, DiaChi, Lop, SDT, Email)
VALUES
('HS001', N'Ngô Minh Anh', '2008-05-10', N'N?', N'TP. Hà N?i', '10A1', '0912345678', 'anhnm@example.com'),
('HS002', N'Ph?m Qu?c Huy', '2008-07-21', N'Nam', N'H?i Phòng', '10A1', '0913456789', 'huyphq@example.com'),
('HS003', N'Lê Th?o Nhi', '2008-08-14', N'N?', N'TP. H? Chí Minh', '10A2', '0914567890', 'nhilt@example.com'),
('HS004', N'Tr?n ??c Long', '2007-12-30', N'Nam', N'?à N?ng', '11A1', '0915678901', 'longtd@example.com');
--
INSERT INTO DM_MONHOC (MaMon, TenMon, SoTiet, ToBoMon)
VALUES
('MATH', N'Toán h?c', 120, 'TOAN'),
('LIT', N'Ng? v?n', 120, 'VAN'),
('ENG', N'Ti?ng Anh', 100, 'ANH'),
('PHY', N'V?t lý', 90, 'LY'),
('CHEM', N'Hóa h?c', 90, 'HOA');

--
INSERT INTO DM_GIANGDAY (MaHS, MaMon, HocKy, NamHoc, DiemMieng, Diem15p, DiemHK, GhiChu)
VALUES
('HS001', 'MATH', 'HK1', '2024-2025', 8.0, 7.5, 8.0, N'H?c t?t'),
('HS001', 'ENG', 'HK1', '2024-2025', 7.0, 7.0, 7.5, N'C?n c? g?ng'),
('HS002', 'MATH', 'HK1', '2024-2025', 6.0, 6.5, 7.0, N'Ti?n b?'),
('HS003', 'CHEM', 'HK1', '2024-2025', 9.0, 8.5, 9.0, N'Xu?t s?c'),
('HS004', 'PHY', 'HK1', '2024-2025', 7.0, 8.0, 8.5, N'H?c khá');
--

INSERT INTO PHANCONG (MaGV, Lop, MaMon, NamHoc)
VALUES
('GV001', '10A1', 'MATH', '2024-2025'),
('GV002', '10A1', 'ENG', '2024-2025'),
('GV003', '10A2', 'PHY', '2024-2025'),
('GV004', '10A2', 'CHEM', '2024-2025');
--

INSERT INTO DANGNHAP (TenDN, MatKhau, MaGV, MaHS)
VALUES
('lol', '1234', NULL, NULL); -- 'hocsinh'

--
INSERT INTO LICHSU_DANGNHAP (TenDN, DiaChiIP, TrangThai)
VALUES
('admin', '192.168.1.2', N'Thành công'),
('gv_lan', '192.168.1.3', N'Th?t b?i - sai m?t kh?u'),
('hs_anh', '192.168.1.5', N'Thành công');
select @@SERVERNAME
Select * from DANGNHAP
ALTER TABLE DANGNHAP ALTER COLUMN MatKhau VARCHAR(50);
delete from DANGNHAP