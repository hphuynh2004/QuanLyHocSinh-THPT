CREATE DATABASE QuanLyHocSinhTHPT;

USE QuanLyHocSinhTHPT;

CREATE TABLE HOCSINH (
    MaHS NVARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(50),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
	DiaChi NVARCHAR(100),
    Lop NVARCHAR(10),
	NamHoc NVARCHAR(10)
);


CREATE TABLE GIAOVIEN (
    MaGV NVARCHAR(10) PRIMARY KEY,       
    HoTen NVARCHAR(50) NOT NULL,        
    NgaySinh DATE NOT NULL,             
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(100),              
    SoDienThoai NVARCHAR(15),            
    MonDay NVARCHAR(50)                  
);

CREATE TABLE MONHOC (
    MaMH NVARCHAR(10) PRIMARY KEY,       
    TenMH NVARCHAR(100) NOT NULL,         
    SoTiet INT NOT NULL CHECK (SoTiet > 0), 
    GhiChu NVARCHAR(255) NULL             
);

CREATE TABLE LOP (
    MaLOP NVARCHAR(10) PRIMARY KEY,       
    TenLOP NVARCHAR(50) NOT NULL,         
    Khoi NVARCHAR(10) NOT NULL,           
    GVCN NVARCHAR(100) NULL,              
    SiSo INT CHECK (SiSo >= 0),           
    NamHoc NVARCHAR(20) NOT NULL,         
    NgayTao DATETIME DEFAULT GETDATE(),   
    TrangThai NVARCHAR(20) NOT NULL       
);

CREATE TABLE KHOI (
    MaKhoi NVARCHAR(10) PRIMARY KEY,     
    TenKhoi NVARCHAR(50) NOT NULL,        
    GhiChu NVARCHAR(255) NULL             
);

CREATE TABLE DIEM (
    MaDiem NVARCHAR(10) PRIMARY KEY,          
    MaHS NVARCHAR(10) NOT NULL,              
    MaMH NVARCHAR(10) NOT NULL,                
    Diem15p FLOAT CHECK (Diem15p BETWEEN 0 AND 10),
    Diem1T FLOAT CHECK (Diem1T BETWEEN 0 AND 10),
    DiemCK FLOAT CHECK (DiemCK BETWEEN 0 AND 10),
    DiemTB FLOAT CHECK (DiemTB BETWEEN 0 AND 10),
    FOREIGN KEY (MaHS) REFERENCES HOCSINH(MaHS),
    FOREIGN KEY (MaMH) REFERENCES MONHOC(MaMH)
);

CREATE TABLE KETQUA (
    MaKQ NVARCHAR(10) PRIMARY KEY,          
    MaHS NVARCHAR(10) NOT NULL,             
    TenHS NVARCHAR(100) NOT NULL,           
    Lop NVARCHAR(20) NOT NULL,             
    GVCN NVARCHAR(100) NULL,                
    DiemTB FLOAT NOT NULL,                  
    HanhKiem NVARCHAR(50) NULL,             
    XepLoai NVARCHAR(50) NULL,              
    FOREIGN KEY (MaHS) REFERENCES HOCSINH(MaHS)
);

SELECT 
    HS.MaHS,
    HS.HoTen AS TenHS,
    HS.Lop,
    L.GVCN,
    ROUND(AVG((D.Diem15P + D.Diem1T * 2 + D.DiemCK * 3) / 6.0), 2) AS DiemTB
FROM HOCSINH HS
JOIN DIEM D ON HS.MaHS = D.MaHS
LEFT JOIN LOP L ON HS.Lop = L.MaLOP
GROUP BY HS.MaHS, HS.HoTen, HS.Lop, L.GVCN

INSERT INTO HOCSINH (MaHS, HoTen, NgaySinh, GioiTinh, DiaChi, Lop, NamHoc)
VALUES
('HS001', N'Nguyễn Văn An', '2007-03-15', N'Nam', N'Hà Nội', '10A1', '2024-2025'),
('HS002', N'Trần Thị Bích Ngọc', '2007-07-22', N'Nữ', N'Hải Phòng', '10A1', '2024-2025'),
('HS003', N'Lê Minh Quân', '2006-11-03', N'Nam', N'Hồ Chí Minh', '11B2', '2024-2025'),
('HS004', N'Phạm Thảo Vy', '2006-09-10', N'Nữ', N'Đà Nẵng', '11B3', '2024-2025'),
('HS005', N'Đỗ Tuấn Kiệt', '2005-12-01', N'Nam', N'Bắc Ninh', '12C1', '2024-2025'),
('HS006', N'Vũ Lan Chi', '2005-06-25', N'Nữ', N'Nghệ An', '12C2', '2024-2025'),
('HS007', N'Ngô Thanh Bình', '2007-05-11', N'Nam', N'Hà Nam', '10A2', '2024-2025'),
('HS008', N'Hoàng Mai Anh', '2007-02-18', N'Nữ', N'Quảng Ninh', '10A2', '2024-2025'),
('HS009', N'Phan Đức Duy', '2006-08-30', N'Nam', N'Bình Dương', '11B1', '2024-2025'),
('HS010', N'Lý Khánh Hà', '2005-10-14', N'Nữ', N'Đà Lạt', '12C3', '2024-2025');

