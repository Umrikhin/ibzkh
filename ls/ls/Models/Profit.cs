using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ls.Models
{
    public class Profit
    {
        [Key]
        public int Id { get; set; } //Идентификатор записи (первичный ключ)

        [Required(ErrorMessage = "Заполните месяц")]
        [Display(Name = "Месяц")]
        [Range(1,12, ErrorMessage ="Месяц должен быть от 1 до 12")]
        public int Month { get; set; } //Месяц

        [Required(ErrorMessage = "Заполните вх. сальдо")]
        [Display(Name = "Вх. сальдо")]
        public double InBalance { get; set; } //Вх. сальдо

        [Required(ErrorMessage = "Заполните поле начислено")]
        [Display(Name = "Начислено")]
        public double Accrued { get; set; } //Начислено

        [Display(Name = "Оплачено")]
        [Required(ErrorMessage = "Заполните поле оплачено")]
        public double Pay { get; set; } //Оплачено

        [Display(Name = "Исх. сальдо")]
        [Required(ErrorMessage = "Заполните исх. сальдо")]
        public double OutBalance { get; set; } //Исх. сальдо

        [Required(ErrorMessage = "Заполните год")]
        public int Year { get; set; } //Год

        [Required(ErrorMessage = "Укажите помещение")]
        public int IdRoom { get; set; } //Ссылка на лицевой счет помещения (внешний ключ)

        [ForeignKey("IdRoom")]
        public Room Room { get; set; }
    }
}
