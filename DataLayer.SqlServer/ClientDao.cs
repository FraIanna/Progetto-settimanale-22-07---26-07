using DataLayer.DaoInterfaces;
using DataLayer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace DataLayer.SqlServer
{
    internal class ClientDao : BaseDao, IClientDao
    {
        public ClientDao(IConfiguration configuration, ILogger<UserDao> logger) : base(configuration, logger) { }

        private const string INSERT_CLIENT = @"
            INSERT INTO Client (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare) 
            VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";
        private const string DELETE_CLIENT = "DELETE FROM Client WHERE CodiceFiscale = @CodiceFiscale";
        private const string SELECT_CLIENT_BY_ID = "SELECT * FROM Client WHERE CodiceFiscale = @CodiceFiscale";
        private const string SELECT_ALL_CLIENTS = "SELECT * FROM Client";
        private const string UPDATE_CLIENT = @"
            UPDATE Client 
            SET Cognome = @Cognome, Nome = @Nome, Citta = @Citta, Provincia = @Provincia, Email = @Email, 
                Telefono = @Telefono, Cellulare = @Cellulare 
            WHERE CodiceFiscale = @CodiceFiscale";
        public ClientEntity Create(ClientEntity client)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(INSERT_CLIENT, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@CodiceFiscale", client.FiscalCode);
                cmd.Parameters.AddWithValue("@Cognome", client.Surname);
                cmd.Parameters.AddWithValue("@Nome", client.Name);
                cmd.Parameters.AddWithValue("@Citta", client.City);
                cmd.Parameters.AddWithValue("@Provincia", client.Province);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Telefono", client.PhoneNumber);
                cmd.Parameters.AddWithValue("@Cellulare", client.CellNumber);
                cmd.ExecuteNonQuery();
                return client;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception creating client");
                throw;
            }
        }

        public ClientEntity Delete(string clientFiscalCode)
        {
            try
            {
                var old = Get(clientFiscalCode);
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(DELETE_CLIENT, conn);
                cmd.Parameters.AddWithValue("@CodiceFiscale", clientFiscalCode);
                cmd.ExecuteNonQuery();
                return old;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception deleting client with fiscal code = {}", clientFiscalCode);
                throw;
            }
        }

        public ClientEntity Get(string clientFiscalCode)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_CLIENT_BY_ID, conn);
                cmd.Parameters.AddWithValue("@CodiceFiscale", clientFiscalCode);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    return new ClientEntity
                    {
                        FiscalCode = reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                        Surname = reader.GetString(reader.GetOrdinal("Cognome")),
                        Name = reader.GetString(reader.GetOrdinal("Nome")),
                        City = reader.GetString(reader.GetOrdinal("Citta")),
                        Province = reader.GetString(reader.GetOrdinal("Provincia")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PhoneNumber = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                        CellNumber = reader.GetString(reader.GetOrdinal("Cellulare"))
                    };
                throw new Exception("Client not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading client with fiscal code = {}", clientFiscalCode);
                throw;
            }
        }

        public IEnumerable<ClientEntity> GetAll()
        {
            var result = new List<ClientEntity>();
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(SELECT_ALL_CLIENTS, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ClientEntity
                    {
                        FiscalCode = reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                        Surname = reader.GetString(reader.GetOrdinal("Cognome")),
                        Name = reader.GetString(reader.GetOrdinal("Nome")),
                        City = reader.GetString(reader.GetOrdinal("Citta")),
                        Province = reader.GetString(reader.GetOrdinal("Provincia")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PhoneNumber = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                        CellNumber = reader.GetString(reader.GetOrdinal("Cellulare"))
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception reading all clients");
                throw;
            }
        }

        public ClientEntity Update(string codiceFiscale, ClientEntity client)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                using var cmd = new SqlCommand(UPDATE_CLIENT, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@CodiceFiscale", client.FiscalCode);
                cmd.Parameters.AddWithValue("@Cognome", client.Surname);
                cmd.Parameters.AddWithValue("@Nome", client.Name);
                cmd.Parameters.AddWithValue("@Citta", client.City);
                cmd.Parameters.AddWithValue("@Provincia", client.Province);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Telefono", client.PhoneNumber);
                cmd.Parameters.AddWithValue("@Cellulare", client.CellNumber);
                cmd.ExecuteNonQuery();
                return client;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception updating client with fiscal code = {CodiceFiscale}", client.FiscalCode);
                throw;
            }
        }
    }
}