INSERT INTO GIAOVIEN (MaGV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, MonDay) VALUES
('GV001', N'Nguyễn Văn Minh', '1980-03-12', N'Nam', N'Hà Nội', '0912345678', N'Toán'),
('GV002', N'Trần Thị Hồng Nhung', '1985-07-24', N'Nữ', N'Hải Phòng', '0987654321', N'Ngữ Văn'),
('GV003', N'Lê Quang Huy', '1979-11-02', N'Nam', N'Ninh Bình', '0905123456', N'Tiếng Anh'),
('GV004', N'Phạm Thị Lan', '1990-01-15', N'Nữ', N'Nam Định', '0912233445', N'Vật Lý'),
('GV005', N'Hoàng Đức Anh', '1983-09-09', N'Nam', N'Thanh Hóa', '0978111222', N'Hóa Học'),
('GV006', N'Nguyễn Thị Mai', '1992-05-18', N'Nữ', N'Hà Tĩnh', '0909988776', N'Sinh Học'),
('GV007', N'Vũ Trọng Nghĩa', '1988-12-21', N'Nam', N'Hà Nội', '0933445566', N'Lịch Sử'),
('GV008', N'Đỗ Thị Bích Phượng', '1986-04-07', N'Nữ', N'Hải Dương', '0966555444', N'Địa Lý'),
('GV009', N'Ngô Thanh Tùng', '1981-10-30', N'Nam', N'Hưng Yên', '0922334455', N'Tin Học'),
('GV010', N'Phan Thị Thu Hà', '1994-02-28', N'Nữ', N'Hà Nội', '0977555444', N'Thể Dục');

INSERT INTO MONHOC (MaMH, TenMH, SoTiet, GhiChu)
VALUES
('MH01', N'Toán học', 45, N'Môn học bắt buộc, rèn luyện tư duy logic'),
('MH02', N'Ngữ văn', 45, N'Môn học bắt buộc, phát triển ngôn ngữ và cảm thụ văn học'),
('MH03', N'Tiếng Anh', 45, N'Môn học bắt buộc, giúp học sinh giao tiếp quốc tế'),
('MH04', N'Vật lý', 45, N'Thuộc nhóm môn Khoa học tự nhiên'),
('MH05', N'Hóa học', 45, N'Thuộc nhóm môn Khoa học tự nhiên'),
('MH06', N'Sinh học', 45, N'Thuộc nhóm môn Khoa học tự nhiên'),
('MH07', N'Lịch sử', 35, N'Thuộc nhóm môn Khoa học xã hội'),
('MH08', N'Địa lý', 35, N'Thuộc nhóm môn Khoa học xã hội');

INSERT INTO LOP (MaLOP, TenLOP, Khoi, GVCN, SiSo, NamHoc, NgayTao, TrangThai)
VALUES
('10A1', N'Lớp 10A1', N'10', N'Nguyễn Thị Mai', 40, N'2024-2025', '2024-08-01', N'Hoạt động'),
('10A2', N'Lớp 10A2', N'10', N'Lê Văn Minh', 38, N'2024-2025', '2024-08-01', N'Hoạt động'),
('10A3', N'Lớp 10A3', N'10', N'Trần Văn Hùng', 37, N'2024-2025', '2024-08-02', N'Hoạt động'),
('11A1', N'Lớp 11A1', N'11', N'Phạm Đức Long', 42, N'2024-2025', '2024-08-03', N'Hoạt động'),
('11A2', N'Lớp 11A2', N'11', N'Hoàng Thị Yến', 39, N'2024-2025', '2024-08-03', N'Ngừng hoạt động'),
('11A3', N'Lớp 11A3', N'11', N'Nguyễn Văn Nam', 41, N'2024-2025', '2024-08-04', N'Hoạt động'),
('12A1', N'Lớp 12A1', N'12', N'Vũ Thanh Sơn', 43, N'2024-2025', '2024-08-05', N'Hoạt động'),
('12A2', N'Lớp 12A2', N'12', N'Nguyễn Thu Hằng', 40, N'2024-2025', '2024-08-06', N'Hoạt động'),
('12A3', N'Lớp 12A3', N'12', N'Lê Thị Hòa', 39, N'2024-2025', '2024-08-06', N'Ngừng hoạt động'),
('12A4', N'Lớp 12A4', N'12', N'Đỗ Văn Nam', 38, N'2024-2025', '2024-08-07', N'Hoạt động');

INSERT INTO KETQUA_HOCTAP (MaDiem, MaHS, MaMH, Diem15P, Diem1T, DiemCK)
SELECT D.MaDiem, D.MaHS, D.MaMH, D.Diem15P, D.Diem1T, D.DiemCK
FROM DIEM D
WHERE NOT EXISTS (
    SELECT 1 FROM KETQUA_HOCTAP K WHERE K.MaDiem = D.MaDiem
);

INSERT INTO KHOI (MaKhoi, TenKhoi, GhiChu)
VALUES 
('K10', N'Khối 10', N'Dành cho học sinh lớp 10'),
('K11', N'Khối 11', N'Dành cho học sinh lớp 11'),
('K12', N'Khối 12', N'Dành cho học sinh lớp 12');



SELECT * FROM HOCSINH;
SELECT * FROM GIAOVIEN;
SELECT * FROM MONHOC;
SELECT * FROM LOP;
SELECT * FROM KHOI;
SELECT * FROM DIEM;
SELECT * FROM KETQUA;



