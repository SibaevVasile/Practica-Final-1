USE InventarITDB;
GO

-- Verificare constrangeri
SELECT 
    tc.TABLE_NAME       AS Tabel,
    tc.CONSTRAINT_TYPE  AS TipConstrangere,
    tc.CONSTRAINT_NAME  AS NumeConstrangere
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
ORDER BY tc.TABLE_NAME;
GO

-- Verificare chei externe
SELECT 
    fk.name                   AS NumeFK,
    tp.name                   AS TabelParinte,
    tr.name                   AS TabelReferinta
FROM sys.foreign_keys fk
JOIN sys.tables tp ON fk.parent_object_id   = tp.object_id
JOIN sys.tables tr ON fk.referenced_object_id = tr.object_id;
GO