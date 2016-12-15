using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using PropertyChanged;

namespace VerejneOsvetlenieData.Data.Tables
{
    [ImplementPropertyChanged]
    public class VystupSelect : IVystup
    {
        public event EventHandler VystupSpracovany;
        public string SelectString { get; private set; }
        private readonly Databaza _databaza;
        public List<string> Columns { get; private set; }
        public string ErrorMessage { get; private set; }
        public ProcedureParameter[] ParametrePreVystup => null;
        public string KlucovyStlpec { get; set; }


        public IEnumerable<List<object>> Rows
        {
            get
            {
                if (_result == null)
                    yield break;
                foreach (var row in _result)
                {
                    yield return row.Values.ToList();
                }
            }
        }
        private IEnumerable<Dictionary<string, object>> _result;

        public VystupSelect(string paSelectString, params string[] paCollumnNames)
        {
            SelectString = paSelectString;
            _databaza = new Databaza();
            Columns = new List<string>();
            Columns.AddRange(paCollumnNames);
        }

        private bool SelectData()
        {
            try
            {
                var result = _databaza.SpecialSelect(SelectString);
                if (!Columns.Any())
                {
                    foreach (var row in result)
                    {
                        Columns = row.Keys.ToList();
                        break;
                    }
                }
                _result = result;
                return true;
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

        public bool SpustiVystup()
        {
            return this.SelectData();
        }


        protected virtual void OnVystupSpracovany()
        {
            VystupSpracovany?.Invoke(this, EventArgs.Empty);
        }
    }
}
