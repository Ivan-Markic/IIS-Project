/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.markic.xml_rpc_server.JAXB;

import java.io.File;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import org.datacontract.schemas._2004._07.libraryforiisproject.Students;
import org.xml.sax.SAXException;

/**
 *
 * @author ivanm
 */
public class ValidateXmlFile { 

    private final String nameOfXmlFile = "C:\\Users\\ivanm\\Desktop\\IIS\\projekt\\IIS-Project\\IIS-Project\\LibraryForIISProject\\Resources\\students.xml";
    private final String nameOfXsdFile = "C:\\Users\\ivanm\\Desktop\\IIS\\projekt\\IIS-Project\\IIS-Project\\LibraryForIISProject\\Resources\\students.xsd";
    
    
    public boolean isXmlFileValid() {
        JAXBContext jaxbContext;
        File xsdFile = new File(nameOfXsdFile);
        File xmlFile = new File(nameOfXmlFile);
    try
    {
      //Get JAXBContext
      jaxbContext = JAXBContext.newInstance(Students.class);
       
      //Create Unmarshaller
      Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
       
      //Setup schema validator
      SchemaFactory sf = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
      Schema studentsSchema = sf.newSchema(xsdFile);
      jaxbUnmarshaller.setSchema(studentsSchema);
       
      //Unmarshal xml file 
      Students students = (Students) jaxbUnmarshaller.unmarshal(xmlFile);
      
      return true;
    }
    catch (JAXBException | SAXException e) 
    {
      e.printStackTrace();
      return false;
    }
  }
}
