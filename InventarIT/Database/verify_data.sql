USE InventarITDB;
GO

-- Numar inregistrari per tabel
SELECT 'Angajat'    AS Tabel, COUNT(*) AS NrInregistrari FROM Angajat
UNION ALL
SELECT 'Echipament' AS Tabel, COUNT(*) AS NrInregistrari FROM Echipament
UNION ALL
SELECT 'Atribuire'  AS Tabel, COUNT(*) AS NrInregistrari FROM Atribuire;
GO

-- Atribuiri active vs returnate
SELECT
    CASE WHEN DataReturnare IS NULL 
         THEN 'Activa' 
         ELSE 'Returnata' 
    END              AS StatusAtribuire,
    COUNT(*)         AS NrAtribuiri
FROM Atribuire
GROUP BY CASE WHEN DataReturnare IS NULL 
              THEN 'Activa' 
              ELSE 'Returnata' 
         END;
GO

-- Echipamente per tip
SELECT
    Tip          AS TipEchipament,
    COUNT(*)     AS Numar,
    SUM(Valoare) AS ValoareTotala
FROM Echipament
GROUP BY Tip
ORDER BY ValoareTotala DESC;
GO