using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Xml.Serialization;

namespace NonLinearDigitalDemo.Models
{

    /// <remarks/>
    [Serializable()]
    [XmlRoot(ElementName = "product")]
    public partial class Product
    {
        
        private string skuField;

        private string nameField;

        private string imageUrlField;

        private decimal priceField;

        private string observationField;

        /// <remarks/>
        [XmlElement("Sku")]
        public string Sku
        {
            get
            {
                return this.skuField;
            }
            set
            {
                this.skuField = value;
            }
        }

        /// <remarks/>
        [XmlElement("Name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlElement("ImageUrl")]
        public string ImageUrl
        {
            get
            {
                return this.imageUrlField;
            }
            set
            {
                this.imageUrlField = value;
            }
        }

        /// <remarks/>
        [XmlElement("Price")]
        [RegularExpression(@"-?(?:\d*[\,\.])?\d+")]
        public decimal Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        [XmlElement("observation")]
        public string observation
        {
            get
            {
                return this.observationField;
            }
            set
            {
                this.observationField = value;
            }
        }
    }


}