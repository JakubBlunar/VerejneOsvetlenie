using System;

namespace VerejneOsvetlenieData.Data
{
    /// <summary>
    /// Atribút na popis poco objektu pre našu DB
    /// </summary>
    public class SqlClassAttribute : Attribute
    {
        /// <summary>
        /// ak je to varchar alebo niečo také tak jeho dĺžka
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// atribút patrí triede
        /// </summary>
        public bool IsTable => !string.IsNullOrEmpty(TableName);

        /// <summary>
        /// atribú patrí property
        /// </summary>
        public bool IsColumn => !string.IsNullOrEmpty(ColumnName);

        /// <summary>
        /// názov stĺpca ktorý replrezentuje property
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// názov tabuľky ktorú replrezentuje trieda
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Názov pre label ak null tak sa nezobrazí
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Ak je potrebné volať hodnotu so špeciálny patternom inak null
        /// </summary>
        public string SpecialFormat { get; set; }

        /// <summary>
        /// ak null tak element nezobrazovať
        /// </summary>
        public bool ShowElement => DisplayName != null;

        public bool IsBitmapImage { get; set; }

        /// <summary>
        /// Názov pre label
        /// </summary>
        public string ElementName => string.IsNullOrEmpty(DisplayName) 
            ? (IsTable ? TableName?.ToLower() : ColumnName?.ToLower() ?? "NOT SET DISPLAY NAME")
            : DisplayName;

        public SqlClassAttribute()
        {
            ColumnName = string.Empty;
            TableName = string.Empty;
            DisplayName = string.Empty;
            SpecialFormat = null;
            IsBitmapImage = false;
        }
    }
}