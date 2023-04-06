namespace ls.Models
{
    public class Tarif
    {
        public int Id { get; set; } //Идентификатор записи (первичный ключ)
        public string Name { get; set; } //Наименование тарифа
        public double Val { get; set; } //Величина тарифа
    }
}
