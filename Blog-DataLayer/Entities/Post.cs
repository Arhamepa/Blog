using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataLayer.Entities
{
    public class Post:BaseEntity
    {
      
        public int User_Id { get; set; }
        public int Category_Id { get; set; }
        public int? SubCategory_Id { get; set; }
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string Slug { get; set; }
        public string Description { get; set; }
        public int Visit { get; set; }
        public string ImageName { get; set; }
        public bool IsSpecial { get; set; }

        [ForeignKey("User_Id")]
        public User User { get; set; }
        [ForeignKey("Category_Id")]
        public Category Category { get; set; }

        [ForeignKey("SubCategory_Id")]
        public Category SubCategory { get; set; }



    }
}
