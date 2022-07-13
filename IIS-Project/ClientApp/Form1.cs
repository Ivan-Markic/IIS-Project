using LibraryForIISProject.Models;
using LibraryForIISProject.Utils;
using StudentServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        #region Constants

        private const string REQUEST_URI = "http://localhost:5000/api/Student";
        private const string REQUEST_XML = "application/xml";

        private const string RPC_METHOD_NAME_FOR_WEATHER = "Weather.getTemperatureFromCityName";
        private const string RPC_URL = "http://localhost:8080";
        private const string WEATHER_DATA_XML_PATH = "../../../../LibraryForIISProject/Resources/templateForWeatherRequest.xml";

        private const string REQUEST_JSON = "application/json";
        private const string ANONYFLOW_API_KEY_HEADER_NAME = "x-api-key";

        #endregion

        public Form1()
        {
            InitializeComponent();
            PopulateData();
            
        }

        private void PopulateData()
        {
            tbName.Text = "Laura";
            tbSurname.Text = "Laurić";
            tbSubject.Text = "Math";
            numbericGrade.Value = 4;
            cbValidate.Items.Add("XSD");
            cbValidate.Items.Add("RNG");
            cbValidate.SelectedIndex = 1;
            cbNameOfNode.Items.Add("name");
            cbNameOfNode.Items.Add("surname");
            cbNameOfNode.Items.Add("subject");
            cbNameOfNode.SelectedIndex = 0;
            tbCity.Text = "Bjelovar";
        }

        private void btnCreateStudent_Click(object sender, EventArgs e)
        {
            if (ValidateFields() == false)
            {
                MessageBox.Show("You need to fill all fields", "Input error");
                return;
            }
            if (cbValidate.SelectedItem.ToString() == "XSD")
            {
                MessageBox.Show("your chooshed XSD");
                AddStudentWithXsdValidation();
            }
            else
            {
                AddStudentWithRngValidation();
            }
        }

        private bool ValidateFields()
        {
            return tbName.Text != "" && tbSurname.Text != "" && tbSubject.Text != "";
        }


        // REST API XSD
        private void AddStudentWithXsdValidation()
        {

            Student student = GetStudentFromInputs<Student>();

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Student));
            MemoryStream stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                dataContractSerializer.WriteObject(writer, student);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(stream.ToArray()));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(REQUEST_URI + "/xml");
            request.Method = "POST";
            request.Accept = REQUEST_XML;
            request.ContentType = REQUEST_XML;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestData, 0, requestData.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
 
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show($"Successfully added {student}", "Success");
                    ResetInputFields();
                }
                else
                {
                    MessageBox.Show($"Status code: {response.StatusCode}", "Info");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Validation failed, check inputs.", "Validation error");
            }
        }

        private void AddStudentWithRngValidation()
        {
            StudentWithoutNamespace student = GetStudentFromInputs<StudentWithoutNamespace>(false);

            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(StudentWithoutNamespace));
            MemoryStream stream = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(stream))
            {
                dataContractSerializer.WriteObject(writer, student);
            }

            byte[] requestData = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(stream.ToArray()));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(REQUEST_URI + "/xmlrng");
            request.Method = "POST";
            request.Accept = REQUEST_XML;
            request.ContentType = REQUEST_XML;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestData, 0, requestData.Length);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show($"Successfully added {student} ", "Success");
                    ResetInputFields();
                }
                else
                {
                    MessageBox.Show($"Status code: {response.StatusCode}", "Info");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Validation failed, check inputs.", "Validation error");
            }
        }

        private void LoadStudentFromSoapService()
        {
            try
            {
                StudentServiceSoapClient studentService = new StudentServiceSoapClient
                    (StudentServiceSoapClient
                        .EndpointConfiguration.StudentServiceSoap);

                GetStudentXmlFilteredByXPathRequest request = 
                    new GetStudentXmlFilteredByXPathRequest(new GetStudentXmlFilteredByXPathRequestBody(tbSearch.Text, cbNameOfNode.SelectedItem.ToString()));

                 XmlElement filteredStudents = studentService
                .GetStudentXmlFilteredByXPathAsync(request)
                .Result
                .Body
                .GetStudentXmlFilteredByXPathResult;

                if (filteredStudents == null)
                {
                    MessageBox.Show("Validation from JAXB did not pass");
                }
                else
                {
                    MessageBox.Show("Validation from JAXB passed");
                    List<Student> students = new List<Student>(
                        XmlFileHandler.GetStudentsFromXml(filteredStudents));

                    MessageBox.Show("Number of students matching given condition: " + students.Count);
                    foreach (Student student in students)
                    {
                        MessageBox.Show("Student " + student.ToString());
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error connecting to the SOAP service, check if it is running.",
                    "SOAP error.");
                return;
            }
        }

        // XML-RPC
        private void GetTemperatureFromXmlRpcServer()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(WEATHER_DATA_XML_PATH);

                doc.DocumentElement.ChildNodes[0].InnerText = RPC_METHOD_NAME_FOR_WEATHER;  //methodName
                doc.DocumentElement.ChildNodes[1]                               // params
                    .FirstChild                                                 // param
                    .FirstChild                                                 // value
                    .FirstChild.InnerText = tbCity.Text;                 // string

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

                tbTemperature.Text = doc
                        .DocumentElement
                        .FirstChild            
                        .FirstChild             
                        .FirstChild             
                        .FirstChild             
                        .InnerText;

                if (tbTemperature.Text == "faultCode0")
                {
                    tbTemperature.Text = "No data";
                    MessageBox.Show($"No data for inputed city {tbCity.Text}");
                }
                else
                {
                    tbTemperature.Text += "°C";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured:\n{ex.Message}\n{ex.StackTrace}", "Error.");
            }
        }


        private void ResetInputFields()
        {
            tbName.Text = "";
            tbSurname.Text = "";
            tbSubject.Text = "";
            numbericGrade.Value = 0;
        }


        private T GetStudentFromInputs<T>(bool haveNamespace = true)
        {
            if (haveNamespace)
            {
                return (T)Convert.ChangeType(new Student
                {
                    Name = tbName.Text,
                    Surname = tbSurname.Text,
                    Subject = tbSubject.Text,
                    Grade = (int)numbericGrade.Value
                }, typeof(T));
            }
            return (T)Convert.ChangeType(new StudentWithoutNamespace
            {
                Name = tbName.Text,
                Surname = tbSurname.Text,
                Subject = tbSubject.Text,
                Grade = (int)numbericGrade.Value
            }, typeof(T));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadStudentFromSoapService();
        }

        private void btnGetTemperature_Click(object sender, EventArgs e)
        {
            GetTemperatureFromXmlRpcServer();
        }
    }
}
