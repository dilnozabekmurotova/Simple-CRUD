using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Finance_task2.Models
{
    public class Users
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
        public virtual Region Region { get; set; }
        public int? RegionId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        public int? DistrictId { get; set; }

        public string Mahalla { get; set; }

        [NotMapped]
        public string RegionName { get; set; }

        [NotMapped]
        public string DistrictName { get; set; }
    }
}
