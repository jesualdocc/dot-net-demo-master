
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APP_Demo__WebAPI_.Models
{
    [NotMapped]
    public class Page
    {
        
        public DrugInfo[] data { get; set; }

        public Metadata metadata { get; set; }

    }

        public class DrugInfo
        {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [JsonPropertyNameAttribute("name_type")]
         public string nameType { get; set; }

        [JsonPropertyNameAttribute("drug_name")]
         public string drugName { get; set; }

        public int PageNumber { get; set; }
      
        }

    public class Metadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string db_published_date { get; set; }
        public int total_pages { get; set; }
        
    }


}









