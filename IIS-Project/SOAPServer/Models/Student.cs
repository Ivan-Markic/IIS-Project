using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SOAPServer.Models
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/LibraryForIISProject.Models", Name = "student")]
    [XmlRoot("student")]
    public class Student
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
    }
}