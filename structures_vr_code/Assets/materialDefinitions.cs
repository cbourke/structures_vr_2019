using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class materialDefinitions : MonoBehaviour {

	 public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> materialCollection = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
    void Start()
    {
        try
        {
            // A data record file "RecordsFile.txt" is read.
            StreamReader oStreamReader = new StreamReader("Assets\\scripts\\Enums\\materials.txt");
            string line;
            string region;
            string type;
            string standard;
            string grade;

            while ((line = oStreamReader.ReadLine()) != null)
            {
                // In records file, we keep one person's record in one line and data fields are seperated by TAB
                // region   type    standard    grade  

                string[] parsedItem = line.Split('\t');

                // Retrieving data fields
                region = parsedItem[0];
                type = parsedItem[1];
                standard = parsedItem[2];
                grade = parsedItem[3];

                // If we get an already existing Postal Code
                if (materialCollection.ContainsKey(region))
                {
                    //  If we get an already existing City
                    if (materialCollection[region].ContainsKey(type))
                    {
                        //  If  we get an already existing StreetAddress
                        if (materialCollection[region][type].ContainsKey(standard))
                        {
                            // Person's record is added into list
                            materialCollection[region][type][standard].Add(grade);
                        }
                        //  If we get a new StreetAddress
                        else
                        {
                            // Street Address is added and value part is newed up
                            materialCollection[region][type].Add(standard, new List<string>());
                            // Person's record is added into list
                            materialCollection[region][type][standard].Add(grade);
                        }
                    }
                    //  If we get a new city 
                    else
                    {
                        // City is added and value part is newed up
                        materialCollection[region].Add(type, new Dictionary<string, List<string>>());

                        // Street Address is added and value part is newed up
                        materialCollection[region][type].Add(standard, new List<string>());

                        // Person's record is added into list
                        materialCollection[region][type][standard].Add(grade);

                    }

                }

                // If we get a new postal code
                else
                {
                    // Postal code is added and value part is newed up
                    materialCollection.Add(region, new Dictionary<string, Dictionary<string, List<string>>>());

                    // City is added and value part is newed up
                    materialCollection[region].Add(type, new Dictionary<string, List<string>>());

                    // Street Address is added and value part is newed up
                    materialCollection[region][type].Add(standard, new List<string>());

                    // Person's record is added into list
                    materialCollection[region][type][standard].Add(grade);
                }
            }
        }

        catch (Exception e)

        {

            throw e;

        }
	}

	public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> getDict() {
        return materialCollection;
    }
}
