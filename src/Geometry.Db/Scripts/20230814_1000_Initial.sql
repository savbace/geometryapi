CREATE TABLE dbo.Rectangles
(
    Id INT NOT NULL IDENTITY(1, 1),
    X FLOAT,
    Y FLOAT,
    Width FLOAT,
    Height FLOAT,
    CONSTRAINT [PK_Rectangles]PRIMARY KEY (Id)
)