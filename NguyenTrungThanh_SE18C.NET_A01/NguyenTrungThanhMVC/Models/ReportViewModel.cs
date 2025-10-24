using BusinessObjects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTrungThanhMVC.Models
{
    public class ReportViewModel
    {
        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7); 

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public List<NewsArticle>? NewsArticles { get; set; }
    }
}