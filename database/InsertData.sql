USE FPTBook
GO

-- Delete old data if exists (in order of foreign keys to avoid errors)
DELETE FROM OrderDetail;
DELETE FROM Orders;
DELETE FROM Feedback;
DELETE FROM Book;
DELETE FROM Category;
DELETE FROM Author;
DELETE FROM Publisher;
DELETE FROM Account;
GO

-- Reset Identity for tables with Identity columns (if necessary)
DBCC CHECKIDENT ('Orders', RESEED, 0);
DBCC CHECKIDENT ('Feedback', RESEED, 0);
GO

-- 1. Insert data into Account table
INSERT INTO Account (Username, Fullname, Password, Email, Telephone, Address, Role) VALUES
('admin', N'Administrator', '0e7517141fb53f21ee439b355b5a1d0a', 'admin@fptbook.com', '0123456789', N'Hanoi', 1),
('chiluan', N'Chí Luân', '0e7517141fb53f21ee439b355b5a1d0a', 'chiluan@gmail.com', '0987654321', N'Ho Chi Minh City', 0),
('ngoctien', N'Ngọc Tiên', '0e7517141fb53f21ee439b355b5a1d0a', 'ngoctien@gmail.com', '0912345678', N'Da Nang', 0);
GO

-- 2. Insert data into Feedback table
INSERT INTO Feedback (Username, Telephone, Email, Message, DateSend) VALUES
('chiluan', '0987654321', 'chiluan@gmail.com', N'The website has a beautiful interface and is easy to use.', GETDATE()),
('ngoctien', '0912345678', 'ngoctien@gmail.com', N'The IT books are very useful, hope to have more specialized books.', GETDATE());
GO

-- 3. Insert data into Category table
INSERT INTO Category (CategoryID, CategoryName, Description) VALUES
('CAT01', N'Information Technology', N'Books on programming, software development, network administration, blockchain...'),
('CAT02', N'Economics & Business', N'Books on financial management, business, investing, success lessons...'),
('CAT03', N'Psychology & Life Skills', N'Books on personal development, life skills, communication, psychology...'),
('CAT04', N'Education & Teaching', N'Books guiding effective study and teaching methods.');
GO

-- 4. Insert data into Author table
INSERT INTO Author (AuthorID, AuthorName, Description) VALUES
('AUTH01', N'Robert T. Kiyosaki', N'Famous author of Rich Dad Poor Dad.'),
('AUTH02', N'Jon Galloway', N'Expert in ASP.NET MVC technology.'),
('AUTH03', N'David J. Schwartz', N'Author of The Magic of Thinking Big.'),
('AUTH04', N'Adam Grant', N'Psychologist, author of Think Again.'),
('AUTH05', N'Robert C. Martin', N'Software development expert, author of Clean Code.'),
('AUTH06', N'Daniel Imran', N'Technology expert, author of Blockchain Basics.'),
('AUTH07', N'Various Authors', N'A collection from various authors and other experts.');
GO

-- 5. Insert data into Publisher table
INSERT INTO Publisher (PublisherID, PublisherName, Description) VALUES
('PUB01', N'Wrox Press', N'Publisher specializing in technology and programming books.'),
('PUB02', N'Plata Publishing', N'Publisher of famous business and finance books.'),
('PUB03', N'Prentice Hall', N'Publisher providing educational and academic materials.'),
('PUB04', N'Penguin Books', N'One of the most prestigious publishers in the world.'),
('PUB05', N'Independently Published', N'Books published independently or by small publishers.');
GO

