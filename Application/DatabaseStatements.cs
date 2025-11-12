using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Trader;

namespace ApplicationWpf
{
    internal class DatabaseStatements
    {
        Connect conn = new Connect();

        public object AddNewUser(object version)
        {
            try
            {
                conn._connection.Open();
                var newVersion = version.GetType().GetProperties();

                string salt = GenerateSalt();
                string hashedPassword = ComputeHmacSha256(newVersion[2].GetValue(version).ToString(), salt);

                string sql = "INSERT INTO `datas`(`Version`, `UserName`, `Password`, `RegTime`, `ModTime`) VALUES (@version,@username,@password,@regtime,@modtime)";

                MySqlCommand cmd = new MySqlCommand(sql, conn._connection);

                cmd.Parameters.AddWithValue("@version", newVersion[0].GetValue(version));
                cmd.Parameters.AddWithValue("@username", newVersion[1].GetValue(version));
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@regtime", newVersion[4].GetValue(version));
                cmd.Parameters.AddWithValue("@modtime", newVersion[5].GetValue(version));

                cmd.ExecuteNonQuery();

                conn._connection.Close();

                return new { message = "New version wasd added successfully!" };
            }
            catch (System.Exception ex)
            {
                return new { message = ex.Message };
            }

        }

        public DataView VersionList()
        {
            try
            {
                conn._connection.Open();
                string sql = "SELECT `Version`, `UserName`, `Password`, `RegTime`, `ModTime` FROM `datas` WHERE 1";

                MySqlCommand cmd = new MySqlCommand(sql, conn._connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn._connection);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                conn._connection.Close();

                return dt.DefaultView;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public string ComputeHmacSha256(string password, string salt)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(salt)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}