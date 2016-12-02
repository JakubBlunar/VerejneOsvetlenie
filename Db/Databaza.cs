using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Db.GeneratorHelpers;
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

        private static OracleConnection GetDbConnection()
        {
            string host = "asterix.fri.uniza.sk";
            int port = 1521;
            string sid = "orcl.fri.uniza.sk";
            string user = "blunar3";
            string password = "123456";

            string connString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + host + ")(PORT=" + port + ")))(CONNECT_DATA=(SERVICE_NAME=" + sid + "))); User Id=" + user + "; Password=" + password + ";";
            return new OracleConnection
            {
                ConnectionString = connString
            };
        }

        public Vysledok UpdateTechnik(string rodCislo, string meno, string priezvisko)
        {
            var vysledok = new Vysledok();

            OracleCommand cmd = new OracleCommand("update_technik", ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislo;
            cmd.Parameters.Add("pa_meno", "varchar2").Value = meno;
            cmd.Parameters.Add("pa_priezvisko", "varchar2").Value = priezvisko;

            cmd.Parameters.Add("vysledok", OracleDbType.Varchar2, 1);
            cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
            {
                vysledok.Popis = "Success";
            }
            else
            {
                vysledok.NastavChybu("Daco sa nepodarilo");
            }
            return vysledok;
        }


        public Vysledok VlozKontroluStlpu(
            string rodCislotechnika, int idStlpu, string popis,
            string stav, int trvanie, DateTime pDatum )
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy hh:mm");

            OracleCommand cmd = new OracleCommand("vlozenie_kontroly_stlpu", ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
            cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
            cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
            cmd.Parameters.Add("pa_stav", "char").Value = stav;
            cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
            cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
            cmd.Parameters.Add("vysledok", OracleDbType.Varchar2, 1);
            cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
            {
                vysledok.Popis = "Success";
            }
            else
            {
                vysledok.NastavChybu("Daco sa nepodarilo");
            }
            return vysledok;
        }

        public Vysledok VlozServisStlpu(
            string rodCislotechnika, int idStlpu, string popis,
            string stav, int trvanie, DateTime pDatum,int cena)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy hh:mm");

            OracleCommand cmd = new OracleCommand("vlozenie_servisu_stlpu", ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
            cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
            cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
            cmd.Parameters.Add("pa_stav", "char").Value = stav;
            cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
            cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
            cmd.Parameters.Add("pa_cena", "number").Value = cena;
            cmd.Parameters.Add("vysledok", OracleDbType.Varchar2, 1);
            cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
            {
                vysledok.Popis = "Success";
            }
            else
            {
                vysledok.NastavChybu("Daco sa nepodarilo");
            }
            return vysledok;
        }

        public Vysledok VlozKontroluLampy(
            string rodCislotechnika, int idLampy, string popis,
            string stav, int trvanie, DateTime pDatum, int svietivost)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy hh:mm");

            OracleCommand cmd = new OracleCommand("vlozenie_kontroly_lampy", ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
            cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;
            cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
            cmd.Parameters.Add("pa_stav", "char").Value = stav;
            cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
            cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
            cmd.Parameters.Add("pa_svietivost", "number").Value = svietivost;
            cmd.Parameters.Add("vysledok", OracleDbType.Varchar2, 1);
            cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
            {
                vysledok.Popis = "Success";
            }
            else
            {
                vysledok.NastavChybu("Daco sa nepodarilo");
            }
            return vysledok;
        }

        public Vysledok VlozServisuLampy(
            string rodCislotechnika, int idLampy, string popis,
            string stav, int trvanie, DateTime pDatum, int cena)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy hh:mm");

            OracleCommand cmd = new OracleCommand("vlozenie_servisu_lampy", ActiveConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
            cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;
            cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
            cmd.Parameters.Add("pa_stav", "char").Value = stav;
            cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
            cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
            cmd.Parameters.Add("pa_cena", "number").Value = cena;
            cmd.Parameters.Add("vysledok", OracleDbType.Varchar2, 1);
            cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
            {
                vysledok.Popis = "Success";
            }
            else
            {
                vysledok.NastavChybu("Daco sa nepodarilo");
            }
            return vysledok;
        }









        // Backup code - niečo z toho treba k generatorom
        #region SelectUkazka
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
                result.Add(reader["rodne_cislo"].ToString());
            }
            return result;
        }
        #endregion
        #region GenerovanieInserty
        public void InsertTechnik(string paRc, string paMeno, string paPriezvisko)
        {
            var sql = $"INSERT INTO s_technik VALUES ('{paRc}','{paMeno}','{paPriezvisko}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public void InsertUlica(int paId, string paNazov, string paMesto)
        {
            var sql = $"INSERT INTO s_ulica VALUES ('{paId}','{paNazov}','{paMesto}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public void InsertLampa(int paId, char paTyp, int paSvietivost)
        {
            var sql = $"INSERT INTO s_lampa VALUES ('{paId}','{paTyp}','{paSvietivost}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public void InsertStlp(int paCislo, int paIdUlice, int paVyska, int paPoradie, string paDatum, char paTyp)
        {
            var sql = $"INSERT INTO s_stlp (cislo, id_ulice, vyska, poradie, datum_instalacie, typ) VALUES " +
                      $"('{paCislo}','{paIdUlice}','{paVyska}','{paPoradie}'," +
                      $"TO_DATE('{paDatum}','dd.mm.yyyy'),'{paTyp}')";
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        public void InsertLampaNaStlpe(int paIdLampy, int paCislo, int paIdTypu, char stav, string paDatIns, string paDatOdinst)
        {
            //var transaction = _conn.BeginTransaction(IsolationLevel.ReadCommitted);

            var sql = $"INSERT INTO s_lampa_na_stlpe VALUES " +
                      $"('{paIdLampy}','{paCislo}','{paIdTypu}','{stav}'," +
                      $"TO_DATE('{paDatIns}','dd.mm.yyyy')";

            if (!paDatOdinst.Equals("NULL"))
            {
                sql += $",TO_DATE('{paDatOdinst}','dd.mm.yyyy'))";
            }
            else
            {
                sql += ",NULL)";
            }

            using (StreamWriter w = File.AppendText("inserty.txt"))
            {
                w.WriteLine(sql + ";");
            }

            //OracleCommand command = ActiveConnection.CreateCommand();
            //command.Transaction = transaction;
            //command.CommandText = sql;
            //command.ExecuteNonQuery();
            //transaction.Commit();
        }

        public void InsertSluzba(int paId, string paRodCislo, string paDatum, string paPopis)
        {
            var transaction = _conn.BeginTransaction(IsolationLevel.ReadCommitted);

            var sql = $"INSERT INTO s_sluzba VALUES " +
                      $"('{paId}','{paRodCislo}','{paDatum}','{paPopis}')";

            OracleCommand command = ActiveConnection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            command.ExecuteNonQuery();
            transaction.Commit();
        }


        #endregion
        #region GenerovanieSelecty

        public Dictionary<int, string> GetLampaDatum()
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = $"SELECT cislo, datum_instalacie FROM s_stlp";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();

            var result = new Dictionary<int, string>();
            while (reader.Read())
            {
                result.Add(int.Parse(reader["cislo"].ToString()), reader["datum_instalacie"].ToString());
            }
            return result;
        }

        public List<StlpGeneratorHelper> StlpySelect()
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = $"SELECT cislo, datum_instalacie FROM s_stlp";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();

            var result = new List<StlpGeneratorHelper>();
            while (reader.Read())
            {
                result.Add(new StlpGeneratorHelper()
                {
                    Cislo = int.Parse(reader["cislo"].ToString()),
                    DatumInstalacie = DateTime.Parse(reader["datum_instalacie"].ToString())
                });
            }
            return result;
        }

        public List<LampaGeneratorHelper> LampySelect()
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = $"SELECT id_lampy, datum_instalacie, datum_demontaze FROM s_lampa_na_stlpe";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();

            var result = new List<LampaGeneratorHelper>();
            while (reader.Read())
            {
                var lampa = new LampaGeneratorHelper()
                {
                    Id = int.Parse(reader["id_lampy"].ToString()),
                    DatumInstalacie = DateTime.Parse(reader["datum_instalacie"].ToString()),
                };
                if (!string.IsNullOrEmpty(reader["datum_demontaze"].ToString()))
                {
                    lampa.DatumOdinstalacie = DateTime.Parse(reader["datum_demontaze"].ToString());
                }
                result.Add(lampa);
            }
            return result;
        }

        public Dictionary<int, DateTime> GetSluzbyDatumy()
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = $"SELECT id_sluzby, datum FROM s_sluzba";
            command.CommandText = sql;

            OracleDataReader reader = command.ExecuteReader();

            var result = new Dictionary<int, DateTime>();
            while (reader.Read())
            {
                result.Add(int.Parse(reader["id_sluzby"].ToString()), DateTime.Parse(reader["datum"].ToString()));
            }
            return result;
        }

        #endregion
    }
}
