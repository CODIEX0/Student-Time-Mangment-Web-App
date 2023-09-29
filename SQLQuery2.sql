SELECT w.Id, w.[Semester Number], m.Code, m.[Number Of Credits], m.[Class Hours Per Week], w.[Hours Spent], s.[Number of Weeks] FROM Week w
JOIN Semester s ON s.Id = w.[Semester Number]
JOIN Module m  ON m.[Semester Number] = w.[Semester Number]
AND m.Code = w.Code;

SELECT w.* FROM Week w
JOIN Module m ON w.Code = m.Code;

SELECT m.*,s.[Number of Weeks] FROM Module m
JOIN Semester s ON s.Id = m.[Semester Number];


SELECT Module.*,Semester.[Number of Weeks]
FROM Module, Semester
WHERE Semester.Id = Module.[Semester Number];

INSERT INTO Week
([Semester Number],Code,[Hours Spent],[Start Date]) VALUES
(1, 'CLDV6453', 2,'04/20/2023');

SELECT m.Id, m.[Semester Number], m.Code, m.Name, m.[Number Of Credits], m.[Class Hours Per Week], m.[Reminder Date], s.[Number of Weeks]
From Module m
JOIN Semester s ON s.Id = m.[Semester Number]
WHERE s.[User Id] = '7a04bca2-626c-48cd-bb23-3b5529381c56';