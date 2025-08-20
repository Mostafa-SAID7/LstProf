using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LstProf.Models
{
    public class NewsletterSubscriber
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}