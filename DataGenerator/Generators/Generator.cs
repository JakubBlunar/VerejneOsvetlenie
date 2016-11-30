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
    }
}
