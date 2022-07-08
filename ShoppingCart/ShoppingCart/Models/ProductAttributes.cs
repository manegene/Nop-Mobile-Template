namespace habahabamall.Models
{
    public class ProductAttributes
    {
        //Attribute Id
        public int Id { get; set; }

        //Attribute name.eg, color,size
        public string AttributesValue { get; set; }

        public string Baseattribute { get; set; }


        //group by mapping id
        public int Mappingid { get; set; }
    }
}
