namespace ls.Models
{
    public class Room
    {
        public int Id { get; set; } //Идентификатор записи (первичный ключ)
        public string NumBill { get; set; } //Номер личевого счета
        public double Area { get; set; } //Площадь
        //Здесь можно добавить и другие свойства, например адрес (код) помещения по ГАР
        public string GAR { get; set; }
    }
}
