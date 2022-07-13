/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package hr.markic.xml_rpc_server.XML_RPC.models;

import java.util.List;

/**
 *
 * @author ivanm
 */
public class Weather {
    
    private String city;
    private Double temperature;
    
    public Weather() {
    }

    public Weather(String city) {
        this.city = city;
    }
    
    public Weather(String city, Double temperature) {
        this.city = city;
        this.temperature = temperature;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public Double getTemperature() {
        return temperature;
    }

    public void setTemperature(Double temperature) {
        this.temperature = temperature;
    }
    
}
