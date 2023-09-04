namespace ShopSystem.Models
{
    public class DataModel
    {
        public int Номер { get; set; }
        public string Штрихкод { get; set; }
        public string Категория { get; set; }
        public string Подкатегория { get; set; }
        public string Продукт { get; set; }
        public double Прибывшая { get; set; }
        public double Текущая { get; set; }
        public int Количство { get; set; }
        public string Магазин { get; set; }
    }
}
