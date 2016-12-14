using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Db.GeneratorHelpers;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Db
{
    public class Databaza
    {
        private static OracleConnection _conn;

        public static OracleConnection ActiveConnection
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

        public void RunProcedure(string nameOfProcedure, Vysledok vysledok,
            params ProcedureParameter[] procedureParameters)
        {
            using (OracleCommand cmd = Databaza.ActiveConnection.CreateCommand())
            {

                cmd.CommandText = nameOfProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (ProcedureParameter parameter in procedureParameters)
                {
                    cmd.Parameters.Add(parameter.NazovParametra, parameter.DbNazovTypu).Value
                        = parameter.HodnotaParametra;
                }
                if (vysledok != null)
                {
                    cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                    cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok?.NastavChybu("Chyba pri vykonavani procdury");
                }

                if (vysledok != null)
                {
                    if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                        vysledok.Popis = "Success";
                    else
                        vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }
        }

        /// <summary>
        /// Metóda na volanie procedúry
        /// zadáte názov procedúry
        /// ak má out parameter s názvom výsledok tak tam vložte novú inštanciu aby sa Vám vrátil cez ňu výsledok ak je to bez out parametra tak tam dajte null
        /// a potom klasicky vypíšte ktoré parametre s akými typmi a hodnotami tak treba vložiť
        /// </summary>
        /// <param name="nameOfProcedure"></param>
        /// <param name="vysledok">funguje podobne ako out parameter akurát nie je povinný pokiaľ ho procedúra neobsahuje</param>
        /// <param name="procedureParameters"></param>
        /// <returns></returns>
        public IEnumerable<List<string>> RunProcedureWithOutput(string nameOfProcedure, Vysledok vysledok, params ProcedureParameter[] procedureParameters)
        {
            StringBuilder b = new StringBuilder();
            string storedProcedure = nameOfProcedure;

            const string anonymousBlock = "begin dbms_output.get_lines(:1, :2); end;";
            const int numToFetch = 10;
            using (OracleCommand cmd = Databaza.ActiveConnection.CreateCommand())
            {

                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (ProcedureParameter parameter in procedureParameters)
                {
                    cmd.Parameters.Add(parameter.NazovParametra, parameter.DbNazovTypu).Value
                        = parameter.HodnotaParametra;
                }
                if (vysledok != null)
                {
                    cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                    cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok?.NastavChybu("Chyba pri vykonavani procdury");
                    yield break;
                }

                if (vysledok != null)
                {
                    if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                        vysledok.Popis = "Success";
                    else
                        vysledok.NastavChybu("Daco sa nepodarilo");
                }

                cmd.Parameters.Clear();
                var pLines = new OracleParameter("1", OracleDbType.Varchar2, numToFetch, "", ParameterDirection.Output)
                {
                    CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                    ArrayBindSize = new int[numToFetch]
                };

                for (var i = 0; i < numToFetch; i++)
                {
                    pLines.ArrayBindSize[i] = 32000;
                }

                var pNumlines = new OracleParameter("2", OracleDbType.Decimal, "", ParameterDirection.InputOutput) { Value = numToFetch };
                cmd.CommandText = anonymousBlock;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(pLines);
                cmd.Parameters.Add(pNumlines);
                cmd.ExecuteNonQuery();

                var numLinesFetched = ((OracleDecimal)pNumlines.Value).ToInt32();

                while (numLinesFetched > 0)
                {
                    for (var i = 0; i < numLinesFetched; i++)
                    {
                        var oracleStrings = pLines.Value as OracleString[];
                        var a = string.Empty;
                        if (oracleStrings != null)
                            yield return oracleStrings[i].ToString().Split(';').ToList();
                    }
                    cmd.ExecuteNonQuery();

                    numLinesFetched = ((OracleDecimal)pNumlines.Value).ToInt32();
                }

                pNumlines.Dispose();
                pLines.Dispose();
            }
        }


        /// <summary>
        /// test odchytavania dbms_output.put_line();
        /// </summary>

        public StringBuilder Test()
        {
            StringBuilder b = new StringBuilder();
            string storedProcedure = "vypis";

            const string anonymousBlock = "begin dbms_output.get_lines(:1, :2); end;";
            const int numToFetch = 10;

            using (OracleCommand cmd = Databaza.ActiveConnection.CreateCommand())
            {

                cmd.CommandText = storedProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("rocnik", "char").Value = '1';
                cmd.ExecuteNonQuery(); // start our procedure


                cmd.Parameters.Clear();
                var pLines = new OracleParameter("1", OracleDbType.Varchar2,
                    numToFetch,
                    "",
                    ParameterDirection.Output)
                {
                    CollectionType = OracleCollectionType.PLSQLAssociativeArray,
                    ArrayBindSize = new int[numToFetch]
                };

                for (var i = 0; i < numToFetch; i++)
                {
                    pLines.ArrayBindSize[i] = 32000;
                }

                var pNumlines = new OracleParameter("2",
                    OracleDbType.Decimal,
                    "",
                    ParameterDirection.InputOutput)
                {
                    Value = numToFetch
                };
                cmd.CommandText = anonymousBlock;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(pLines);
                cmd.Parameters.Add(pNumlines);
                cmd.ExecuteNonQuery();

                var numLinesFetched = ((OracleDecimal)pNumlines.Value).ToInt32();

                while (numLinesFetched > 0)
                {
                    for (var i = 0; i < numLinesFetched; i++)
                    {
                        var oracleStrings = pLines.Value as OracleString[];
                        if (oracleStrings != null)
                            b.AppendLine(oracleStrings[i].ToString());
                    }
                    cmd.ExecuteNonQuery();
                    numLinesFetched = ((OracleDecimal)pNumlines.Value).ToInt32();
                }

                pNumlines.Dispose();
                pLines.Dispose();
            }

            return b;
        }


        public DataTable GetAllInfo()
        {
            DataTable dtable;
            using (var adapt = new OracleDataAdapter())
            {
                adapt.SelectCommand = new OracleCommand("SELECT * FROM s_info", ActiveConnection);
                var dset = new DataSet("dset");
                adapt.Fill(dset);
                dtable = dset.Tables[0];
            }
            return dtable;

        }

        public Vysledok UpdateDoplnokStlpu(int idStlpu, int idDoplnku,
            char typDoplnku, string popisDoplnku,
            DateTime datumInstalacie, DateTime? datumDemontaze = null
            )
        {
            var vysledok = new Vysledok();

            string dInstalacie = datumInstalacie.ToString("dd.MM.yyyy HH:mm");
            var dDemontaze = datumDemontaze?.ToString("dd.MM.yyyy HH:mm") ?? "null";

            using (var cmd = new OracleCommand("update_doplnok_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_id_doplnku", "number").Value = idDoplnku;
                cmd.Parameters.Add("pa_typ_doplnku", "char").Value = typDoplnku;
                cmd.Parameters.Add("pa_popis_doplnku", "varchar2").Value = popisDoplnku;
                cmd.Parameters.Add("pa_datum_instalacie", "varchar2").Value = dInstalacie;
                cmd.Parameters.Add("pa_datum_demontaze", "varchar2").Value = dDemontaze;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok VlozDoplnokStlpu(int idStlpu, char typDoplnku, string popisDoplnku, DateTime datumInstalacie, DateTime? datumDemontaze = null)
        {
            var vysledok = new Vysledok();

            string dInstalacie = datumInstalacie.ToString("dd.MM.yyyy HH:mm");
            string dDemontaze = datumDemontaze?.ToString("dd.MM.yyyy HH:mm") ?? "null";

            using (var cmd = new OracleCommand("insert_doplnok_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_typ_doplnku", "char").Value = typDoplnku;
                cmd.Parameters.Add("pa_popis_doplnku", "varchar2").Value = popisDoplnku;
                cmd.Parameters.Add("pa_datum_instalacie", "varchar2").Value = dInstalacie;
                cmd.Parameters.Add("pa_datum_instalacie", "varchar2").Value = dDemontaze;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }
        public Vysledok ZmazTypLampy(int idTypu)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_typ_lampy", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_typu", "number").Value = idTypu;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok ZmazLampuNaStlpe(int idLampy)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_lampa_na_stlpe", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok ZmazTechnika(string rodCislo)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_sluzba", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_rod_cislo_technika", "char").Value = rodCislo;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok ZmazSluzbu(int idSluzby)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_sluzba", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_sluzby", "number").Value = idSluzby;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok ZmazUlicu(int idUlice)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_ulica", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_ulice", "number").Value = idUlice;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok ZmazInfoOStlpe(int idInfa)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("delete_info", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_info", "number").Value = idInfa;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok VlozTechnika(string rodCislo, string meno, string priezvisko)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("insert_technik", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislo;
                cmd.Parameters.Add("pa_meno", "varchar2").Value = meno;
                cmd.Parameters.Add("pa_priezvisko", "varchar2").Value = priezvisko;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }


        public Vysledok UpdateUlica(int idUlice, string nazov, string mesto = null)
        {
            var vysledok = new Vysledok();

            var m = mesto ?? "null";

            using (var cmd = new OracleCommand("update_ulica", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_ulice", "number").Value = idUlice;
                cmd.Parameters.Add("pa_nazov", "varchar2").Value = nazov;
                cmd.Parameters.Add("pa_mesto", "varchar2").Value = m;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok InsertUlica(string nazov, string mesto = null)
        {
            var vysledok = new Vysledok();

            var m = mesto ?? "null";

            using (var cmd = new OracleCommand("insert_ulica", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_nazov", "varchar2").Value = nazov;
                cmd.Parameters.Add("pa_mesto", "varchar2").Value = m;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok InsertTypLampy(char typ, int svietivost)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("insert_typ_lampy", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_typ", "char").Value = typ;
                cmd.Parameters.Add("pa_svietivost", "number").Value = svietivost;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok InsertLampaNaStlpe(int idStlpu, int idTypu, char stav, DateTime datumInstalacie)
        {
            var vysledok = new Vysledok();

            string dInstalacie = datumInstalacie.ToString("dd.MM.yyyy HH:mm");

            using (var cmd = new OracleCommand("update_lampa_na_stlpe", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_id_typu", "number").Value = idTypu;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_datum_instalacie", "varchar2").Value = dInstalacie;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }


        public Vysledok UpdateLampaNaStlpe(int idLampy, int idStlpu, int idTypu, char stav, DateTime datumInstalacie, DateTime? datumDemontaze)
        {
            var vysledok = new Vysledok();

            string dInstalacie = datumInstalacie.ToString("dd.MM.yyyy HH:mm");
            var dDemontaze = datumDemontaze?.ToString("dd.MM.yyyy HH:mm") ?? "null";

            using (var cmd = new OracleCommand("update_lampa_na_stlpe", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;
                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_id_typu", "number").Value = idTypu;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_datum_instalacie", "varchar2").Value = dInstalacie;
                cmd.Parameters.Add("pa_datum_demontaze", "varchar2").Value = dDemontaze;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;

        }

        public Vysledok UpdateTypLampy(int idTypu, int svietivost, char typ)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("update_typ_lampy", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_typu", "number").Value = idTypu;
                cmd.Parameters.Add("pa_typ", "char").Value = typ;
                cmd.Parameters.Add("pa_svietivost", "number").Value = svietivost;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;
        }

        public Vysledok UpdateInfoStlpu(int idZaznamu, int idStlpu, char typ, byte[] data)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("update_info_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_info", "number").Value = idZaznamu;
                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;

                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleDbType = OracleDbType.Blob;
                blobParameter.ParameterName = "pa_data";
                blobParameter.Value = data;
                blobParameter.Size = data.Length;
                cmd.Parameters.Add(blobParameter);
                cmd.Parameters.Add("pa_typ", "char").Value = typ;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;
        }

        public Vysledok VlozInfoStlpu(int idStlpu, char typ, byte[] data)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("vloz_info_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;

                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleDbType = OracleDbType.Blob;
                blobParameter.ParameterName = "pa_data";
                blobParameter.Value = data;
                blobParameter.Size = data.Length;
                cmd.Parameters.Add(blobParameter);
                cmd.Parameters.Add("pa_typ", "char").Value = typ;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }

            return vysledok;
        }



        public Vysledok UpdateTechnik(string rodCislo, string meno, string priezvisko)
        {
            var vysledok = new Vysledok();

            using (var cmd = new OracleCommand("update_technik", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislo;
                cmd.Parameters.Add("pa_meno", "varchar2").Value = meno;
                cmd.Parameters.Add("pa_priezvisko", "varchar2").Value = priezvisko;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }

            }
            return vysledok;
        }


        public Vysledok VlozKontroluStlpu(
            string rodCislotechnika, int idStlpu, string popis,
            string stav, int trvanie, DateTime pDatum)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy HH:mm");

            using (var cmd = new OracleCommand("vlozenie_kontroly_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
                cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }
            return vysledok;
        }

        public Vysledok VlozServisStlpu(
            string rodCislotechnika, int idStlpu, string popis,
            string stav, int trvanie, DateTime pDatum, int cena)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy HH:mm");

            using (var cmd = new OracleCommand("vlozenie_servisu_stlpu", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
                cmd.Parameters.Add("pa_id_stlpu", "number").Value = idStlpu;
                cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
                cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
                cmd.Parameters.Add("pa_cena", "number").Value = cena;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }
            return vysledok;
        }

        public Vysledok VlozKontroluLampy(
            string rodCislotechnika, int idLampy, string popis,
            string stav, int trvanie, DateTime pDatum, int svietivost)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy HH:mm");

            using (var cmd = new OracleCommand("vlozenie_kontroly_lampy", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
                cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;
                cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
                cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
                cmd.Parameters.Add("pa_svietivost", "number").Value = svietivost;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }
            return vysledok;

        }

        public Vysledok VlozServisuLampy(
            string rodCislotechnika, int idLampy, string popis,
            string stav, int trvanie, DateTime pDatum, int cena)
        {
            var vysledok = new Vysledok();

            string datum = pDatum.ToString("dd.MM.yyyy HH:mm");

            using (var cmd = new OracleCommand("vlozenie_servisu_lampy", ActiveConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("pa_rod_cislo", "char").Value = rodCislotechnika;
                cmd.Parameters.Add("pa_id_lampy", "number").Value = idLampy;
                cmd.Parameters.Add("pa_popis", "varchar2").Value = popis;
                cmd.Parameters.Add("pa_stav", "char").Value = stav;
                cmd.Parameters.Add("pa_trvanie", "number").Value = trvanie;
                cmd.Parameters.Add("pa_datum", "varchar2").Value = datum;
                cmd.Parameters.Add("pa_cena", "number").Value = cena;

                cmd.Parameters.Add("vysledok", OracleDbType.Char, 1);
                cmd.Parameters["vysledok"].Direction = ParameterDirection.Output;

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    vysledok.NastavChybu("Chyba pri vykonavani procdury");
                    return vysledok;
                }

                if (cmd.Parameters["vysledok"].Value.ToString().Equals("S"))
                {
                    vysledok.Popis = "Success";
                }
                else
                {
                    vysledok.NastavChybu("Daco sa nepodarilo");
                }
            }
            return vysledok;
        }





        public void ExecuteNonQueryOwn(string sql)
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
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

        public IEnumerable<Dictionary<string, object>> SpecialSelect(string select)
        {
            OracleCommand command = ActiveConnection.CreateCommand();
            string sql = select;
            command.CommandText = sql;

            var reader = command.ExecuteReader();

            var table = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                table.Add(reader.GetName(i), null);
            }
            while (reader.Read())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    table[reader.GetName(i)] = null;
                    table[reader.GetName(i)] = reader[reader.GetName(i)];
                }
                yield return table;
            }
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
