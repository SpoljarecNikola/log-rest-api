using Log_REST_Api.DataModel;
using Microsoft.EntityFrameworkCore;
using Log_REST_Api.Data;

namespace Log_REST_Api.DatabaseContext
{
    public class LogDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "LogDb");
        }

        public DbSet<Log> Logs { get; set; }

        public static void AddLogsData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<LogDatabaseContext>() ?? throw new InvalidOperationException("Cannot access the database context.");
            
            Seed seed = new();
            List<Log> logs = seed.GetLogs();

            db.Logs.AddRange(logs);

            db.SaveChanges();
        }
    }


}
