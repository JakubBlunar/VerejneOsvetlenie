using System.Collections.Generic;
using Db;

namespace VerejneOsvetlenieData.Data.Interfaces
{
    public abstract class SqlEntita
    {
        public abstract bool Update();
        public abstract bool Insert();
        public abstract bool Drop();

        /// <summary>
        /// entita sa načíta podľa svojho id
        /// </summary>
        /// <param name="paIdEntity"></param>
        /// <returns></returns>
        public virtual bool SelectPodlaId(params object[] paIdEntity)
        {
            var atribut = SqlClassAttribute.ExtractSqlClassAttribute(this);
            var db = new Databaza();
            var iterator =
                db.SpecialSelect(
                    $"select * from {atribut.TableName} where {string.Format(atribut.TableKeyContraint, paIdEntity)}");
            var enumerator = iterator.GetEnumerator();
            if (enumerator.MoveNext())
            {
                MapujRiadokSelectu(enumerator.Current, this);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Funguje to iba na selecty bez aliasov alebo alias musí byť ako názov stĺpca
        /// </summary>
        /// <returns></returns>
        public static SqlEntita MapujRiadokSelectu<TData>(Dictionary<string, object> paRiadok, TData paExistujucaEntita = null) where TData : SqlEntita
        {
            var entita = paExistujucaEntita ?? System.Activator.CreateInstance<TData>();
            var props = typeof(TData).GetProperties();
            foreach (var propertyInfo in props)
            {
                var atribut = SqlClassAttribute.ExtractSqlClassAttribute(propertyInfo);
                if (atribut != null && paRiadok.ContainsKey(atribut.ColumnName))
                {
                    var hodnota = System.Convert.ChangeType(paRiadok[atribut.ColumnName], propertyInfo.PropertyType);
                    propertyInfo.SetValue(entita, hodnota);
                }
            }
            return entita;
        }
    }
}
