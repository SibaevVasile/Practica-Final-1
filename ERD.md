# Diagrama Entitate-Relație — InventarIT

## Entități și atribute

ANGAJAT
├── IdAngajat (PK)
├── Nume
├── Prenume
├── Departament
├── Email (UNIQUE)
└── Telefon

ECHIPAMENT
├── IdEchipament (PK)
├── Denumire
├── Tip
├── NumarSerie (UNIQUE)
├── Producator
├── Valoare
└── Status

ATRIBUIRE
├── IdAtribuire (PK)
├── IdEchipament (FK → Echipament)
├── IdAngajat (FK → Angajat)
├── DataAtribuire
├── DataReturnare (nullable)
└── Observatii

## Relații
- Un ANGAJAT poate avea mai multe ATRIBUIRI (1:N)
- Un ECHIPAMENT poate apărea în mai multe ATRIBUIRI (1:N)
- O ATRIBUIRE aparține unui singur ANGAJAT și unui singur ECHIPAMENT