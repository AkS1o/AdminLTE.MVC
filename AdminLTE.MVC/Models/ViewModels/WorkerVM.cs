using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.MVC.Models.ViewModels
{
    public class WorkerVM
    {
        public Worker Worker { get; set; }
        public IEnumerable<SelectListItem> CommunitiesSelectList { get; set; }
    }
}
