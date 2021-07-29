using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Patronymic { get; set; }

        public string Image { get; set; }

        [Display(Name = "Community Type")]
        public int CommunityId { get; set; }

        [ForeignKey("CommunityId")]
        public virtual Community Community { get; set; }
    }
}
