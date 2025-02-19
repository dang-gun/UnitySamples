
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace StartSample
{
    public class StartSample_SQLiteContextFactory 
        : EFCoreSQLiteBundle.SQLiteContextFactory<StartSample_SQLiteContext>
    {
        protected override string DesignTimeDataSource 
            => "replace it with path to design time database";

        public StartSample_SQLiteContextFactory() 
            : base(UnityEngine.Application.persistentDataPath, "data.db") 
        { 
        }

        protected override StartSample_SQLiteContext InternalCreateDbContext(
            DbContextOptions<StartSample_SQLiteContext> optionsBuilder)
            => new StartSample_SQLiteContext(optionsBuilder);

    }
}