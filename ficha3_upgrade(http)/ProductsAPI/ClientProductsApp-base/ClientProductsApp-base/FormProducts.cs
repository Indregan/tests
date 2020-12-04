using ProductsAPI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO; //Stream
using System.Linq;
using System.Net; //HttpWebRequest
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

//XML
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;


//JavaScriptSerializer --> necessário criar referencia para System.Web.Extensions

namespace ClientProductsApp
{
    public partial class FormProducts : Form
    {

        string baseURI = @"http://localhost:59072/api/products"; //needs to be updated!



        private string Get(string URI)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:59072/api/products");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }

        public FormProducts()
        {
            InitializeComponent();
        }

        private void buttonGetAll_Click(object sender, EventArgs e)
        {
            /* string content = Get(baseURI);

             JavaScriptSerializer jSer = new JavaScriptSerializer(); //converter o Json para estrutura C#
             List<Product> products = new List<Product>();
             products = jSer.Deserialize<List<Product>>(content);

             foreach (Product item in products)
             {
                 String StrLine = "Nome: " + item.Name;
                 richTextBoxShowProducts.AppendText(StrLine + Environment.NewLine);
             }*/

            var client = new RestClient("http://localhost:59072/api/products");


            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            String strBody = response.Content.ToString();
            richTextBoxShowProducts.AppendText(strBody);

        }

        private void buttonGetProductById_Click(object sender, EventArgs e)
        {
            String strID = textBox1.Text;
            var client = new RestClient("http://localhost:59072/api/products" + strID);

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            String strBody = response.Content.ToString();
            //JavaScriptSerializer jSer = new JavaScriptSerializer();
            //Product p = jSer.Deserialize<Product>(strBody);
            //String strOut = "" + p.Id + Environment.NewLine + "Name:";
            textBoxOutput.AppendText(strBody);

        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            String strID = textBoxID.Text;
            String strName = textBoxName.Text;
            String strCategory = textBoxCategory.Text;
            String strPrice = textBoxPrice.Text;

            Product p = new Product();
            p.Id = Convert.ToInt32(strID);
            p.Name = strName;
            p.Category = strCategory;
            p.Price = decimal.Parse(strPrice);

            JavaScriptSerializer jSer = new JavaScriptSerializer();
            String strJsonBody = jSer.Serialize(p);

            var client = new RestClient("http://localhost:59072/api/products");

            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json;charset=utf-8", strJsonBody, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Erro....");
            }
            else
            {
                MessageBox.Show("Guardado...");
            }
        }

        private void buttonPut_Click(object sender, EventArgs e)
        {
            String strID = textBoxID.Text;
            String strName = textBoxName.Text;
            String strCategory = textBoxCategory.Text;
            String strPrice = textBoxPrice.Text;

            Product p = new Product();
            p.Id = Convert.ToInt32(strID);
            p.Name = strName;
            p.Category = strCategory;
            p.Price = decimal.Parse(strPrice);

            JavaScriptSerializer jSer = new JavaScriptSerializer();
            String strJsonBody = jSer.Serialize(p);

            var client = new RestClient("http://localhost:59072/api/products" + strID);

            var request = new RestRequest();
            request.Method = Method.PUT;
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json;charset=utf-8", strJsonBody, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Erro....");
            }
            else
            {
                MessageBox.Show("Alterado...");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            String strID = textBoxID.Text;
            var client = new RestClient("http://localhost:59072/api/products" + strID);

            var request = new RestRequest();
            request.Method = Method.DELETE;
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Erro....");
            }
            else
            {
                MessageBox.Show("Apagado....");
            }

        }

        private void buttonPost_Click_1(object sender, EventArgs e)
        {

        }

        //XML
        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xmlDocument = XDocument.Load(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\Employee.xml"); //PATH TEM DE SER DINAMICO

            XDocument result = new XDocument
                (new XElement("table", new XAttribute("border", 1),
                    new XElement("thead",
                        new XElement("tr",
                            new XElement("th", "Department"),
                            new XElement("th", "Name"),
                            new XElement("th", "Gender"),
                            new XElement("th", "Salary"))),
                    new XElement("tbody",
                        from emp in xmlDocument.Descendants("Employee")
                        select new XElement("tr",
                                        new XElement("td", emp.Attribute("Department").Value),    //new XElement("td", emp.Attribute("Department")?.Value ?? ""),
                                        new XElement("td", emp.Element("Name").Value),
                                        new XElement("td", emp.Element("Gender").Value),
                                        new XElement("td", emp.Element("Salary").Value)))));

            //se file já existe, dizer q já foi criado

            result.Save(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\Employee.htm"); //PATH TEM DE SER DINAMICO

            MessageBox.Show("html criado");
        }

        private void FormProducts_Load(object sender, EventArgs e)
        {

        }
    }

}
