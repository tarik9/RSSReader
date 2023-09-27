using System;
using System.ComponentModel.DataAnnotations;

namespace MinimalRSSReader.Models
{
    public class RSSFeed
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string FeedUrl { get; set; }

        // Add properties for Description and PubDate
        public string Description { get; set; }
        public DateTime PubDate { get; set; }
    }
}
