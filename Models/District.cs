using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Finance_task2.Models
{
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Index(0)]
        [DisplayName("Name")]
        public string Name
        {
            get; set;
        }
        
        [ForeignKey("RegionId")]
        public virtual Region region { get; set; }
        public int? RegionId { get; set; }

        public List<Users> Users { get; set; }

    }
}
