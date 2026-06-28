USE FPTBook
GO

-- Xóa dữ liệu cũ nếu có (theo thứ tự khóa ngoại để không bị lỗi)
DELETE FROM OrderDetail;
DELETE FROM Orders;
DELETE FROM Feedback;
DELETE FROM Book;
DELETE FROM Category;
DELETE FROM Author;
DELETE FROM Publisher;
DELETE FROM Account;
GO

-- Reset Identity cho các bảng có cột Identity (nếu cần thiết)
DBCC CHECKIDENT ('Orders', RESEED, 0);
DBCC CHECKIDENT ('Feedback', RESEED, 0);
GO

-- 1. Thêm dữ liệu bảng Account
INSERT INTO Account (Username, Fullname, Password, Email, Telephone, Address, Role) VALUES
('admin', N'Quản Trị Viên', '0e7517141fb53f21ee439b355b5a1d0a', 'admin@fptbook.com', '0123456789', N'Hà Nội', 1),
('chiluan', N'Nguyễn Văn A', '0e7517141fb53f21ee439b355b5a1d0a', 'chiluan@gmail.com', '0987654321', N'Hồ Chí Minh', 0),
('ngoctien', N'Trần Thị B', '0e7517141fb53f21ee439b355b5a1d0a', 'ngoctien@gmail.com', '0912345678', N'Đà Nẵng', 0);
GO

-- 2. Thêm dữ liệu bảng Feedback
INSERT INTO Feedback (Username, Telephone, Email, Message, DateSend) VALUES
('chiluan', '0987654321', 'chiluan@gmail.com', N'Website có giao diện đẹp và dễ sử dụng.', GETDATE()),
('ngoctien', '0912345678', 'ngoctien@gmail.com', N'Sách công nghệ thông tin rất hữu ích, hy vọng có thêm sách chuyên ngành.', GETDATE());
GO

-- 3. Thêm dữ liệu bảng Category
INSERT INTO Category (CategoryID, CategoryName, Description) VALUES
('CAT01', N'Công Nghệ Thông Tin', N'Sách về lập trình, phát triển phần mềm, quản trị mạng, blockchain...'),
('CAT02', N'Kinh Tế & Kinh Doanh', N'Sách về quản lý tài chính, kinh doanh, đầu tư, bài học thành công...'),
('CAT03', N'Tâm Lý & Kỹ Năng Sống', N'Sách về phát triển bản thân, kỹ năng sống, giao tiếp, tâm lý học...'),
('CAT04', N'Giáo Dục & Sư Phạm', N'Sách hướng dẫn phương pháp học tập, giảng dạy hiệu quả.');
GO

-- 4. Thêm dữ liệu bảng Author
INSERT INTO Author (AuthorID, AuthorName, Description) VALUES
('AUTH01', N'Robert T. Kiyosaki', N'Tác giả nổi tiếng với cuốn Cha Giàu Cha Nghèo.'),
('AUTH02', N'Jon Galloway', N'Chuyên gia về công nghệ ASP.NET MVC.'),
('AUTH03', N'David J. Schwartz', N'Tác giả cuốn Sự Kỳ Diệu Của Tư Duy Lớn.'),
('AUTH04', N'Adam Grant', N'Nhà tâm lý học, tác giả cuốn Think Again.'),
('AUTH05', N'Robert C. Martin', N'Chuyên gia phát triển phần mềm, tác giả cuốn Clean Code.'),
('AUTH06', N'Daniel Imran', N'Chuyên gia công nghệ, tác giả cuốn Blockchain Basics.'),
('AUTH07', N'Nhiều Tác Giả', N'Tập hợp từ nhiều tác giả và chuyên gia khác.');
GO

-- 5. Thêm dữ liệu bảng Publisher
INSERT INTO Publisher (PublisherID, PublisherName, Description) VALUES
('PUB01', N'Wrox Press', N'Nhà xuất bản chuyên về sách công nghệ, lập trình.'),
('PUB02', N'Plata Publishing', N'Nhà xuất bản các tựa sách kinh doanh, tài chính nổi tiếng.'),
('PUB03', N'Prentice Hall', N'Nhà xuất bản chuyên cung cấp tài liệu giáo dục và học thuật.'),
('PUB04', N'Penguin Books', N'Một trong những nhà xuất bản uy tín nhất thế giới.'),
('PUB05', N'Independently Published', N'Sách được xuất bản độc lập hoặc bởi các nhà xuất bản nhỏ.');
GO

