using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolodCodeFirstSageBookEF.Database
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Sage> Sages { get; set; }

        public Book() => Sages = new List<Sage>();

        public override string ToString() => Name;
    }
}