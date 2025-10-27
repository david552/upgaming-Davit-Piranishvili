CREATE DATABASE UpGamingDB;
GO

USE UpGamingDB;
GO

--  CREATE TABLES

CREATE TABLE Authors (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Books (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    AuthorID INT NOT NULL,
    PublicationYear INT NOT NULL,
    FOREIGN KEY (AuthorID) REFERENCES Authors(ID)
);
GO

--  INSERT SAMPLE DATA

INSERT INTO Authors (Name) VALUES
('Robert C. Martin'),
('Jeffrey Richter');
GO

INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES
('Clean Code', 1, 2008),
('CLR via C#', 2, 2012),
('The Clean Coder', 1, 2011);
GO

--  UPDATE SCRIPT (as stored procedure)

CREATE PROCEDURE UpdateBookPublicationYear
    @BookID INT,
    @NewYear INT
AS
BEGIN
    UPDATE Books
    SET PublicationYear = @NewYear
    WHERE ID = @BookID;
END
GO

-- DELETE SCRIPT (as stored procedure)

CREATE PROCEDURE DeleteBookByID
    @BookID INT
AS
BEGIN
    DELETE FROM Books
    WHERE ID = @BookID;
END
GO

-- SELECT SCRIPT (as stored procedure)
-- Get books published after 2010 with author name

CREATE PROCEDURE GetBooksAfter2010
AS
BEGIN
    SELECT b.Title, a.Name AS AuthorName, b.PublicationYear
    FROM Books b
    INNER JOIN Authors a ON b.AuthorID = a.ID
    WHERE b.PublicationYear > 2010;
END
GO

-- Insert book (as stored procedure)

CREATE PROCEDURE InsertBook
    @Title NVARCHAR(200),
    @AuthorID INT,
    @PublicationYear INT
AS
BEGIN
    INSERT INTO Books (Title, AuthorID, PublicationYear)
    VALUES (@Title, @AuthorID, @PublicationYear);
END
GO

-- Execute stored procedure to update the year
EXEC UpdateBookPublicationYear @BookID = 2, @NewYear = 2013;
GO

-- Verify the update
SELECT * FROM Books WHERE ID = 2;

-- Execute stored procedure to delete the book
EXEC DeleteBookByID @BookID = 3;
GO

-- Verify the deletion
SELECT * FROM Books;


-- Execute stored procedure to get books after 2010
EXEC GetBooksAfter2010;
GO

-- Insert a new book into the Books table using the InsertBook stored procedure
EXEC InsertBook @Title = 'The best book', @AuthorID = 2, @PublicationYear = 2019;
GO

SELECT * FROM Books;