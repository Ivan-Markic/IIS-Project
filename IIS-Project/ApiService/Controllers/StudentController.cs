using ApiService.Auth;
using Commons.Xml.Relaxng;
using LibraryForIISProject.Models;
using LibraryForIISProject.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
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
        private IConfiguration configuration;
        private JwtAuthenticationManager jwtAuthenticationManager;
        private bool validationError;

        public StudentController(List<Student> students, IConfiguration configuration, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.students = students;
            this.configuration = configuration;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public string Get()
        {
            return "Hi user :)";
        }

        [Authorize]
        [HttpGet("secret")]
        public IActionResult GetStudents()
        {
            return Ok("I just passed IIS :)");
        }

        [AllowAnonymous]
        [HttpPost("xml")]
        public void ValidationXSD(XmlElement studentXml)
        {
            XmlDocument doc = null;
            try
            {
                doc = studentXml.OwnerDocument;
                doc.AppendChild(studentXml);
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
        
        [AllowAnonymous]
        [HttpPost("xmlrng")]
        public void ValidationRng(XmlElement studentXml)
        {
            XmlDocument doc = null;
            try
            {
                doc = studentXml.OwnerDocument;
                doc.AppendChild(studentXml);

                XmlDocument tempFile = new XmlDocument();
                tempFile.LoadXml(studentXml.OuterXml);
                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(TEMP_STUDENT_FILENAME, Encoding.UTF8))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    tempFile.Save(xmlTextWriter);
                }

                XmlReader studentReader = new XmlTextReader(TEMP_STUDENT_FILENAME);
                XmlReader rngReader = new XmlTextReader(RNG_STUDENT_FILEPATH);

                using (RelaxngValidatingReader rngValidator = new RelaxngValidatingReader(studentReader, rngReader))
                {
                    while (!rngValidator.EOF)
                    {
                        rngValidator.Read();
                    }
                }
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
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                Response.WriteAsync($"Something went wrong: {ex.Message}\n{doc.OuterXml}");
            }

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var token = jwtAuthenticationManager.Authenticate(username, password);
            
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