-- 6. Thêm dữ liệu bảng Book (sử dụng tên ảnh trong FPTBook/Image)
INSERT INTO Book (BookID, BookName, CategoryID, AuthorID, PublisherID, Price, Quantity, Image, Description) VALUES
('B001', N'Blockchain Basics', 'CAT01', 'AUTH06', 'PUB05', 150000, 50, 'BlockchainBasics.jpg', N'A non-technical introduction in 25 steps.'),
('B002', N'Clean Code', 'CAT01', 'AUTH05', 'PUB03', 250000, 30, 'Clean-Code.jpg', N'A Handbook of Agile Software Craftsmanship.'),
('B003', N'Daily Wisdom for Why Does He Do That', 'CAT03', 'AUTH07', 'PUB04', 120000, 40, 'Daily Wisdom for Why Does He Do That.jpg', N'Encouragement for women involved with angry and controlling men.'),
('B004', N'Dangerous Personalities', 'CAT03', 'AUTH07', 'PUB05', 180000, 25, 'Dangerous Personalities An FBI Profiler Shows You How to Identify and Protect Yourself from Harmful People.jpg', N'An FBI Profiler Shows You How to Identify and Protect Yourself from Harmful People.'),
('B005', N'Feeling Great', 'CAT03', 'AUTH07', 'PUB04', 200000, 60, 'Feeling Great The Revolutionary New Treatment for Depression and Anxiety.jpg', N'The Revolutionary New Treatment for Depression and Anxiety.'),
('B006', N'How Tutoring Works', 'CAT04', 'AUTH07', 'PUB05', 140000, 20, 'How Tutoring Works Six Steps to Grow Motivation and Accelerate Student Learning.jpg', N'Six Steps to Grow Motivation and Accelerate Student Learning.'),
('B007', N'How to Survive University', 'CAT04', 'AUTH07', 'PUB05', 90000, 100, 'How to Survive University An Essential Pocket Guide.jpg', N'An Essential Pocket Guide.'),
('B008', N'Making Differentiation a Habit', 'CAT04', 'AUTH07', 'PUB03', 170000, 15, 'Making Differentiation a Habit How to Ensure Success in Academically Diverse Classrooms.jpg', N'How to Ensure Success in Academically Diverse Classrooms.'),
('B009', N'Professional ASP.NET MVC 5', 'CAT01', 'AUTH02', 'PUB01', 300000, 45, 'Professional-ASP-NET-MVC-5-Galloway-Jon.jpg', N'Comprehensive guide to ASP.NET MVC 5.'),
('B010', N'Rich Dad Poor Dad', 'CAT02', 'AUTH01', 'PUB02', 160000, 80, 'RichDadPoorDad.jpg', N'What the Rich Teach Their Kids About Money That the Poor and Middle Class Do Not!'),
('B011', N'Self-Paced Training Kit Exam', 'CAT01', 'AUTH07', 'PUB01', 220000, 10, 'Self-Paced-Training-Kit-Exam.jpg', N'Preparation for certification exam.'),
('B012', N'The Story of Success', 'CAT03', 'AUTH07', 'PUB04', 190000, 35, 'The Story of Success.jpg', N'Outliers: The Story of Success.'),
('B013', N'The Magic of Thinking Big', 'CAT03', 'AUTH03', 'PUB04', 150000, 70, 'The-Magic-of-Thinking-Big-9780671646783.jpg', N'Acquire the secrets of success ... achieve everything youve always wanted.'),
('B014', N'Think Again', 'CAT03', 'AUTH04', 'PUB04', 210000, 55, 'Think Again.jpg', N'The Power of Knowing What You Dont Know.'),
('B015', N'Unwinding Anxiety', 'CAT03', 'AUTH07', 'PUB05', 175000, 40, 'Unwinding Anxiety New Science Shows How to Break the Cycles of Worry and Fear to Heal Your Mind.jpg', N'New Science Shows How to Break the Cycles of Worry and Fear to Heal Your Mind.'),
('B016', N'Wisdom of Insecurity', 'CAT03', 'AUTH07', 'PUB04', 130000, 50, 'Wisdom of Insecurity  A Message for an Age of Anxiety.jpg', N'A Message for an Age of Anxiety.');
GO

-- 7. Thêm dữ liệu bảng Orders
INSERT INTO Orders (Username, Telephone, Fullname, DeliveyAddress, OrderDate, TotalPrice) VALUES
('chiluan', '0987654321', N'Nguyễn Văn A', N'Số 1, Lê Duẩn, Quận 1, TP HCM', GETDATE(), 400000),
('ngoctien', '0912345678', N'Trần Thị B', N'Số 5, Nguyễn Văn Linh, Hải Châu, Đà Nẵng', GETDATE(), 300000);
GO

-- 8. Thêm dữ liệu bảng OrderDetail (Chú ý: OrderID là IDENTITY nên tự động tăng, ta giả sử 2 đơn vừa tạo có ID là 1 và 2)
-- Nếu DBCC CHECKIDENT đã được reset, đơn đầu tiên sẽ có ID = 1
INSERT INTO OrderDetail (OrderID, BookID, Quantity, Price, Subtotal) VALUES
(1, 'B001', 1, 150000, 150000),
(1, 'B002', 1, 250000, 250000),
(2, 'B009', 1, 300000, 300000);
GO
