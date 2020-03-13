using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolodCodeFirstSageBookEF.Database
{
    public class Sage
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public Sage() => Books = new List<Book>();

        public override string ToString() => Name;
    }
}