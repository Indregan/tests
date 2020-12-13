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
using Newtonsoft.Json;
using ProductsAPI;
using System.Web.Http;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Product = ProductsAPI.Models.Product;


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
            // Load the style sheet.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\generic.xsl"); //TEM DE ABRIR de um path GENERICO

            XPathDocument xDoc = new XPathDocument(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\books.xml");   //TEM DE ABRIR QUALQUER DOC xml
            //XmlTextWriter writer = new XmlTextWriter("outputXML.html", null);

            using (XmlTextWriter writer = new XmlTextWriter("outputXML.html", null))
            {
                // Execute the transform and output the results to a file.
                xslt.Transform(xDoc, null, writer, new XmlUrlResolver());
                writer.Close();
            }

            using (StreamReader stream = new StreamReader("outputXML.html"))
            {
                //ESCREVER PARA UMA PASTA dos outputs !!!!
                File.WriteAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\outputXML.html", stream.ReadToEnd());

                MessageBox.Show("conversão concluida");
            }
            
        }

        //JSON
        private void button2_Click(object sender, EventArgs e)
        {
            string jsonFile = File.ReadAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json"); //TEM DE ABRIR QUALQUER DOC json

            // To convert JSON text contained in string json into an XML node
            XmlDocument xDoc = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonFile, "output"); //using Newtonsoft.Json;

            // Load the style sheet.
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\generic.xsl"); //TEM DE ABRIR de um path GENERICO

            using (XmlTextWriter writer = new XmlTextWriter("outputJSON.html", null))
            {
                // Execute the transform and output the results to a file.
                xslt.Transform(xDoc, null, writer, new XmlUrlResolver());
                writer.Close();
            }

            using (StreamReader stream = new StreamReader("outputJSON.html"))
            {
                //ESCREVER PARA UMA PASTA dos outputs !!!!
                File.WriteAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\outputJSON.html", stream.ReadToEnd());

                MessageBox.Show("conversão concluida");
            }
        }

/*        private void button3_Click(object sender, EventArgs e)
        {

            string postUrl = "http://localhost:59072";

            string jsonFile = File.ReadAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json"); //TEM DE ABRIR QUALQUER DOC json


            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest()
            {
                Resource = postUrl
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");//xml
            request.AddJsonBody(jsonFile);

            var response = client.Post(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Erro....");
                Console.WriteLine(response.Content);
            }
            else
            {
                MessageBox.Show("Guardado...");
                Console.WriteLine(response.Content);
            }
        }*/

        /*        private void button4_Click(object sender, EventArgs e)
                {
                    string postUrl = "http://localhost:59072/jsonEndpoint";

                    string stringXML = "<books><book><title>title</title><author>Tom</author><price>19.95</price></book></books>";


                    IRestClient client = new RestClient();
                    IRestRequest request = new RestRequest()
                    {
                        Resource = postUrl
                    };

                    request.AddHeader("Content-Type", "application/xml");
                    request.AddHeader("Accept", "application/xml");//xml
                    request.AddParameter("XmlBody", stringXML, ParameterType.RequestBody);

                    IRestResponse response = client.Post(request); 

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("Erro....");
                        Console.WriteLine(response.Content);
                    }
                    else
                    {
                        MessageBox.Show("Guardado...");
                        Console.WriteLine(response.Content);
                    }
                }*/

                public string postXMLData()
                {
                    string postUrl = "http://localhost:59072";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);

                    //string stringXML = "<books><book><title>title</title><author>Tom</author><price>19.95</price></book></books>";

                    string xmlFile = File.ReadAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\books.xml");


                    byte[] bytes;
                    bytes = System.Text.Encoding.ASCII.GetBytes(xmlFile);
                    request.ContentType = "text/xml; encoding='utf-8'";
                    request.ContentLength = bytes.Length;
                    request.Method = "POST";
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                    HttpWebResponse response;
                    response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream responseStream = response.GetResponseStream();
                        string responseStr = new StreamReader(responseStream).ReadToEnd();
                        return responseStr;
                    }
                    return null;

                }

                private void button4_Click(object sender, EventArgs e)
                {
                    postXMLData();
                }

        private void button3_Click(object sender, EventArgs e)
        {

            string jsonFile = File.ReadAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json"); //TEM DE ABRIR QUALQUER DOC json

            var client = new RestClient("http://localhost:59072/");

            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonFile);
            //request.Parameters.Clear();
            request.AddParameter("application/json;charset=utf-8", jsonFile, ParameterType.RequestBody);
            //request.AddJsonBody(jsonFile);

            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Erro....");
                Console.WriteLine(response.Content);
            }
            else
            {
                MessageBox.Show("Guardado...");
                Console.WriteLine(response.Content);
            }
        }

        /*        private void button3_Click(object sender, EventArgs e)
                {

                    string postUrl = "http://localhost:59072";

                    string jsonFile = File.ReadAllText(@"C:\Users\tmati\Documents\tests\ficha3_upgrade(http)\ProductsAPI\testFiles\Sample-JSON-file.json"); //TEM DE ABRIR QUALQUER DOC json


                    var client = new RestClient(postUrl);
                    var request = new RestRequest(Method.POST);
                    //var request = new RestRequest("api/{controller}", Method.POST);
                    request.RequestFormat = DataFormat.Json;
                    request.AddParameter("Application/Json", jsonFile, ParameterType.RequestBody);
                    //request.AddUrlSegment("controller", "jsonEndpoint");

                    var response = client.Execute(request);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("Erro....");
                        Console.WriteLine(response.Content);
                    }
                    else
                    {
                        MessageBox.Show("Guardado...");
                        Console.WriteLine(response.Content);
                    }
                }*/
    }

}
