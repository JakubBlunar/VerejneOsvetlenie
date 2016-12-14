using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace VerejneOsvetlenieData.Data.Tables
{
    public interface IVystup
    {
        event EventHandler VystupSpracovany;
        List<string> Columns { get; }
        string ErrorMessage { get; }
        ProcedureParameter[] ParametrePreVystup { get; }
        IEnumerable<List<object>> Rows { get; }
        bool SpustiVystup();
    }
}
