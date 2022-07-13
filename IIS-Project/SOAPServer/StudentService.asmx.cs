using SOAPServer.Models;
using SOAPServer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Services;
using System.Xml;

namespace SOAPServer
{
    /// <summary>
    /// Summary description for StudentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StudentService : WebService
    {
        #region static

        private static readonly string XML_FILEPATH
            = $"{AppDomain.CurrentDomain.BaseDirectory}/../LibraryForIISProject/Resources/students.xml";
        private static readonly string XML_ABSOLUTE_FILEPATH
            = "C:\\Users\\ivanm\\Desktop\\IIS\\projekt\\IIS-Project\\IIS-Project\\LibraryForIISProject\\Resources\\students.xml";
        private static readonly string XML_SOAP_FILEPATH
            = $"{AppDomain.CurrentDomain.BaseDirectory}/../LibraryForIISProject/Resources/studentsSOAP.xml";
        private static readonly string JAXB_DATA_XML_PATH = $"{AppDomain.CurrentDomain.BaseDirectory}/../../LibraryForIISProject/Resources/templateForJAXBRequest.xml";
        private const string XSD_TARGET_NAMESPACE
           = "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models";

        #endregion

        #region const

        private const string RPC_METHOD_NAME_FOR_JAXB = "Validation.isXmlFileValid";
        private const string RPC_URL = "http://localhost:8080";
        
        private const string REQUEST_XML = "application/xml";

        private const string XPATH_STRING = "/students/student/placeHolder[text()='PLACEHOLDER']";

        #endregion

        [WebMethod]
        public string HelloWorld()
        {
            return "Hii";
        }

        [WebMethod]
        public XmlElement GetStudentXmlFilteredByXPath(string nameOfNode, string valueToInclude)
        {

                XmlDocument doc = LoadAndSaveStudentsToXml();

                string xpath = XPATH_STRING.Replace("PLACEHOLDER", valueToInclude).Replace("placeHolder", nameOfNode.ToLower());
                XmlNodeList xmlNodeList = doc.SelectNodes(xpath);

                List<XmlNode> nodes = new List<XmlNode>();
                for (int i = 0; i < xmlNodeList.Count; i++)
                {
                    nodes.Add(xmlNodeList[i]);
                }

                XmlElement xmlElement = doc.DocumentElement;
                xmlElement.RemoveAll();
                nodes.ForEach(n => xmlElement.AppendChild(n.ParentNode));

                doc.Save(XML_SOAP_FILEPATH);

                if (CheckIfXmlFileIsValid() == false)
                {
                    return null;
                }
                return xmlElement;
            }

        private bool CheckIfXmlFileIsValid()
        {
            XmlDocument studentsDoc = new XmlDocument();
            studentsDoc.Load(XML_ABSOLUTE_FILEPATH);
            studentsDoc.DocumentElement.SetAttribute("xmlns", "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models");
            studentsDoc.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            studentsDoc.Save(XML_ABSOLUTE_FILEPATH);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(JAXB_DATA_XML_PATH);

                doc.DocumentElement.ChildNodes[0].InnerText = RPC_METHOD_NAME_FOR_JAXB;
                
                MemoryStream xmlStream = new MemoryStream();
                doc.Save(xmlStream);

                byte[] data = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(xmlStream.ToArray()));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RPC_URL);
                request.Method = "POST";
                request.Accept = REQUEST_XML;
                request.ContentType = REQUEST_XML;

                using (Stream requestData = request.GetRequestStream())
                {
                    requestData.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseData = response.GetResponseStream();

                doc = new XmlDocument();
                doc.Load(responseData);
 
                string value = doc
                        .DocumentElement
                        .FirstChild
                        .FirstChild
                        .FirstChild
                        .FirstChild
                        .InnerText;
                if (doc
                        .DocumentElement
                        .FirstChild
                        .FirstChild
                        .FirstChild
                        .FirstChild
                        .InnerText != "1")
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                studentsDoc.DocumentElement.RemoveAllAttributes();
                studentsDoc.Save(XML_ABSOLUTE_FILEPATH);
            }
        }

        private static XmlDocument LoadAndSaveStudentsToXml()
        {
            List<Student> students
                = XmlFileHandler.GetStudentsFromXml(XML_FILEPATH);
            XmlElement studentsXml = XmlFileHandler.SerializeStudents(students);
            XmlDocument doc = studentsXml.OwnerDocument;
            doc.DocumentElement.SetAttribute("xmlns", XSD_TARGET_NAMESPACE);
            return doc;
        }
    }
}
