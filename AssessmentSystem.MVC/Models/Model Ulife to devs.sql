
IF OBJECT_ID('StudentAssessmentPerformance') IS NOT NULL
DROP TABLE StudentAssessmentPerformance
GO

IF OBJECT_ID('StudentAssessmentAnswer') IS NOT NULL
DROP TABLE StudentAssessmentAnswer
GO

IF OBJECT_ID('StudentAssessment') IS NOT NULL
DROP TABLE StudentAssessment
GO

IF OBJECT_ID('QuestionOption') IS NOT NULL
DROP TABLE QuestionOption
GO

IF OBJECT_ID('Question') IS NOT NULL
DROP TABLE Question
GO

IF OBJECT_ID('Assessment') IS NOT NULL
DROP TABLE Assessment
GO

IF OBJECT_ID('Questionnarie') IS NOT NULL
DROP TABLE Questionnarie
GO

IF OBJECT_ID('Professor') IS NOT NULL
DROP TABLE Professor
GO

IF OBJECT_ID('Student') IS NOT NULL
DROP TABLE Student
GO

IF OBJECT_ID('Person') IS NOT NULL
DROP TABLE Person
GO

CREATE TABLE Person (
	PersonId INTEGER PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(500) NOT NULL,
	CPF VARCHAR(11) NOT NULL,
	RegisterDate DATETIME NOT NULL
	CONSTRAINT UQ_CPF UNIQUE(CPF)
)
GO

CREATE TABLE Student (
	StudentId INTEGER PRIMARY KEY IDENTITY(1, 1),
	PersonId INTEGER NOT NULL,
	RegisterDate DATETIME NOT NULL
	--Login
	--Password
	FOREIGN KEY (PersonId) REFERENCES Person (PersonId)
)
GO

CREATE TABLE Professor (
	ProfessorId INTEGER PRIMARY KEY IDENTITY(1, 1),
	PersonId INTEGER NOT NULL,
	RegisterDate DATETIME NOT NULL
	--Login
	--Password
	FOREIGN KEY (PersonId) REFERENCES Person (PersonId)
)
GO

CREATE TABLE Questionnarie (
	QuestionnarieId INTEGER PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(500) NOT NULL,
	RegisterDate DATETIME NOT NULL,
	ProfessorId INTEGER NOT NULL
	FOREIGN KEY (ProfessorId) REFERENCES Professor (ProfessorId)
)
GO

CREATE TABLE Question (
	QuestionId INTEGER PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(500) NOT NULL,
	RegisterDate DATETIME NOT NULL,
	ProfessorId INTEGER NOT NULL,
	QuestionnarieId INTEGER NOT NULL
	FOREIGN KEY (ProfessorId) REFERENCES Professor (ProfessorId),
	FOREIGN KEY (QuestionnarieId) REFERENCES Questionnarie (QuestionnarieId)
)
GO

CREATE TABLE QuestionOption (
	QuestionOptionId INTEGER PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(500) NOT NULL,
	IsCorrect BIT,
	RegisterDate DATETIME NOT NULL,
	ProfessorId INTEGER NOT NULL,
	QuestionId INTEGER NOT NULL
	FOREIGN KEY (ProfessorId) REFERENCES Professor (ProfessorId),
	FOREIGN KEY (QuestionId) REFERENCES Question (QuestionId)
)
GO

CREATE TABLE Assessment (
	AssessmentId INTEGER PRIMARY KEY IDENTITY(1, 1),
	[Name] VARCHAR(500) NOT NULL,
	RegisterDate DATETIME NOT NULL,
	ProfessorId INTEGER NOT NULL,
	QuestionnarieId INTEGER NOT NULL
	FOREIGN KEY (ProfessorId) REFERENCES Professor (ProfessorId),
	FOREIGN KEY (QuestionnarieId) REFERENCES Questionnarie (QuestionnarieId)
)
GO

CREATE TABLE StudentAssessment (
	StudentAssessmentId INTEGER PRIMARY KEY IDENTITY(1, 1),
	AssessmentId INTEGER NOT NULL,
	StudentId INTEGER NOT NULL,
	StartDate DATETIME,
	EndDate DATETIME
	FOREIGN KEY (AssessmentId) REFERENCES Assessment (AssessmentId),
	FOREIGN KEY (StudentId) REFERENCES Student (StudentId),
	CONSTRAINT UQ_AssessmentId_StudentId UNIQUE (AssessmentId, StudentId)
)
GO

