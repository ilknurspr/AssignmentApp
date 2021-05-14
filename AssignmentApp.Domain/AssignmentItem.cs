using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AssignmentApp.Domain
{
    public class AssignmentItem : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Assignment Name")]
        public string AssignmentName { get; set; }
        [Display(Name = "Is completed?")]
        public bool IsCompleted { get; set; } = false;
    }
}
