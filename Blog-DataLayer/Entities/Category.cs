﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_DataLayer.Entities
{
    public class Category: BaseEntity
    {
       
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentId { get; set; }

        #region Relation
        [InverseProperty("Category")]
        public  ICollection<Post> Posts { get; set; }
        [InverseProperty("SubCategory")]
        public ICollection<Post> SubCatPosts { get; set; }
        #endregion

    }
}
