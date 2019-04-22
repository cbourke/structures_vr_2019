using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstraction of the parameters set by the CSI OAPI function "SapObject.SapModel.Results.JointDispl(...)".
/// The data members of this class correspond to parameters used by SapObject.SapModel.Results.JointDispl(...).
/// For more information on OAPI functions, consult CSI_OAPI_Documentation.chm in your SAP2000 installation directory.
/// </summary>
public class jointDispl : MonoBehaviour
{
    public string name;
    public string itemType;
    public int numberResults;
    public string[] obj;
    public string[] elm;
    public string[] loadCase;
    public string[] stepType;
    public double[] stepNum;
    public double[] u1;
    public double[] u2;
    public double[] u3;
    public double[] r1;
    public double[] r2;
    public double[] r3;

    //Constructor; simply sets all the data member values.
    public jointDispl(string name, string itemType, int numberResults, string[] obj, string[] elm, string[] loadCase, string[] stepType,
    double[] stepNum, double[] u1, double[] u2, double[] u3, double[] r1, double[] r2, double[] r3)
    {
        this.name = name;
        this.itemType = itemType;
        this.numberResults = numberResults;
        this.obj = obj;
        this.elm = elm;
        this.loadCase = loadCase;
        this.stepType = stepType;
        this.stepNum = stepNum;
        this.u1 = u1;
        this.u2 = u2;
        this.u3 = u3;
        this.r1 = r1;
        this.r2 = r2;
        this.r3 = r3;
    }

    /// <summary>
    /// Prints the data members of this object in a human-readable format.
    /// Used for debugging.
    /// </summary>
    public override string ToString()
    {
        string result = "";

        result = result + "name=" + name + "\n";
        result = result + "itemtype=" + itemType + "\n";
        result = result + "numberResults=" + numberResults + "\n";
        result = result + "obj={" + string.Join(", ", obj) + "}\n";
        result = result + "elm={" + string.Join(", ", elm) + "}\n";
        result = result + "loadCase={" + string.Join(", ", loadCase) + "}\n";
        result = result + "stepType={" + string.Join(", ", stepType) + "}\n";
        result = result + "stepNum={" + string.Join(", ", stepNum) + "}\n";
        result = result + "f1={" + string.Join(", ", u1) + "}\n";
        result = result + "f2={" + string.Join(", ", u2) + "}\n";
        result = result + "f3={" + string.Join(", ", u3) + "}\n";
        result = result + "m1={" + string.Join(", ", r1) + "}\n";
        result = result + "m2={" + string.Join(", ", r2) + "}\n";
        result = result + "m3={" + string.Join(", ", r3) + "}\n";

        return result;
    }
}
