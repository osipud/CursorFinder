using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_WCF.Data.Tables
{
    [Table("Users")]

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        
        [Unique]
        [Required]

        public string UserLogin { get; set; }
        public string UserPassword { get; set; }

    }


}
