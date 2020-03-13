namespace VolodCodeFirstSageBookEF.Database
{
    using System.Data.Entity;

    public class MyModel : DbContext
    {
        public MyModel()
            : base("MyModel")
        {
        }

        public virtual DbSet<Sage> Sages { get; set; }
        public virtual DbSet<Book> Books { get; set; }
    }
}