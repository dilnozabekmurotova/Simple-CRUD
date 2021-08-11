using System;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance_task2.Models
{
    public class Region
    {
        public Region(int id, string name)
        {
            this.id = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Index(0)]
        [DisplayName("Name")]
        public string Name
        {
            get; set;
        }

        public List<District> District { get; set; }
        public List<Users> Users { get; set; }
    }
}
