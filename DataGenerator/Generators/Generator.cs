using System;
using Db;

namespace DataGenerator.Generators
{
    public abstract class Generator
    {
        protected Random _random;
        protected Databaza _database;

        protected Generator()
        {
            _database = new Databaza();
            _random = new Random();
        }

        public abstract void Generate(int paCount);

        public DateTime RandomDay(DateTime paMinDate)
        {
            DateTime start = paMinDate;
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }

        public DateTime RandomDay(DateTime paMinDate, DateTime paMaxDate)
        {
            DateTime start = paMinDate;
            int range = (paMaxDate - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}
