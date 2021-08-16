using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.MVC.Models.ViewModels
{
    public class WorkerSelectListVM
    {
        [Display(Name = "Worker")]
        public int? WorkerId { get; set; }
        public Worker Worker { get; set; }
        public IEnumerable<SelectListItem> CommunitiesSelectList { get; set; }
        public IEnumerable<SelectListItem> WorkersSelectList { get; set; }
    }
}
