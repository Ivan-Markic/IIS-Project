using SOAPServer.Models;
using SOAPServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        private static readonly string XML_FILEPATH
            = $"{AppDomain.CurrentDomain.BaseDirectory}/../LibraryForIISProject/Resources/students.xml";
        private static readonly string XML_SOAP_FILEPATH
            = $"{AppDomain.CurrentDomain.BaseDirectory}/../LibraryForIISProject/Resources/studentsSOAP.xml";
        private const string XSD_TARGET_NAMESPACE
           = "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models";

        private const string XPATH_STRING = "/students/student/placeHolder[text()='PLACEHOLDER']";

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

            return xmlElement;
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
