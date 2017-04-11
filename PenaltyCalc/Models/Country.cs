using System;
using System.Data.Entity;

namespace PenaltyCalc.Models
{
    public class Country
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public String Currency { get; set; }
        public float CPUSD { get; set; }

    }
    public class CountryDBContext : DbContext
    {
        public DbSet<Country> Country { get; set; }
    }
}