CREATE DATABASE JWTTokenHandler

USE JWTTokenHandler

CREATE TABLE [dbo].[User](
[UserID] [int] IDENTITY(1,1) PRIMARY KEY,
[UserName] [nvarchar](30) NOT NULL,
[PASSWORD] [nvarchar](30) NOT NULL
)


CREATE TABLE [dbo].[RefreshToken](
[RefreshTokenID] [int] IDENTITY(1,1) PRIMARY KEY,
[Token] [nvarchar](30) NOT NULL,
[UserID] int,
CONSTRAINT [RefreshToken_User] FOREIGN KEY (UserID)
REFERENCES [dbo].[User] ([UserID])
)

INSERT INTO [dbo].[User] values ('Manoj','123456')

ALTER TABLE [dbo].[RefreshToken]
ALTER COLUMN [Token] [nvarchar](100) NOT NULL
