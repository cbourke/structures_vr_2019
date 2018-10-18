using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDataForXML : MonoBehaviour {
    public List<XMLFrame> xmlFrames;

    public StructureDataForXML()
    {
        xmlFrames = new List<XMLFrame>();
    }

    void Update()
    {
        
    }

}

public class XMLFrame
{
    public int x1;
    public int y1;
    public int z1;
    public int x2;
    public int y2;
    public int z2;
}
