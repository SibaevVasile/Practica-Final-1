# Diagrama Entity-Relationship — InventarIT

## Entități și atribute

+------------------+        +------------------+
|     ANGAJAT      |        |   ECHIPAMENT     |
+------------------+        +------------------+
| PK IdAngajat INT |        | PK IdEchipament  |
|    Nume          |        |    Denumire      |
|    Prenume       |        |    Tip           |
|    Departament   |        | UQ NumarSerie    |
| UQ Email         |        |    Producator    |
|    Telefon       |        |    Valoare > 0   |
+------------------+        |    Status        |
         |                  +------------------+
         |                           |
         | 1                         | 1
         |                           |
         N                           N
         +----------+----------+
         |        ATRIBUIRE         |
         +--------------------------+
         | PK IdAtribuire  INT      |
         | FK IdAngajat    INT      |
         | FK IdEchipament INT      |
         |    DataAtribuire DATE    |
         |    DataReturnare DATE    |
         |    Observatii            |
         +--------------------------+

## Relații
- ANGAJAT (1) ────────── (N) ATRIBUIRE
  Un angajat poate primi mai multe echipamente

- ECHIPAMENT (1) ────────── (N) ATRIBUIRE
  Un echipament poate apărea în mai multe atribuiri
  (istoricul atribuirilor)

## Tipuri de date

### Tabel: Angajat
| Camp        | Tip           | Constrangeri        |
|-------------|---------------|---------------------|
| IdAngajat   | INT           | PK, IDENTITY(1,1)   |
| Nume        | NVARCHAR(100) | NOT NULL            |
| Prenume     | NVARCHAR(100) | NOT NULL            |
| Departament | NVARCHAR(100) | NOT NULL            |
| Email       | NVARCHAR(150) | NOT NULL, UNIQUE    |
| Telefon     | NVARCHAR(20)  | NULL                |

### Tabel: Echipament
| Camp         | Tip           | Constrangeri        |
|--------------|---------------|---------------------|
| IdEchipament | INT           | PK, IDENTITY(1,1)   |
| Denumire     | NVARCHAR(200) | NOT NULL            |
| Tip          | NVARCHAR(100) | NOT NULL            |
| NumarSerie   | NVARCHAR(100) | NOT NULL, UNIQUE    |
| Producator   | NVARCHAR(100) | NOT NULL            |
| Valoare      | DECIMAL(10,2) | NOT NULL, CHECK > 0 |
| Status       | NVARCHAR(50)  | NOT NULL            |

### Tabel: Atribuire
| Camp          | Tip           | Constrangeri             |
|---------------|---------------|--------------------------|
| IdAtribuire   | INT           | PK, IDENTITY(1,1)        |
| IdEchipament  | INT           | NOT NULL, FK → Echipament|
| IdAngajat     | INT           | NOT NULL, FK → Angajat   |
| DataAtribuire | DATE          | NOT NULL                 |
| DataReturnare | DATE          | NULL                     |
| Observatii    | NVARCHAR(500) | NULL                     |

## Constrângeri speciale
- UNIQUE (IdEchipament, DataReturnare) pe tabelul Atribuire
  → previne atribuirea aceluiași echipament de două ori 
    în același timp
- CHECK (Valoare > 0) pe tabelul Echipament
  → valoarea unui echipament nu poate fi negativă sau zero

## Ordinea de creare a tabelelor în SQL
1. Angajat     ← nu depinde de nimeni
2. Echipament  ← nu depinde de nimeni
3. Atribuire   ← depinde de Angajat și Echipament (FK)