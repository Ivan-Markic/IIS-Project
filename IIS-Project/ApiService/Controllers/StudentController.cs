using Commons.Xml.Relaxng;
using LibraryForIISProject.Models;
using LibraryForIISProject.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        #region Constants

        private const string XSD_TARGET_NAMESPACE
            = "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models";
        private const string XSD_STUDENT_FILEPATH = "../LibraryForIISProject/Resources/student.xsd";
        private const string XML_STUDENTS_FILEPATH = "../LibraryForIISProject/Resources/students.xml";
        private const string XSD_STUDENTS_FILEPATH = "../LibraryForIISProject/Resources/students.xsd";
        private const string RNG_STUDENT_FILEPATH = "../LibraryForIISProject/Resources/students.rng";
        private const string TEMP_STUDENT_FILENAME = "../LibraryForIISProject/Resources/tempStudentFile.xml";
        
        #endregion
        
        private List<Student> students;
        private bool validationError;

        public StudentController(List<Student> students)
        {
            this.students = students;
        }

        [HttpGet]
        public List<Student> Get()
        {
            return students;
        }

        // POST api/<CustomersController>/xml
        [HttpPost("xml")]
        public void Post(XmlElement customerXml)
        {
            XmlDocument doc = null;
            try
            {
                doc = customerXml.OwnerDocument;
                doc.AppendChild(customerXml);
                doc.Schemas.Add(XSD_TARGET_NAMESPACE, XSD_STUDENT_FILEPATH);

                doc.Validate(XmlValidation);

                if (!validationError)
                {
                    DataContractSerializer deserialize = new DataContractSerializer(typeof(Student));
                    MemoryStream xmlStream = new MemoryStream();
                    doc.Save(xmlStream);
                    xmlStream.Position = 0;
                    Student student = (Student)deserialize.ReadObject(xmlStream);
                    students.Add(student);
                    try
                    {
                        XmlFileHandler.AddStudentToXml(XML_STUDENTS_FILEPATH, student);
                    }
                    catch (Exception ex)
                    {
                        Response.StatusCode = StatusCodes.Status400BadRequest;
                        Response.WriteAsync($"Failed to add student to XML.\n{ex.Message}");
                    }
                }
                else
                {
                    Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    Response.WriteAsync($"Validation not passed.");
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                Response.WriteAsync($"{ex.Message}\n{doc.OuterXml}");
            }

        }

        private void XmlValidation(object sender, ValidationEventArgs e)
        {
            validationError = true;
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }

        // POST api/<CustomersController>/xmlrng
        [HttpPost("xmlrng")]
        public void PostRng(XmlElement studentXml)
        {
            XmlDocument doc = null;
            try
            {
                doc = studentXml.OwnerDocument;
                doc.AppendChild(studentXml);

                XmlDocument tempFile = new XmlDocument();
                tempFile.LoadXml(studentXml.OuterXml);
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(TEMP_STUDENT_FILENAME, System.Text.Encoding.UTF8))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    tempFile.Save(xmlTextWriter);
                }

                XmlReader studentReader = new XmlTextReader(TEMP_STUDENT_FILENAME);
                XmlReader rngReader = new XmlTextReader(RNG_STUDENT_FILEPATH);

                bool validated = true;

                using (RelaxngValidatingReader rngValidator = new RelaxngValidatingReader(studentReader, rngReader))
                {
                    while (!rngValidator.EOF)
                    {
                        rngValidator.Read();
                    }
                }

                if (validated)
                {
                    DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(StudentWithoutNamespace));
                    MemoryStream xmlStream = new MemoryStream();
                    doc.Save(xmlStream);
                    xmlStream.Position = 0;
                    Student student = ((StudentWithoutNamespace)dataContractSerializer.ReadObject(xmlStream)).ToNormalStudent();
                    students.Add(student);
                    try
                    {
                        XmlFileHandler.AddStudentToXml(XML_STUDENTS_FILEPATH, student);
                    }
                    catch (Exception ex)
                    {
                        Response.StatusCode = StatusCodes.Status500InternalServerError;
                        Response.WriteAsync($"Failed to add student to XML.\n{ex.Message}\n{ex.StackTrace}");
                    }
                }
                else
                {
                    Response.StatusCode = StatusCodes.Status406NotAcceptable;
                    Response.WriteAsync($"Validation not passed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                Response.WriteAsync($"Something went wrong: {ex.Message}\n{doc.OuterXml}");
            }

        }
    }
}
