using InventarIT.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventarIT.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = GetConnectionString();
        }

        private static string GetConnectionString()
        {
            return @"Data Source=SIBAEV-LEGION\SQLEXPRESS;Initial Catalog=InventarITDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;Packet Size=4096;Command Timeout=0";
        }

        public SqlConnection GetConnection()
            => new SqlConnection(_connectionString);

        public bool TesteazaConexiunea()
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                return true;
            }
            catch { return false; }
        }

        // Echipamente
        public List<Echipament> GetAllEchipamente()
        {
            var lista = new List<Echipament>();
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(
                    "SELECT IdEchipament, Denumire, Tip, " +
                    "NumarSerie, Producator, Valoare, Status " +
                    "FROM Echipament ORDER BY Denumire", con);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Echipament
                    {
                        IdEchipament = reader.GetInt32(0),
                        Denumire = reader.GetString(1),
                        Tip = reader.GetString(2),
                        NumarSerie = reader.GetString(3),
                        Producator = reader.GetString(4),
                        Valoare = reader.GetDecimal(5),
                        Status = reader.GetString(6)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la citire echipamente:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return lista;
        }

        // Angajati
        public List<Angajat> GetAllAngajati()
        {
            var lista = new List<Angajat>();
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(
                    "SELECT IdAngajat, Nume, Prenume, " +
                    "Departament, Email, Telefon " +
                    "FROM Angajat ORDER BY Nume", con);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Angajat
                    {
                        IdAngajat = reader.GetInt32(0),
                        Nume = reader.GetString(1),
                        Prenume = reader.GetString(2),
                        Departament = reader.GetString(3),
                        Email = reader.GetString(4),
                        Telefon = reader.IsDBNull(5)
                                      ? string.Empty
                                      : reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la citire angajați:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return lista;
        }

        // Atribuiri (cu JOIN)
        public List<Atribuire> GetAllAtribuiri()
        {
            var lista = new List<Atribuire>();
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(@"
                    SELECT
                        at.IdAtribuire,
                        at.IdEchipament,
                        at.IdAngajat,
                        at.DataAtribuire,
                        at.DataReturnare,
                        at.Observatii,
                        a.Nume + ' ' + a.Prenume AS NumeAngajat,
                        e.Denumire               AS DenumireEchipament,
                        e.NumarSerie,
                        e.Valoare
                    FROM Atribuire at
                    JOIN Angajat    a ON at.IdAngajat    = a.IdAngajat
                    JOIN Echipament e ON at.IdEchipament = e.IdEchipament
                    ORDER BY at.DataAtribuire DESC", con);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Atribuire
                    {
                        IdAtribuire = reader.GetInt32(0),
                        IdEchipament = reader.GetInt32(1),
                        IdAngajat = reader.GetInt32(2),
                        DataAtribuire = reader.GetDateTime(3),
                        DataReturnare = reader.IsDBNull(4)
                                               ? null
                                               : reader.GetDateTime(4),
                        Observatii = reader.IsDBNull(5)
                                               ? string.Empty
                                               : reader.GetString(5),
                        NumeAngajat = reader.GetString(6),
                        DenumireEchipament = reader.GetString(7),
                        NumarSerie = reader.GetString(8),
                        ValoareEchipament = reader.GetDecimal(9)
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la citire atribuiri:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            return lista;
        }
        // CRUD Angajat
        public void AddAngajat(Angajat a)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(@"
            INSERT INTO Angajat
                (Nume, Prenume, Departament, Email, Telefon)
            VALUES
                (@Nume, @Prenume, @Departament, @Email, @Telefon)",
                    con);
                cmd.Parameters.AddWithValue("@Nume", a.Nume);
                cmd.Parameters.AddWithValue("@Prenume", a.Prenume);
                cmd.Parameters.AddWithValue("@Departament", a.Departament);
                cmd.Parameters.AddWithValue("@Email", a.Email);
                cmd.Parameters.AddWithValue("@Telefon", a.Telefon);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la adăugare angajat:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void UpdateAngajat(Angajat a)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(@"
            UPDATE Angajat SET
                Nume        = @Nume,
                Prenume     = @Prenume,
                Departament = @Departament,
                Email       = @Email,
                Telefon     = @Telefon
            WHERE IdAngajat = @Id", con);
                cmd.Parameters.AddWithValue("@Nume", a.Nume);
                cmd.Parameters.AddWithValue("@Prenume", a.Prenume);
                cmd.Parameters.AddWithValue("@Departament", a.Departament);
                cmd.Parameters.AddWithValue("@Email", a.Email);
                cmd.Parameters.AddWithValue("@Telefon", a.Telefon);
                cmd.Parameters.AddWithValue("@Id", a.IdAngajat);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la modificare angajat:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void DeleteAngajat(int id)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(
                    "DELETE FROM Angajat WHERE IdAngajat = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la ștergere angajat:\n{ex.Message}\n\n" +
                    "Verificați că angajatul nu are atribuiri active.",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // CRUD Echipament
        // =============================================
        public void AddEchipament(Echipament e)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(@"
            INSERT INTO Echipament
                (Denumire, Tip, NumarSerie,
                 Producator, Valoare, Status)
            VALUES
                (@Denumire, @Tip, @NumarSerie,
                 @Producator, @Valoare, @Status)", con);
                cmd.Parameters.AddWithValue("@Denumire", e.Denumire);
                cmd.Parameters.AddWithValue("@Tip", e.Tip);
                cmd.Parameters.AddWithValue("@NumarSerie", e.NumarSerie);
                cmd.Parameters.AddWithValue("@Producator", e.Producator);
                cmd.Parameters.AddWithValue("@Valoare", e.Valoare);
                cmd.Parameters.AddWithValue("@Status", e.Status);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la adăugare echipament:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void UpdateEchipament(Echipament e)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmd = new SqlCommand(@"
            UPDATE Echipament SET
                Denumire   = @Denumire,
                Tip        = @Tip,
                NumarSerie = @NumarSerie,
                Producator = @Producator,
                Valoare    = @Valoare,
                Status     = @Status
            WHERE IdEchipament = @Id", con);
                cmd.Parameters.AddWithValue("@Denumire", e.Denumire);
                cmd.Parameters.AddWithValue("@Tip", e.Tip);
                cmd.Parameters.AddWithValue("@NumarSerie", e.NumarSerie);
                cmd.Parameters.AddWithValue("@Producator", e.Producator);
                cmd.Parameters.AddWithValue("@Valoare", e.Valoare);
                cmd.Parameters.AddWithValue("@Status", e.Status);
                cmd.Parameters.AddWithValue("@Id", e.IdEchipament);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la modificare echipament:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        public void DeleteEchipament(int id)
        {
            try
            {
                using var con = GetConnection();
                con.Open();

                // Verifica atribuiri active
                var cmdVerif = new SqlCommand(@"
            SELECT COUNT(*) FROM Atribuire
            WHERE IdEchipament = @Id
            AND DataReturnare IS NULL", con);
                cmdVerif.Parameters.AddWithValue("@Id", id);
                int atribuiriActive = (int)cmdVerif.ExecuteScalar();

                if (atribuiriActive > 0)
                {
                    MessageBox.Show(
                        "Nu se poate șterge echipamentul!\n" +
                        "Există atribuiri active asociate.\n\n" +
                        "Returnați echipamentul înainte de ștergere.",
                        "Ștergere imposibilă",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var cmd = new SqlCommand(@"
            DELETE FROM Atribuire
            WHERE IdEchipament = @Id;
            DELETE FROM Echipament
            WHERE IdEchipament = @Id;", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la ștergere echipament:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        // CRUD Atribuire
        public void AddAtribuire(
            int idEchipament, int idAngajat,
            DateTime dataAtribuire, string status,
            string observatii)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmdInsert = new SqlCommand(@"
            INSERT INTO Atribuire
                (IdEchipament, IdAngajat,
                 DataAtribuire, Observatii)
            VALUES
                (@IdEchipament, @IdAngajat,
                 @DataAtribuire, @Observatii)", con);
                cmdInsert.Parameters.AddWithValue(
                    "@IdEchipament", idEchipament);
                cmdInsert.Parameters.AddWithValue(
                    "@IdAngajat", idAngajat);
                cmdInsert.Parameters.AddWithValue(
                    "@DataAtribuire", dataAtribuire);
                cmdInsert.Parameters.AddWithValue(
                    "@Observatii",
                    string.IsNullOrEmpty(observatii)
                    ? DBNull.Value : observatii);
                cmdInsert.ExecuteNonQuery();
                var cmdUpdate = new SqlCommand(@"
            UPDATE Echipament
            SET Status = 'Atribuit'
            WHERE IdEchipament = @Id", con);
                cmdUpdate.Parameters.AddWithValue(
                    "@Id", idEchipament);
                cmdUpdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la adăugare atribuire:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void DeleteAtribuire(int idAtribuire)
        {
            try
            {
                using var con = GetConnection();
                con.Open();
                var cmdGet = new SqlCommand(@"
            SELECT IdEchipament, DataReturnare
            FROM Atribuire
            WHERE IdAtribuire = @Id", con);
                cmdGet.Parameters.AddWithValue("@Id", idAtribuire);

                int idEchipament = 0;
                bool esteActiva = false;

                using (var r = cmdGet.ExecuteReader())
                {
                    if (r.Read())
                    {
                        idEchipament = r.GetInt32(0);
                        esteActiva = r.IsDBNull(1);
                    }
                }
                var cmdDel = new SqlCommand(@"
            DELETE FROM Atribuire
            WHERE IdAtribuire = @Id", con);
                cmdDel.Parameters.AddWithValue("@Id", idAtribuire);
                cmdDel.ExecuteNonQuery();
                if (esteActiva && idEchipament > 0)
                {
                    var cmdUpd = new SqlCommand(@"
                UPDATE Echipament
                SET Status = 'Disponibil'
                WHERE IdEchipament = @Id", con);
                    cmdUpd.Parameters.AddWithValue("@Id", idEchipament);
                    cmdUpd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Eroare la anulare atribuire:\n{ex.Message}",
                    "Eroare", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}