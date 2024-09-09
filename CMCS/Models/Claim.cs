using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMCS.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }

        [Required]
        public int LecturerID { get; set; }

        [Required]
        public string Module { get; set; }  

        [Required]
        public decimal HoursWorked { get; set; }

        [Required]
        public decimal TotalClaimAmount { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string Status { get; set; }

        // Add ManagerID
        public int? ManagerID { get; set; }

        // Add CoordinatorID
        public int? CoordinatorID { get; set; }

        // Navigation properties
        public Lecturer Lecturer { get; set; }
        public AcademicManager AcademicManager { get; set; }  // Navigation property
        public Coordinator Coordinator { get; set; }
        // Add the SupportingDocumentPath property
        public string SupportingDocumentPath { get; set; }
    }
}
