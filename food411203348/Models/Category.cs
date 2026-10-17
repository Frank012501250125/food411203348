using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace food411203348.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "分類名稱")]
        public string Name { get; set; }

        // 建立一對多關聯：一個分類會包含很多個食物
        public List<FoodItem> FoodItems { get; set; }
    }
}