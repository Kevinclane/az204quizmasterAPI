using az204quizmasterAPI.Models.Entities;

namespace az204quizmasterAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<QA> QAs { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<ActiveQA> ActiveQAs { get; set; }
    }
}
