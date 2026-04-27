# InventarIT — Sistem de Gestionare a Inventarului IT

## Descriere
Aplicație desktop dezvoltată în C# WPF pentru gestionarea 
echipamentelor IT ale unei companii: evidența dispozitivelor, 
a angajaților și a atribuirilor de echipamente.

## Tehnologii
- Limbaj: C# (.NET 8)
- Interfață: WPF (Windows Presentation Foundation)
- Bază de date: SQL Server Express
- Acces BD: Microsoft.Data.SqlClient

## Entități principale
- Echipament — dispozitivele IT gestionate
- Angajat — persoanele cărora li se atribuie echipamente
- Atribuire — legătura dintre angajat și echipament

## Structura proiectului
InventarIT/
├── Models/        ← Echipament.cs, Angajat.cs, Atribuire.cs
├── Data/          ← DatabaseHelper.cs
├── Views/         ← ferestrele WPF (.xaml)
├── ViewModels/    ← logica de prezentare
└── Helpers/       ← validări, utilitare
