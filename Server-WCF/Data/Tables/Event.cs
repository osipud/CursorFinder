using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WCF.Data.Tables
{
    [Table("Events")]

    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EventId { get; set; }

        [Unique]
        [Required]

        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public int cX { get; set; }
        public int cY { get; set; }

    }
}
