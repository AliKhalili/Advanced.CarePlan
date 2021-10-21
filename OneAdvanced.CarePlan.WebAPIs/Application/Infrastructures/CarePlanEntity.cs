using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures
{
    public class CarePlanEntity
    {
        public DateTime ModifiedDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarePlanId { get; set; }
        public string Title { get; set; }
        public string PatientName { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public string Reasons { get; set; }
        public string Actions { get; set; }
        public string Frequency { get; set; }
        public bool Completed { get; set; }
        public DateTime? EndDate { get; set; }
        public string Outcomes { get; set; }
    }
}