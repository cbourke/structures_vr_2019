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
	private Dictionary<string, float> convTable = new Dictionary<string, float>();
  private List<string> unitList = new List<string>();

	/// <summary>
	/// Populates the unit list with the differnt unit combinations
	/// The unit list is used by Ui elements to let the user pick their current units
	/// </summary>
	void Awake()
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
		Debug.Log("Start units");	
	}

	/// <summary>
	/// Returns the list of units
	/// </summary>
	public List<string> getUnits()
	{
		Debug.Log("get units");
		return unitList;
	}

	/// <summary>
	/// Populates the dictionary with the conversion factors
	/// </summary>
	public void populateDict()
	{
		this.convTable.Add("m", 1.0f);
		this.convTable.Add("cm", 100.0f);
		this.convTable.Add("mm", 1000.0f);
		this.convTable.Add("ft", 3.281f);
		this.convTable.Add("in", 39.37008f);

		this.convTable.Add("N", 1.0f);
		this.convTable.Add("lb", 0.2248f);
		this.convTable.Add("Tonf", 0.0001019716f);
		this.convTable.Add("KN", 0.001f);
		this.convTable.Add("Kip", 0.00022481f);
		this.convTable.Add("Kgf", 0.101972f);
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
	public float getLength(float length)
	{
		return (float)Math.Round(length * this.convTable[this.length], this.decimalPlaces);
	}
	
	/// <summary>
	/// Converts the passed value to meters
	/// </summary>
	public float getLengthMeters(float length)
	{
		return (float)Math.Round(length / this.convTable[this.length], this.decimalPlaces);
	}

	/// <summary>
	//// takes force in Newtons and returns user defined force
	/// </summary>
	public float getForce(float force)
	{
		return (float)Math.Round(force * this.convTable[this.force], this.decimalPlaces);
	}

	/// <summary>
	/// returns force in newtons
	/// </summary>
	public float getForceNewtons(float force)
	{
		return (float)Math.Round(force / this.convTable[this.force], this.decimalPlaces);
	}
	
	/// <summary>
	/// takes temp in Celcius and returns user defined temp
	/// </summary>
	public float getTemperature(float temp) {
		if (this.temperature == "C")
		{
			return temp;
		}
		return (float)Math.Round((temp * 1.8f + 32), this.decimalPlaces);
	}

	/// <summary>
	/// given a temp in farenheit this converts and returns it as celcius
	/// </summary>
	public float getTemperatureCelcius(float temp)
	{
		return (float)Math.Round(((temp - 32) / 1.8f), this.decimalPlaces);
	}

}
