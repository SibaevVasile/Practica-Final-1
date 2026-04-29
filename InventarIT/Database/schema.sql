-- =============================================
-- InventarIT — Schema baza de date
-- SQL Server Express
-- =============================================

IF EXISTS (SELECT name FROM sys.databases 
           WHERE name = 'InventarITDB')
    DROP DATABASE InventarITDB;
GO

CREATE DATABASE InventarITDB;
GO

USE InventarITDB;
GO

-- =============================================
-- Tabel: Angajat
-- =============================================
CREATE TABLE Angajat (
    IdAngajat    INT            PRIMARY KEY IDENTITY(1,1),
    Nume         NVARCHAR(100)  NOT NULL,
    Prenume      NVARCHAR(100)  NOT NULL,
    Departament  NVARCHAR(100)  NOT NULL,
    Email        NVARCHAR(150)  NOT NULL,
    Telefon      NVARCHAR(20)   NULL,
    CONSTRAINT UQ_Angajat_Email UNIQUE (Email)
);
GO

-- =============================================
-- Tabel: Echipament
-- =============================================
CREATE TABLE Echipament (
    IdEchipament INT            PRIMARY KEY IDENTITY(1,1),
    Denumire     NVARCHAR(200)  NOT NULL,
    Tip          NVARCHAR(100)  NOT NULL,
    NumarSerie   NVARCHAR(100)  NOT NULL,
    Producator   NVARCHAR(100)  NOT NULL,
    Valoare      DECIMAL(10,2)  NOT NULL,
    Status       NVARCHAR(50)   NOT NULL DEFAULT 'Disponibil',
    CONSTRAINT UQ_Echipament_NumarSerie UNIQUE (NumarSerie),
    CONSTRAINT CK_Echipament_Valoare    CHECK  (Valoare > 0)
);
GO

-- =============================================
-- Tabel: Atribuire
-- =============================================
CREATE TABLE Atribuire (
    IdAtribuire   INT           PRIMARY KEY IDENTITY(1,1),
    IdEchipament  INT           NOT NULL,
    IdAngajat     INT           NOT NULL,
    DataAtribuire DATE          NOT NULL DEFAULT GETDATE(),
    DataReturnare DATE          NULL,
    Observatii    NVARCHAR(500) NULL,
    CONSTRAINT FK_Atribuire_Echipament
        FOREIGN KEY (IdEchipament)
        REFERENCES Echipament(IdEchipament),
    CONSTRAINT FK_Atribuire_Angajat
        FOREIGN KEY (IdAngajat)
        REFERENCES Angajat(IdAngajat),
    CONSTRAINT UQ_EchipamentActiv
        UNIQUE (IdEchipament, DataReturnare)
);
GO

-- =============================================
-- Verificare structura tabele
-- =============================================
SELECT 
    t.name        AS Tabel,
    c.name        AS Camp,
    tp.name       AS TipDate,
    c.max_length  AS Lungime,
    c.is_nullable AS PermiteNull
FROM sys.tables t
JOIN sys.columns c  ON t.object_id = c.object_id
JOIN sys.types tp   ON c.user_type_id = tp.user_type_id
WHERE t.name IN ('Angajat', 'Echipament', 'Atribuire')
ORDER BY t.name, c.column_id;
GO