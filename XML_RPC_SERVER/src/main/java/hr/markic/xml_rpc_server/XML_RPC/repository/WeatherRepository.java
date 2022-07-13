/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.markic.xml_rpc_server.XML_RPC.repository;

import hr.markic.xml_rpc_server.XML_RPC.models.Weather;
import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.stream.XMLStreamException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

/**
 *
 * @author ivanm
 */
public class WeatherRepository {

    private final String WEATHER_URI
            = "https://vrijeme.hr/hrvatska_n.xml";

    private List<Weather> weathers;

    public List<Weather> getWeathers() {
        if (weathers == null) {
            try {
                weathers = parseWeathers();
            } catch (IOException | XMLStreamException ex) {
                Logger.getLogger(WeatherRepository.class.getName()).log(Level.SEVERE, null, ex);
                return new ArrayList<>();
            }
        }
        return weathers;
    }

    public List<Weather> parseWeathers() throws IOException, XMLStreamException {
        List<Weather> localWeathers = new ArrayList<>();

        System.err.println("Connected successfully :)");

        try {
            DocumentBuilderFactory documentBuilderFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder documentBuilder = documentBuilderFactory.newDocumentBuilder();
            Document document = documentBuilder.parse(WEATHER_URI);
            document.getDocumentElement().normalize();
            System.out.println("Root element: " + document.getDocumentElement().getNodeName());
            NodeList nodeList = document.getElementsByTagName("Grad");
            System.out.println("----------------------------");
            

            for (int index = 0; index < nodeList.getLength(); index++) {
                Node node = nodeList.item(index);

                if (node.getNodeType() == Node.ELEMENT_NODE) {
                    Element element = (Element) node;
                    
                    String city = element
                                    .getElementsByTagName("GradIme")
                                    .item(0)
                                    .getTextContent();
                    Double temperature = Double.valueOf(element
                                    .getElementsByTagName("Temp")
                                    .item(0)
                                    .getTextContent());
                    
                   localWeathers.add(new Weather(city, temperature));
   
                }
            }   
        } catch (Exception e) {
            e.printStackTrace();
        }

        System.err.println("Parse successful.");
        return localWeathers;
    }

    public Double getTemperatureFromCityName(String city) throws
            IOException, XMLStreamException {
        weathers = parseWeathers();

        return weathers.stream().filter(weather
                -> weather.getCity().toLowerCase().equals(city.toLowerCase())).findFirst().orElse(null).getTemperature();
    }

}
