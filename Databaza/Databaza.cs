using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace Databaza
{
    public class Databaza: IDatabaza
    {
        private OracleConnection _conn;

        private OracleConnection ActiveConnection
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
                string r = (string)reader["rod_cislo"] + ", " + (string)reader["meno"] + ", " + (string)reader["priezvisko"];
                result.Add(r);
            }
            return result;
        }


        private static OracleConnection GetDbConnection()
        {
            string host = "asterix.fri.uniza.sk";
            int port = 1521;
            string sid = "orcl.fri.uniza.sk";
            string user = "blunar3";
            string password = "123456";

            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST="+ host+")(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME="+ sid+"))); User Id="+ user+"; Password="+password+";";
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connString;
            return conn;
        }
    }
    

}
