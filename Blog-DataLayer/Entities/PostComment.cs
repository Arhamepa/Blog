using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataLayer.Entities
{
    public class PostComment:BaseEntity
    {

        [Required]
        public string Text { get; set; }
        public int Post_Id { get; set; }
        public int User_Id { get; set; }
        [ForeignKey("Post_Id")]
        public Post Post { get; set; }

        [ForeignKey("User_Id")]
        public User User { get; set; }
    }
}
