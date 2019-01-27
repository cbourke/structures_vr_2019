using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class unitsController : MonoBehaviour {
	private string length;
	private string force;
	private string temperature;

	// how many decimal places to round to
	private int decimalPlaces = 3;

	// stores ratios for unit conversions 
	// the system defaults to m,N,C
	// temp conversions handled in function because its not a ratio and theres only 2
	private Dictionary<string, double> convTable = new Dictionary<string, double>();
    
	public void populateDict()
    {
        // distance conversions
        this.convTable.Add("m", 1.0);
        this.convTable.Add("cm", 0.01);
        this.convTable.Add("mm", 0.001);
        this.convTable.Add("ft", 3.281);
        this.convTable.Add("in", 0.0254);

        // force conversions
        this.convTable.Add("N", 1.0);
        this.convTable.Add("lb", 0.2248);
        this.convTable.Add("Tonf", 0.0001019716);
        this.convTable.Add("KN", 0.001);
        this.convTable.Add("Kip", 0.00022481);
        this.convTable.Add("Kgf", 0.101972);
    }

    public void setUnits(string unitsString){
		// strip whitespace
		unitsString = Regex.Replace(unitsString, @"\s+", "");
		// split (format: m,N,C)
		string[] unitsArray = unitsString.Split(',');
		
		this.force = unitsArray[0];
		this.length = unitsArray[1];
		this.temperature = unitsArray[2];
	}

	// unit getters and setters
	public string getLengthUnit() {
		return this.length;
	}

	public void setLengthUnit(string unit) {
		this.length = unit;
	}

	public string getForceUnit() {
		return this.force;
	}

	public void setForceUnit(string unit) {
		this.force = unit;
	}

	public string getTempUnit() {
		return this.temperature;
	}

	public void setTempUnit(string unit) {
		this.temperature = unit;
	}

	// takes length in meters and returns user defined units
	public double getLength(double length) {
        Debug.Log("length: " + length * this.convTable[this.length]);
		return Math.Round(length * this.convTable[this.length], this.decimalPlaces);
	}

	// get lenght in meters given current unit
	public double getLengthMeters(double length) {
		return Math.Round(length / this.convTable[this.length], this.decimalPlaces);
    }

	// takes force in Newtons and returns user defined force
	public double getForce(double force) {
		return Math.Round(force * this.convTable[this.force], this.decimalPlaces);
    }

	// returns force in newtons
	public double getForceNewtons(double force) {
		return Math.Round(force / this.convTable[this.force], this.decimalPlaces);
    }
	
	// takes temp in Celcius and returns user defined temp
	public double getTemperature(double temp) {
		if (this.temperature == "C"){
			return temp;
		}
		return Math.Round((temp * 1.8f + 32), this.decimalPlaces);
	}

	// given a temp in farenheit this converts and returns it as celcius
	public double getTemperatureCelcius(double temp){
		return Math.Round(((temp - 32) / 1.8f), this.decimalPlaces);
    }

}
