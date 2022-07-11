using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SOAPServer.Models
{
    [DataContract(Namespace = "", Name = "student")]
    [XmlRoot("student")]
    public class StudentWithoutNamespace
    {
        [XmlElement("name")]

        [DataMember(Order = 0, Name = "name")]
        public string Name { get; set; }
        [XmlElement("surname")]
        [DataMember(Order = 1, Name = "surname")]
        public string Surname { get; set; }
        [XmlElement("subject")]
        [DataMember(Order = 2, Name = "subject")]
        public string Subject { get; set; }
        [XmlElement("grade")]
        [DataMember(Order = 3, Name = "grade")]
        public int Grade { get; set; }
        public override string ToString()
        {
            return $"{Name} {Surname} {Subject}={Grade}";
        }

        public Student ToNormalStudent()
            => new Student
            {
                Name = this.Name,
                Surname = this.Surname,
                Subject = this.Subject,
                Grade = this.Grade
            };
    }
}