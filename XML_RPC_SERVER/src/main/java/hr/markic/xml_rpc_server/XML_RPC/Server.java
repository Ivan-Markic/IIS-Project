/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.markic.xml_rpc_server.XML_RPC;

import hr.markic.xml_rpc_server.JAXB.ValidateXmlFile;
import hr.markic.xml_rpc_server.XML_RPC.repository.WeatherRepository;
import java.io.IOException;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServer;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.WebServer;

/**
 *
 * @author ivanm
 */
public class Server {
    
    public static void main(String[] args) {
        
        try {
            System.out.println("Starting XML-RPC server");
            WebServer server = new WebServer(8080);

            XmlRpcServer xmlServer = server.getXmlRpcServer();
            PropertyHandlerMapping phm = new PropertyHandlerMapping();
            phm.addHandler("Validation", ValidateXmlFile.class);
            phm.addHandler("Weather", WeatherRepository.class);
            xmlServer.setHandlerMapping(phm);

            XmlRpcServerConfigImpl serverConfig
            = (XmlRpcServerConfigImpl) xmlServer.getConfig();
            serverConfig.setContentLengthOptional(false);
            serverConfig.setEnabledForExtensions(true);

            server.start();
            System.out.println("Server started.");
            System.out.println("Waiting for requests...");
            
            } catch (IOException | XmlRpcException ex) {
            System.out.println("\nAn error occured.");
            }
    }

}
