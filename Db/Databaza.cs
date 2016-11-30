using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Db
{
    public class Databaza
    {
        private static OracleConnection _conn;

        private static OracleConnection ActiveConnection
        {
            get
            {
                if (_conn == null)
                {
                    _conn = GetDbConnection();
                    _conn.Open();
                }
                return _conn;
            } 

        }

        public List<string> GetOsoby()
        {
            var result = new List<string>();

            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = "SELECT * FROM os_udaje";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string r = reader["rod_cislo"] + ", " + reader["meno"] + ", " + reader["priezvisko"];
                result.Add(r);
            }
            return result;
        }

        public List<string> GetTechnici()
        {
            var result = new List<string>();

            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = "SELECT * FROM s_technik";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string r = reader["rodne_cislo"] + ", " + reader["meno"] + ", " + reader["priezvisko"];
                result.Add(r);
            }
            return result;
        }



        public void InsertTechnik(string paRc, string paMeno, string paPriezvisko)
        {
            var sql = $"INSERT INTO s_technik VALUES ('{paRc}','{paMeno}','{paPriezvisko}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public void InsertUlica(int paId, string paNazov, string paMesto)
        {
            var sql = $"INSERT INTO s_technik VALUES ('{paId}','{paNazov}','{paMesto}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }


        private static OracleConnection GetDbConnection()
        {
            string host = "asterix.fri.uniza.sk";
            int port = 1521;
            string sid = "orcl.fri.uniza.sk";
            string user = "blunar3";
            string password = "123456";

            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+ host+")(PORT="+port+")))(CONNECT_DATA=(SERVICE_NAME="+ sid+"))); User Id="+ user+"; Password="+password+";";
            return new OracleConnection
            {
                ConnectionString = connString
            };
        }
    }
    

}
