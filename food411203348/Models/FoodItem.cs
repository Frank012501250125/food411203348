using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // 💡 1. 記得在最上面新增這一行！

namespace food411203348.Models
{
    public class FoodItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "食物名稱必填")]
        [Display(Name = "食物名稱")]
        public string Name { get; set; }

        [Display(Name = "分類編號")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "食物種類")]
        [ValidateNever] // 💡 2. 新增這一行：告訴系統新增資料時，不要去驗證這個關聯物件！
        public Category Category { get; set; }

        [Display(Name = "價格 (元)")]
        public int Price { get; set; }

        [Display(Name = "熱量 (大卡)")]
        public int Calories { get; set; }

        [Display(Name = "個人評分 (1-5顆星)")]
        [Range(1, 5, ErrorMessage = "評分必須在 1 到 5 之間")]
        public int Rating { get; set; }
    }
}