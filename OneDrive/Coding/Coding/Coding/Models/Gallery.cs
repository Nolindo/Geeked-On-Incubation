using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coding.Models
{
    public class Gallery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }
        [Required]
        public string ImageTitle { get; set; }
        [Required]
        public string ImageDescription { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsActive { get; set; }
        public Collection Categories { get; set; }
        public int CategoryId { get; set; }
        
    }
}