CREATE TABLE StudentAssessmentAnswer (
	StudentAssessmentAnswerId INTEGER PRIMARY KEY IDENTITY(1, 1),
	StudentAssessmentId INTEGER NOT NULL,
	RegisterDate DATETIME NOT NULL,
	QuestionOptionId INTEGER NOT NULL
	FOREIGN KEY (StudentAssessmentId) REFERENCES StudentAssessment (StudentAssessmentId),
	FOREIGN KEY (QuestionOptionId) REFERENCES QuestionOption (QuestionOptionId)
)
GO

CREATE TABLE StudentAssessmentPerformance (
	StudentAssessmentPerformanceId INTEGER PRIMARY KEY IDENTITY(1, 1),
	StudentAssessmentId INTEGER NOT NULL,
	Grade DECIMAL,
	RegisterDate DATETIME NOT NULL
	FOREIGN KEY (StudentAssessmentId) REFERENCES StudentAssessment (StudentAssessmentId)
)
GO

IF EXISTS( SELECT 1 FROM Sys.Objects WHERE [Name] = 'f_GetStudentGrade' AND [Type] = 'FN' )
BEGIN
	DROP FUNCTION f_GetStudentGrade
END
GO

CREATE FUNCTION f_GetStudentGrade (@StudentAssessmentId INT)  
RETURNS DECIMAL( 5, 2 )  
BEGIN

	DECLARE @QuestionCount INT, @QuestionCountCorrect INT, @QuestionnarieId INT, @Grade DECIMAL
	
	SELECT @QuestionCountCorrect = COUNT(DISTINCT QO.QuestionOptionId),
	@QuestionnarieId = Qn.QuestionnarieId
	FROM StudentAssessment SA (NOLOCK)
	JOIN Assessment A (NOLOCK) ON SA.AssessmentId = A.AssessmentId
	JOIN Questionnarie Qn (NOLOCK) ON A.QuestionnarieId = Qn.QuestionnarieId
	JOIN Question Q (NOLOCK) ON Qn.QuestionnarieId = Q.QuestionnarieId
	JOIN QuestionOption QO (NOLOCK) ON Q.QuestionId = QO.QuestionId
	WHERE QO.IsCorrect = 1
	AND SA.StudentAssessmentId = 4
	GROUP BY Qn.QuestionnarieId
	
	SELECT @QuestionCount = COUNT(DISTINCT QO.QuestionOptionId)
	FROM Questionnarie Qn
	JOIN Question Q (NOLOCK) ON Qn.QuestionnarieId = Q.QuestionnarieId
	JOIN QuestionOption QO (NOLOCK) ON Q.QuestionId = QO.QuestionId
	WHERE QO.IsCorrect = 1
	AND Qn.QuestionnarieId = @QuestionnarieId
	
	
	SET @Grade = CAST(@QuestionCountCorrect AS DECIMAL) / CAST(@QuestionCount AS DECIMAL) * 100
	RETURN @Grade
END
GO

