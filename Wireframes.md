# Wireframes — InventarIT

## MainWindow
+------------------------------------------+
|  InventarIT                    [_][□][X] |
+------------------------------------------+
|  [Echipamente] [Angajati] [Atribuiri]    |
|  [Raport]                                |
+------------------------------------------+
|                                          |
|         Continut fereastra activa        |
|         (se schimba la navigare)         |
|                                          |
+------------------------------------------+

## EchipamentView
+------------------------------------------+
|  Gestionare Echipamente                  |
+------------------------------------------+
|  Cauta: [_______________] [Cauta]        |
|  Filtru Tip: [Toate ▼]                   |
+------------------------------------------+
|  Id | Denumire | Tip | Serie | Valoare   |
|-----|----------|-----|-------|-----------|
|  1  | Dell ... | Lap | SN-.. | 4500      |
|  2  | LG  ...  | Mon | SN-.. |  950      |
+------------------------------------------+
|  [Adauga] [Modifica] [Sterge]            |
+------------------------------------------+

## AngajatView
+------------------------------------------+
|  Gestionare Angajati                     |
+------------------------------------------+
|  Cauta: [_______________] [Cauta]        |
+------------------------------------------+
|  Id | Nume | Prenume | Dept | Email      |
|-----|------|---------|------|------------|
|  1  | Pop  | Alex    | IT   | a.pop@...  |
+------------------------------------------+
|  [Adauga] [Modifica] [Sterge]            |
+------------------------------------------+

## AtribuireView
+------------------------------------------+
|  Gestionare Atribuiri                    |
+------------------------------------------+
|  Angajat: [Toti ▼]  Status: [Toate ▼]   |
+------------------------------------------+
|  Id | Angajat | Echipament | Data | St.  |
|-----|---------|------------|------|------|
|  1  | Pop Al  | Dell Lat.  | 01/24| Act  |
+------------------------------------------+
|  [Atribuie] [Returneaza] [Anuleaza]      |
+------------------------------------------+

## RaportView
+------------------------------------------+
|  Raport Sumar Inventar IT                |
+------------------------------------------+
|  Angajat        | Nr.Ech | Valoare Totala|
|-----------------|--------|---------------|
|  Popescu Alex   |   2    |   5450.00 lei |
|  Ionescu Maria  |   1    |   2200.00 lei |
+------------------------------------------+
|  Total echipamente atribuite: X          |
|  Valoare totala inventar:     X lei      |
+------------------------------------------+
|              [Inchide]                   |
+------------------------------------------+

## Flux navigare
MainWindow
├── click "Echipamente" → EchipamentView
├── click "Angajati"   → AngajatView
├── click "Atribuiri"  → AtribuireView
└── click "Raport"     → RaportView (fereastra separata)