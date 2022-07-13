using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace LibraryForIISProject.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models", Name = "students")]
    [XmlRoot("students")]
    public class StudentListWrapper
    {
        [XmlElement("student")]
        [DataMember(Order = 0, Name = "students")]
        public List<Student> Students { get; set; }
        public StudentListWrapper()
        {
            Students = new List<Student>();
        }

        public StudentListWrapper(List<Student> students)
        {
            Students = students;
        }
    }
}
