using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixStationDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoFixStationDatabaseImplement
{
    public class AutoFixStationDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=AutoFixStationDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<Employee> Employees { set; get; }
        public virtual DbSet<ServiceRecord> ServiceRecords { set; get; }
        public virtual DbSet<SparePart> SpareParts { set; get; }
        public virtual DbSet<StoreKeeper> StoreKeepers { set; get; }
        public virtual DbSet<TimeOfWork> TimeOfWorks { set; get; }
        public virtual DbSet<TO> TOs { set; get; }
        public virtual DbSet<TO_Work> TO_Works { set; get; }
        public virtual DbSet<Work> Works { set; get; }
        public virtual DbSet<WorkType> WorkTypes { set; get; }
        public virtual DbSet<WorkType_SparePart> WorkType_SpareParts { set; get; }
    }
}