IF NOT EXISTS (SELECT 1 FROM Person)
BEGIN 
	INSERT INTO Person ([Name], CPF, RegisterDate)
	VALUES 
	('Professor 1', '12345678911', GETDATE()),
	('Professor 2', '12345678912', GETDATE()),
	('Professor 3', '12345678913', GETDATE()),
	('Student 1', '12345678921', GETDATE()),
	('Student 2', '12345678922', GETDATE()),
	('Student 3', '12345678923', GETDATE())

	INSERT INTO Student (PersonId, RegisterDate)
	SELECT PersonId, GETDATE()
	FROM Person 
	WHERE CHARINDEX('Student', [Name]) > 0

	INSERT INTO Professor (PersonId, RegisterDate)
	SELECT PersonId, GETDATE()
	FROM Person 
	WHERE CHARINDEX('Professor', [Name]) > 0

	INSERT INTO Questionnarie ([Name], RegisterDate, ProfessorId)
	SELECT 'Questionnarie ' + CAST((ROW_NUMBER() OVER(ORDER BY P.Name)) AS VARCHAR) + ' of ' + P.[Name],
	GETDATE(),
	PR.ProfessorId
	FROM Professor PR (NOLOCK)
	JOIN Person P (NOLOCK) ON PR.PersonId = P.PersonId
	CROSS JOIN
	(SELECT number FROM master..spt_values WHERE type = 'P' AND number BETWEEN 1 AND 2)AS Nums(n)
	ORDER BY P.[Name] 

	INSERT INTO Question ([Name], RegisterDate, ProfessorId, QuestionnarieId)
	SELECT 'Question ' + CAST((ROW_NUMBER() OVER(ORDER BY P.Name)) AS VARCHAR) + ' of ' + P.[Name],
	GETDATE(),
	PR.ProfessorId,
	Qn.QuestionnarieId
	FROM Professor PR (NOLOCK)
	JOIN Person P (NOLOCK) ON PR.PersonId = P.PersonId
	JOIN Questionnarie Qn (NOLOCK) ON PR.ProfessorId = Qn.ProfessorId
	CROSS JOIN
	(SELECT number FROM master..spt_values WHERE type = 'P' AND number BETWEEN 1 AND 4)AS Nums(n)
	ORDER BY P.[Name]

	INSERT INTO QuestionOption ([Name], IsCorrect, RegisterDate, ProfessorId, QuestionId)
	SELECT 'QuestionOption ' + CAST((ROW_NUMBER() OVER(ORDER BY P.Name)) AS VARCHAR) + ' of ' + P.[Name],
	CASE WHEN (ROW_NUMBER() OVER(ORDER BY P.Name) % 4) = 0 
		THEN 1 
		ELSE 0 
	END AS IsCorrect,
	GETDATE(),
	PR.ProfessorId,
	Q.QuestionId
	FROM Professor PR (NOLOCK)
	JOIN Person P (NOLOCK) ON PR.PersonId = P.PersonId
	JOIN Questionnarie Qn (NOLOCK) ON PR.ProfessorId = Qn.ProfessorId
	JOIN Question Q (NOLOCK) ON Qn.QuestionnarieId = Q.QuestionnarieId
	CROSS JOIN
	(SELECT number FROM master..spt_values WHERE type = 'P' AND number BETWEEN 1 AND 4)AS Nums(n)
	ORDER BY P.[Name]

	INSERT INTO Assessment ([Name], RegisterDate, ProfessorId, QuestionnarieId)
	SELECT 'Assessment ' + CAST((ROW_NUMBER() OVER(ORDER BY P.Name)) AS VARCHAR) + ' of ' + P.[Name],
	GETDATE(),
	PR.ProfessorId,
	Qn.QuestionnarieId
	FROM Professor PR (NOLOCK)
	JOIN Person P (NOLOCK) ON PR.PersonId = P.PersonId
	JOIN Questionnarie Qn (NOLOCK) ON PR.ProfessorId = Qn.ProfessorId
	ORDER BY P.[Name]

	INSERT INTO StudentAssessment (AssessmentId, StudentId, StartDate, EndDate)
	SELECT A.AssessmentId, 
	S.StudentId, 
	CASE WHEN (ROW_NUMBER() OVER(ORDER BY S.StudentId) % 2) = 0 
		THEN GETDATE()
		ELSE NULL 
	END AS StartDate,
	CASE WHEN (ROW_NUMBER() OVER(ORDER BY S.StudentId) % 4) = 0 
		THEN GETDATE()
		ELSE NULL 
	END AS StartDate
	FROM Student S (NOLOCK)
	CROSS JOIN Assessment A (NOLOCK)

	INSERT INTO StudentAssessmentAnswer (StudentAssessmentId, RegisterDate, QuestionOptionId)
	SELECT SA.StudentAssessmentId, GETDATE(), QO.QuestionOptionId
	FROM StudentAssessment SA (NOLOCK)
	JOIN Assessment A (NOLOCK) ON SA.AssessmentId = A.AssessmentId
	JOIN Questionnarie Qn (NOLOCK) ON A.QuestionnarieId = Qn.QuestionnarieId
	JOIN Question Q (NOLOCK) ON Qn.QuestionnarieId = Q.QuestionnarieId
	JOIN QuestionOption QO (NOLOCK) ON Q.QuestionId = QO.QuestionId
	WHERE QO.IsCorrect = 1
	ORDER BY SA.StudentAssessmentId

	INSERT INTO StudentAssessmentPerformance (StudentAssessmentId, Grade, RegisterDate)
	SELECT SA.StudentAssessmentId,
	(SELECT dbo.f_GetStudentGrade(SA.StudentAssessmentId)),
	GETDATE()
	FROM StudentAssessment SA (NOLOCK)
	WHERE SA.EndDate IS NOT NULL
END