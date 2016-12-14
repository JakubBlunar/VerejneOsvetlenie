using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace VerejneOsvetlenieData.Data.Tables
{
    public class VystupProcedura : IVystup
    {
        public event EventHandler VystupSpracovany;
        public string NazovProcedury { get; private set; }
        private readonly Databaza _databaza;
        public List<string> Columns { get; private set; }
        public string ErrorMessage { get; private set; }
        public ProcedureParameter[] ParametrePreVystup { get; private set; }
        private bool _podporaVysledku;

        public IEnumerable<List<object>> Rows
        {
            get
            {
                if (_result == null)
                    yield break;
                foreach (var row in _result)
                {
                    yield return row;
                }
            }
        }
        private IEnumerable<List<object>> _result;

        public VystupProcedura(string paNazovProcedury, bool paPodporujeVysledok, IEnumerable<string> paCollumnNames, params ProcedureParameter[] paParameters)
        {
            NazovProcedury = paNazovProcedury;
            _databaza = new Databaza();
            Columns = new List<string>();
            Columns.AddRange(paCollumnNames);
            ParametrePreVystup = paParameters;
            _podporaVysledku = paPodporujeVysledok;
        }

        public bool SpustiVystup()
        {
            try
            {
                Vysledok vysledok = _podporaVysledku ? new Vysledok() : null;
                _result = _databaza.RunProcedureWithOutput(NazovProcedury, vysledok, ParametrePreVystup); //.SpecialSelect(NazovProcedury);
                if (vysledok?.JeChyba == true)
                    ErrorMessage = vysledok.Popis;
                return !vysledok?.JeChyba ?? true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
            finally
            {
                OnVystupSpracovany();
            }
        }

        protected virtual void OnVystupSpracovany()
        {
            VystupSpracovany?.Invoke(this, EventArgs.Empty);
        }
    }
}
