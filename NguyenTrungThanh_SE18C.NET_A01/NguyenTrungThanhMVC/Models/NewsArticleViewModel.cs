using BusinessObjects;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NguyenTrungThanhMVC.Models
{
    public class NewsArticleViewModel
    {
        public string? NewsArticleId { get; set; }

        [Required(ErrorMessage = "News title is required.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 200 characters.")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required.")]
        [StringLength(255)]
        public string Headline { get; set; }

        [Required(ErrorMessage = "News content is required.")]
        public string NewsContent { get; set; }

        public bool NewsStatus { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category")]
        public short CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

        public List<Tag>? AllTags { get; set; }

        public List<int>? SelectedTagIds { get; set; }
    }
}