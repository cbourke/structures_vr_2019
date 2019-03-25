using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

/* This class handles the unit conversions */
/* Unity is written in Meters, so internally everything is converted to meters */
public class unitsController : MonoBehaviour {
	private string length;
	private string force;
	private string temperature;

	// how many decimal places to round to
	private int decimalPlaces = 8;

	// stores ratios for unit conversions 
	// the system defaults to m,N,C
	// temp conversions handled in function because its not a ratio and theres only 2
	private Dictionary<string, double> convTable = new Dictionary<string, double>();
  private List<string> unitList = new List<string>();

	/// <summary>
	/// Populates the unit list with the differnt unit combinations
	/// The unit list is used by Ui elements to let the user pick their current units
	/// </summary>
	void Start()
	{
		populateDict();
		unitList.Add("lb, in, F");
		unitList.Add("lb, ft, F");
		unitList.Add("Kip, in F");
		unitList.Add("Kip, ft, F");
		unitList.Add("KN, mm, C");
		unitList.Add("KN, m, C");
		unitList.Add("Kgf, mm, C");
		unitList.Add("Kgf, m, C");
		unitList.Add("N, mm, C");
		unitList.Add("N, m, C");
		unitList.Add("Tonf, mm, C");
		unitList.Add("Tonf, m, C");
		unitList.Add("KN, cm, C");
		unitList.Add("Kgf, cm, C");
		unitList.Add("N, cm, C");
		unitList.Add("Tonf, cm, C");
		
	}

	/// <summary>
	/// Returns the list of units
	/// </summary>
	public List<string> getUnits()
	{
		return unitList;
	}

	/// <summary>
	/// Populates the dictionary with the conversion factors
	/// </summary>
	public void populateDict()
	{
		this.convTable.Add("m", 1.0);
		this.convTable.Add("cm", 100.0);
		this.convTable.Add("mm", 1000.0);
		this.convTable.Add("ft", 3.281);
		this.convTable.Add("in", 39.37008);

		this.convTable.Add("N", 1.0);
		this.convTable.Add("lb", 0.2248);
		this.convTable.Add("Tonf", 0.0001019716);
		this.convTable.Add("KN", 0.001);
		this.convTable.Add("Kip", 0.00022481);
		this.convTable.Add("Kgf", 0.101972);
	}

	/// <summary>
	/// Sets the current units from a properly formated string (format: m,N,C)
	/// </summary>
	public void setUnits(string unitsString)
	{
		// strip whitespace
		unitsString = Regex.Replace(unitsString, @"\s+", "");
		string[] unitsArray = unitsString.Split(',');
		
		this.force = unitsArray[0];
		this.length = unitsArray[1];
		this.temperature = unitsArray[2];
		Debug.Log("force: " + force);
		Debug.Log("length: " + length);
		Debug.Log("temperature: " + temperature);
	}

	/* unit getters and setters */
	public string getLengthUnit()
	{
		return this.length;
	}

	public void setLengthUnit(string unit)
	{
		this.length = unit;
	}

	public string getForceUnit()
	{
		return this.force;
	}

	public void setForceUnit(string unit)
	{
		this.force = unit;
	}

	public string getTempUnit()
	{
		return this.temperature;
	}

	public void setTempUnit(string unit)
	{
		this.temperature = unit;
	}

	/// <summary>
	/// takes length in meters and returns user defined units
	/// </summary>
	public double getLength(double length)
	{
		return Math.Round(length * this.convTable[this.length], this.decimalPlaces);
	}
	
	/// <summary>
	/// get lenght in meters given current unit
	/// </summary>
	public double getLengthMeters(double length)
	{
		return Math.Round(length / this.convTable[this.length], this.decimalPlaces);
	}

	/// <summary>
	//// takes force in Newtons and returns user defined force
	/// </summary>
	public double getForce(double force)
	{
		return Math.Round(force * this.convTable[this.force], this.decimalPlaces);
	}

	/// <summary>
	/// returns force in newtons
	/// </summary>
	public double getForceNewtons(double force)
	{
		return Math.Round(force / this.convTable[this.force], this.decimalPlaces);
	}
	
	/// <summary>
	/// takes temp in Celcius and returns user defined temp
	/// </summary>
	public double getTemperature(double temp) {
		if (this.temperature == "C")
		{
			return temp;
		}
		return Math.Round((temp * 1.8f + 32), this.decimalPlaces);
	}

	/// <summary>
	/// given a temp in farenheit this converts and returns it as celcius
	/// </summary>
	public double getTemperatureCelcius(double temp)
	{
		return Math.Round(((temp - 32) / 1.8f), this.decimalPlaces);
	}

}
