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

        // =============================================
        // Echipamente
        // =============================================
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

        // =============================================
        // Angajati
        // =============================================
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

    }
}