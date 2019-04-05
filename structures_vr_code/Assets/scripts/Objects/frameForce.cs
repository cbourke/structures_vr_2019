using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameForce : MonoBehaviour
{
    public string name;
    public string itemType;
    public int numberResults;
    public string[] obj;
    public double[] objSta;
    public string[] elm;
    public double[] elmSta;
    public string[] loadCase;
    public string[] stepType;
    public double[] stepNum;
    public double[] p;
    public double[] v2;
    public double[] v3;
    public double[] t;
    public double[] m2;
    public double[] m3;

    public frameForce(string name, string itemType, int numberResults, string[] obj, double[] objSta, string[] elm, double[] elmSta, string[] loadCase, string[] stepType,
        double[] stepNum, double[] p, double[] v2, double[] v3, double[] t, double[] m2, double[] m3)
    {
        this.name = name;
        this.itemType = itemType;
        this.numberResults = numberResults;
        this.obj = obj;
        this.objSta = objSta;
        this.elm = elm;
        this.elmSta = elmSta;
        this.loadCase = loadCase;
        this.stepType = stepType;
        this.stepNum = stepNum;
        this.p = p;
        this.v2 = v2;
        this.v3 = v3;
        this.t = t;
        this.m2 = m2;
        this.m3 = m3;
    }

    public override string ToString()
    {
        string result = "";

        result = result + "name=" + name + "\n";
        result = result + "itemtype=" + itemType + "\n";
        result = result + "numberResults=" + numberResults + "\n";
        result = result + "obj={" + string.Join(", ", obj) + "}\n";
        result = result + "objSta={" + string.Join(", ", objSta) + "}\n";
        result = result + "elm={" + string.Join(", ", elm) + "}\n";
        result = result + "elmSta={" + string.Join(", ", elmSta) + "}\n";
        result = result + "loadCase={" + string.Join(", ", loadCase) + "}\n";
        result = result + "stepType={" + string.Join(", ", stepType) + "}\n";
        result = result + "stepNum={" + string.Join(", ", stepNum) + "}\n";
        result = result + "p={" + string.Join(", ", p) + "}\n";
        result = result + "v2={" + string.Join(", ", v2) + "}\n";
        result = result + "v3={" + string.Join(", ", v3) + "}\n";
        result = result + "t={" + string.Join(", ", t) + "}\n";
        result = result + "m2={" + string.Join(", ", m2) + "}\n";
        result = result + "m3={" + string.Join(", ", m3) + "}\n";

        return result;
    }
}
