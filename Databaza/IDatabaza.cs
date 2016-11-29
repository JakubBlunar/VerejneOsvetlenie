using System.Collections.Generic;
using System.Dynamic;
using Oracle.ManagedDataAccess.Client;

namespace Databaza
{
    public interface IDatabaza
    {
        List<string> GetOsoby();
    }
}