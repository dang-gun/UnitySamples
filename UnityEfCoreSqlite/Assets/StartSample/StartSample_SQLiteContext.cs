using UnityEngine;

using Microsoft.EntityFrameworkCore;



namespace StartSample
{
    public class StartSample_SQLiteContext : DbContext
    {
        /// <summary>
        /// 테스트용 데이터
        /// </summary>
        public DbSet<StartSampleDbModel> TestData { get; set; }

        public StartSample_SQLiteContext()
            : base()
        {
        }
        public StartSample_SQLiteContext(DbContextOptions<StartSample_SQLiteContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StartSampleDbModel>().HasData(
                new StartSampleDbModel
                {
                    idStartSampleDbModel = 1,
                    Int1 = 1,
                    Str1 = "test01",
                    Date1 = System.DateTime.Now,
                }
                , new StartSampleDbModel
                {
                    idStartSampleDbModel = 2,
                    Int1 = 2,
                    Str1 = "test02",
                    Date1 = System.DateTime.Now.AddDays(1),
                });
        }
    }
}
