INSERT INTO Module (Id, [Semester Number],Code,Name,[Number Of Credits],[Class Hours Per Week]) VALUES
(1 ,3, 'PROG6212', 'Programming', 15, 12);

INSERT INTO Week (Id, [Module Code],[Hours Spent],Name,[Self-Study Hours],[Remaining Self-Study Hours]) VALUES
(1 , 'PROG6212', 3, 15, 12);

INSERT INTO Farmers (Id, [Name],[LogggedIn]) VALUES
(1 , 'Cody', 'True');

SELECT * FROM  m 
JOIN Semester s ON s.Id = m.[Semeter Number];

SELECT * FROM [AspNetUsers];

SELECT  Code, Name, [Reminder Date] FROM Module WHERE [User Id] = '7a04bca2-626c-48cd-bb23-3b5529381c56';

SELECT Id, [Number of Weeks], [Start Date] FROM Semester WHERE [User Id] = '7a04bca2-626c-48cd-bb23-3b5529381c56';


 SELECT m.*, s.[Number of Weeks]
                            FROM Module m
                             JOIN Semester s ON s.Id = m.[Semester Number];

SELECT m.*, s.[Number of Weeks]
                             FROM Module m, Semester s
                             WHERE m.[Semester Number] = s.Id;

                          delete FROM Products;