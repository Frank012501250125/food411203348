using System.ComponentModel.DataAnnotations;

namespace food411203348.Models // 記得把「你的專案名稱」改成你實際的專案名稱
{
    public class FoodItem
    {
        [Key] // 設定為主鍵
        public int Id { get; set; }

        [Required(ErrorMessage = "食物名稱必填")]
        [Display(Name = "食物名稱")]
        public string Name { get; set; }

        [Display(Name = "食物種類")] // 例如：主食、甜點、飲料
        public string Category { get; set; }

        [Display(Name = "價格 (元)")]
        public int Price { get; set; }

        [Display(Name = "熱量 (大卡)")]
        public int Calories { get; set; }

        [Display(Name = "個人評分 (1-5顆星)")]
        [Range(1, 5, ErrorMessage = "評分必須在 1 到 5 之間")]
        public int Rating { get; set; }
    }
}