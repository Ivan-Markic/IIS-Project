using LibraryForIISProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        #region Constants

        private const string REQUEST_URI = "http://localhost:5000/api/Student";
        private const string REQUEST_XML = "application/xml";
        private const string WEATHER_DATA_XML_PATH = "../../../WeatherXml/weatherData.xml";

        private const string RPC_METHOD_NAME = "Weather.getTemperature";
        private const string RPC_URL = "http://localhost:8080";

        private const string XML_ANONYMIZED_CUSTOMERS_PATH = "../../../../../Data/customersAnon.xml";
        private const string ANONYFLOW_API_URL = "https://api.anonyflow.com";
        private const string ANONYFLOW_API_KEY = "tkCrmwPM2EnF1Ax2GSsH6JxaBbRpVXAaJhwMLwM1";
        private const string REQUEST_JSON = "application/json";
        private const string ANONYFLOW_ANONYMIZE_PACKET_METHOD = "/anony-packet";
        private const string ANONYFLOW_DEANONYMIZE_PACKET_METHOD = "/deanony-packet";
        private const string ANONYFLOW_API_KEY_HEADER_NAME = "x-api-key";

        #endregion

        public Form1()
        {
            InitializeComponent();
            tbName.Text = "Laura";
            tbSurname.Text = "Laurić";
            tbSubject.Text = "Math";
            numbericGrade.Value = 4;
            cbValidate.Items.Add("XSD");
            cbValidate.Items.Add("RNG");
            cbValidate.SelectedIndex = 1;
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
   
    }
}
