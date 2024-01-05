SELECT name FROM sys.databases
GO

USE PhoneBookProgram
GO

SELECT table_catalog [database], table_schema [schema], table_name name, table_type type
FROM INFORMATION_SCHEMA.TABLES
GO

SELECT * FROM Blogs
GO

SELECT * FROM Posts
GO
