using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LstProf.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Summary { get; set; }

        [Required]
        [MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string MetaTitle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? MetaDescription { get; set; }

        [Required]
        [MaxLength(500)]
        public string FeaturedImageUrl { get; set; } = "/images/Blogs/placeholder.svg";


        // Foreign Key for Category
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Foreign Key for Author/User
        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
        public User? Author { get; set; }

        // Status: 0 = Draft, 1 = Published, etc.
        public int Status { get; set; } = 0;

        // Timestamps
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
        public DateTime? PublishedAt { get; set; }

        // Scheduling
        public DateTime? ScheduledAt { get; set; }

        // Metrics
        public int ViewCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int? ReadingTime { get; set; } // in minutes

        // Flags
        public bool IsFeatured { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        // Navigation properties
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
