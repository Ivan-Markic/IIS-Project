using LibraryForIISProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LibraryForIISProject.Utils
{
    public class XmlFileHandler
    {
        public static List<Student> GetStudentsFromXml(string xmlPath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(StudentListWrapper));
            using (Stream xmlStream = new FileStream(xmlPath, FileMode.Open))
            {
                return ((StudentListWrapper)serializer.Deserialize(xmlStream)).Students;
            }
        }

        public static List<Student> GetStudentsFromXml(XmlElement studentsOfLastName)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(StudentListWrapper));
            using (XmlReader xmlReader = new XmlNodeReader(studentsOfLastName))
            {
                return ((StudentListWrapper)serializer.Deserialize(xmlReader)).Students;
            }
        }

        public static void AddStudentToXml(string xmlPath, Student student)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            XmlElement node = SerializeStudent(student);

            XmlNode importedNode = doc.ImportNode(node, true);
            doc.DocumentElement.AppendChild(importedNode);
            doc.Save(xmlPath);
        }

        public static XmlElement SerializeStudent(Student student)
        {
            XmlDocument doc = new XmlDocument();
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(typeof(Student)).Serialize(writer, student);
            }
            return doc.DocumentElement;
        }
    }
}
