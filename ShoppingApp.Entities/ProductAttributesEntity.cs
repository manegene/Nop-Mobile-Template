using System.Collections.Generic;

namespace ShoppingApp.Entities
{
    public class ProductAttributesEntity
    {
        //Attribute value eg, maroon,black,14
        public List<string> AttributesValue2 { get; set; }

        //Attribute name. eg color, size
        public string AttributesValue { get; set; }

        /// <summary>
        /// Gets or seys the product id.
        /// </summary>
        public int Id { get; set; }

        //grouping by id
        public int Mappingid { get; set; }

        public string Baseattribute { get; set; }


        public virtual ProductEntity Product { get; set; }



    }
}