-- 6. Insert data into Book table (using image names in FPTBook/Image)
INSERT INTO Book (BookID, BookName, CategoryID, AuthorID, PublisherID, Price, Quantity, Image, Description) VALUES
('B001', N'Blockchain Basics', 'CAT01', 'AUTH06', 'PUB05', 150, 50, 'BlockchainBasics.jpg', N'A non-technical introduction in 25 steps.'),
('B002', N'Clean Code', 'CAT01', 'AUTH05', 'PUB03', 250, 30, 'Clean-Code.jpg', N'A Handbook of Agile Software Craftsmanship.'),
('B003', N'Daily Wisdom for Why Does He Do That', 'CAT03', 'AUTH07', 'PUB04', 120, 40, 'Daily Wisdom for Why Does He Do That.jpg', N'Encouragement for women involved with angry and controlling men.'),
('B004', N'Dangerous Personalities', 'CAT03', 'AUTH07', 'PUB05', 180, 25, 'Dangerous Personalities An FBI Profiler Shows You How to Identify and Protect Yourself from Harmful People.jpg', N'An FBI Profiler Shows You How to Identify and Protect Yourself from Harmful People.'),
('B005', N'Feeling Great', 'CAT03', 'AUTH07', 'PUB04', 200, 60, 'Feeling Great The Revolutionary New Treatment for Depression and Anxiety.jpg', N'The Revolutionary New Treatment for Depression and Anxiety.'),
('B006', N'How Tutoring Works', 'CAT04', 'AUTH07', 'PUB05', 140, 20, 'How Tutoring Works Six Steps to Grow Motivation and Accelerate Student Learning.jpg', N'Six Steps to Grow Motivation and Accelerate Student Learning.'),
('B007', N'How to Survive University', 'CAT04', 'AUTH07', 'PUB05', 90, 100, 'How to Survive University An Essential Pocket Guide.jpg', N'An Essential Pocket Guide.'),
('B008', N'Making Differentiation a Habit', 'CAT04', 'AUTH07', 'PUB03', 170, 15, 'Making Differentiation a Habit How to Ensure Success in Academically Diverse Classrooms.jpg', N'How to Ensure Success in Academically Diverse Classrooms.'),
('B009', N'Professional ASP.NET MVC 5', 'CAT01', 'AUTH02', 'PUB01', 300, 45, 'Professional-ASP-NET-MVC-5-Galloway-Jon.jpg', N'Comprehensive guide to ASP.NET MVC 5.'),
('B010', N'Rich Dad Poor Dad', 'CAT02', 'AUTH01', 'PUB02', 160, 80, 'RichDadPoorDad.jpg', N'What the Rich Teach Their Kids About Money That the Poor and Middle Class Do Not!'),
('B011', N'Self-Paced Training Kit Exam', 'CAT01', 'AUTH07', 'PUB01', 220, 10, 'Self-Paced-Training-Kit-Exam.jpg', N'Preparation for certification exam.'),
('B012', N'The Story of Success', 'CAT03', 'AUTH07', 'PUB04', 190, 35, 'The Story of Success.jpg', N'Outliers: The Story of Success.'),
('B013', N'The Magic of Thinking Big', 'CAT03', 'AUTH03', 'PUB04', 150, 70, 'The-Magic-of-Thinking-Big-9780671646783.jpg', N'Acquire the secrets of success ... achieve everything youve always wanted.'),
('B014', N'Think Again', 'CAT03', 'AUTH04', 'PUB04', 210, 55, 'Think Again.jpg', N'The Power of Knowing What You Dont Know.'),
('B015', N'Unwinding Anxiety', 'CAT03', 'AUTH07', 'PUB05', 175, 40, 'Unwinding Anxiety New Science Shows How to Break the Cycles of Worry and Fear to Heal Your Mind.jpg', N'New Science Shows How to Break the Cycles of Worry and Fear to Heal Your Mind.'),
('B016', N'Wisdom of Insecurity', 'CAT03', 'AUTH07', 'PUB04', 130, 50, 'Wisdom of Insecurity  A Message for an Age of Anxiety.jpg', N'A Message for an Age of Anxiety.');
GO

-- 7. Insert data into Orders table
INSERT INTO Orders (Username, Telephone, Fullname, DeliveyAddress, OrderDate, TotalPrice) VALUES
('chiluan', '0987654321', N'Nguyen Van A', N'No. 1, Le Duan, District 1, Ho Chi Minh City', GETDATE(), 400),
('ngoctien', '0912345678', N'Tran Thi B', N'No. 5, Nguyen Van Linh, Hai Chau, Da Nang', GETDATE(), 300);
GO

-- 8. Insert data into OrderDetail table (Note: OrderID is IDENTITY and auto-increments, assuming the 2 recently created orders have IDs 1 and 2)
-- If DBCC CHECKIDENT was reset, the first order will have ID = 1
INSERT INTO OrderDetail (OrderID, BookID, Quantity, Price, Subtotal) VALUES
(1, 'B001', 1, 150, 150),
(1, 'B002', 1, 250, 250),
(2, 'B009', 1, 300, 300);
GO
