using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/* This class reads in the different material types from a TSV file and stores them in a multilevel dictionary */
/* Note that the majority of this code was copied from online, so its integrity is questionable. It does, however, work */
/* The TSV file is stored in the StreamingAssets folder so that when the project is built it does not get removed */
public class materialDefinitions : MonoBehaviour {

    public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> materialCollection = new Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>>();
    
    /// <summary>
    /// Adds each of the materials into the collection
    /// </summary>
    void Start()
    {
        try
        {
            StreamReader oStreamReader = new StreamReader(Application.dataPath + "\\StreamingAssets\\materials.txt");
            string line;
            string region;
            string type;
            string standard;
            string grade;

            while ((line = oStreamReader.ReadLine()) != null)
            {
                /* region   type    standard    grade */
                string[] parsedItem = line.Split('\t');

                // Retrieving data fields
                region = parsedItem[0];
                type = parsedItem[1];
                standard = parsedItem[2];
                grade = parsedItem[3];

                // If we get an already existing Region
                if (materialCollection.ContainsKey(region))
                {
                    //  If we get an already existing Type
                    if (materialCollection[region].ContainsKey(type))
                    {
                        //  If  we get an already existing Standard
                        if (materialCollection[region][type].ContainsKey(standard))
                        {
                            // Grade is added into list
                            materialCollection[region][type][standard].Add(grade);
                        }
                        //  If we get a new Standard
                        else
                        {
                            // Standard is added and value part is newed up
                            materialCollection[region][type].Add(standard, new List<string>());
                            // Grade is added into list
                            materialCollection[region][type][standard].Add(grade);
                        }
                    }
                    //  If we get a new Type 
                    else
                    {
                        // Type is added and value part is newed up
                        materialCollection[region].Add(type, new Dictionary<string, List<string>>());

                        // Standard is added and value part is newed up
                        materialCollection[region][type].Add(standard, new List<string>());

                        // Grade is added into list
                        materialCollection[region][type][standard].Add(grade);
                    }
                }
                // If we get a new Region
                else
                {
                    // Region is added and value part is newed up
                    materialCollection.Add(region, new Dictionary<string, Dictionary<string, List<string>>>());

                    // Type is added and value part is newed up
                    materialCollection[region].Add(type, new Dictionary<string, List<string>>());

                    // Standard is added and value part is newed up
                    materialCollection[region][type].Add(standard, new List<string>());

                    // Grade is added into list
                    materialCollection[region][type][standard].Add(grade);
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
	}

    /// <summary>
    /// Returns the collection of materials
    /// </summary>
	public Dictionary<string, Dictionary<string, Dictionary<string, List<string>>>> getDict() {
        return materialCollection;
    }

    /// <summary>
    /// Prints out all the materials
    /// @TODO this may or may not work, the code was moved here but never retested
    /// </summary>
    public void printDict()
    {      
        Debug.Log("MATERIALS");
        foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<string>>>> r in materialCollection) {
            Debug.Log("Region: " + r.Key);
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> t in r.Value) {
                Debug.Log("type: " + t.Key);
                foreach (KeyValuePair<string, List<string>> s in t.Value) {
                    Debug.Log("standard: " + s.Key);
                    foreach (string g in s.Value) {
                        Debug.Log("grade: " + g);        
                    }
                }
            }
        }
    }
}
