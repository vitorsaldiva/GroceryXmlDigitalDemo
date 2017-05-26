using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NonLinearDigitalDemo.Models;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace NonLinearDigitalDemo.Util
{
    public class XmlAccess
    {
        private string _path;
        public XmlAccess(string path)
        {
            _path = path;
        }
        public List<Product> Get()
        {
            var products = LoadData();
            var listOfProducts = products
                .Root
                .Elements()
                .Select(
                    newProduct => new Product
                    {
                        Sku = (string)newProduct.Element("Sku"),
                        Name = (string)newProduct.Element("Name"),
                        ImageUrl = (string)newProduct.Element("ImageUrl"),
                        Price = (decimal)newProduct.Element("Price"),
                        observation = (string)newProduct.Element("observation"),
                    }
                )
                .ToList();

            return listOfProducts;
        }

        public Product GetBySku(string sku)
        {
            var products = LoadData();
            try
            {
                var product = products
                        .Root
                        .Elements()
                        .SingleOrDefault(element => element.Element("Sku").Value == sku);
                if (product == null)
                    return null;
                else
                    return Cast<Product>(product);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(Product product)
        {
            var data = LoadData();
            data
                .Root
                .Add(CastToXElement(product));
            data.Save(_path);
        }

        public void Update(Product product)
        {
            var data = LoadData();
            data
                .Root
                .Elements()
                .Single(element => element.Element("Sku").Value == product.Sku)
                .ReplaceWith(CastToXElement(product));
            data.Save(_path);
        }

        public void Delete(Product product)
        {
            var data = LoadData();
            data
                .Root
                .Elements()
                .Single(element => element.Element("Sku").Value == product.Sku)
                .Remove();
            data.Save(_path);
        }

        public XDocument LoadData()
        {
            return XDocument.Load(_path);
        }

        public T Cast<T>(XElement element)
        {
            var serializer = new XmlSerializer(typeof(T));
            var castedElement = (T)serializer.Deserialize(element?.CreateReader());
            return castedElement;
        }

        public XElement CastToXElement<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, obj);
                return XElement.Parse(stringWriter.ToString());
            }
        }
    }
}