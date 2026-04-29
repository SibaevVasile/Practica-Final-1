-- =============================================
-- InventarIT — Date de test (Seed Data)
-- =============================================

USE InventarITDB;
GO

-- Curata datele existente (ordine inversa FK)
DELETE FROM Atribuire;
DELETE FROM Echipament;
DELETE FROM Angajat;
GO

-- Reseteaza identity corect (urmatorul ID va fi 1)
DBCC CHECKIDENT ('Atribuire',  RESEED, 0) WITH NO_INFOMSGS;
DBCC CHECKIDENT ('Echipament', RESEED, 0) WITH NO_INFOMSGS;
DBCC CHECKIDENT ('Angajat',    RESEED, 0) WITH NO_INFOMSGS;
GO

-- =============================================
-- 6 angajati
-- =============================================
INSERT INTO Angajat (Nume, Prenume, Departament, Email, Telefon) VALUES
('Popescu',    'Alexandru', 'IT',         'a.popescu@firma.md',    '0791111111'),
('Ionescu',    'Maria',     'HR',         'm.ionescu@firma.md',    '0792222222'),
('Gheorghe',   'Andrei',    'Financiar',  'a.gheorghe@firma.md',   '0793333333'),
('Constantin', 'Elena',     'Marketing',  'e.constantin@firma.md', '0794444444'),
('Popa',       'Cristian',  'Management', 'c.popa@firma.md',       '0795555555'),
('Dumitrescu', 'Ana',       'IT',         'a.dumitrescu@firma.md', '0796666666');
GO

-- Verifica ID-urile generate
SELECT IdAngajat, Nume, Prenume FROM Angajat;
GO

-- =============================================
-- 6 echipamente
-- =============================================
INSERT INTO Echipament (Denumire, Tip, NumarSerie, Producator, Valoare, Status) VALUES
('Dell Latitude 5520', 'Laptop',     'SN-LAP-001', 'Dell',     4500.00, 'Atribuit'),
('LG UltraWide 27"',  'Monitor',    'SN-MON-001', 'LG',        950.00, 'Atribuit'),
('Logitech MX Keys',  'Tastatura',  'SN-KEY-001', 'Logitech',  450.00, 'Disponibil'),
('HP LaserJet Pro',   'Imprimanta', 'SN-PRN-001', 'HP',       2200.00, 'Atribuit'),
('iPhone 13 Pro',     'Telefon',    'SN-TEL-001', 'Apple',    4200.00, 'Atribuit'),
('Lenovo ThinkPad X1','Laptop',     'SN-LAP-002', 'Lenovo',   5800.00, 'Disponibil');
GO

-- Verifica ID-urile generate
SELECT IdEchipament, Denumire, Tip FROM Echipament;
GO

-- =============================================
-- 8 atribuiri
-- =============================================
INSERT INTO Atribuire (IdEchipament, IdAngajat, DataAtribuire, DataReturnare, Observatii) VALUES
(1, 1, '2024-01-10', NULL,         'Laptop principal dezvoltator'),
(2, 1, '2024-01-10', '2024-08-31', 'Monitor returnat inainte de realocare'),
(4, 2, '2024-02-01', NULL,         'Imprimanta departament HR'),
(5, 5, '2024-01-15', NULL,         'Telefon de serviciu'),
(3, 3, '2024-01-20', '2024-06-01', 'Returnata la plecare din proiect'),
(6, 4, '2024-03-01', '2024-08-15', 'Returnata dupa training'),
(1, 6, '2023-06-01', '2023-12-31', 'Atribuire expirata'),
(2, 3, '2024-09-01', NULL,         'Monitor atribuit dupa returnare');
GO

-- Verifica atribuiri
SELECT IdAtribuire, IdEchipament, IdAngajat, 
       DataAtribuire, DataReturnare FROM Atribuire;
GO