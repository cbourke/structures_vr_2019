using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frameJointForce
{
    public string name;
    public string itemType;
    public int numberResults;
    public string[] obj;
    public string[] elm;
    public string[] pointElm;
    public string[] loadCase;
    public string[] stepType;
    public double[] stepNum;
    public double[] f1;
    public double[] f2;
    public double[] f3;
    public double[] m1;
    public double[] m2;
    public double[] m3;

    public frameJointForce(string name, string itemType, int numberResults, string[] obj, string[] elm, string[] pointElm, string[] loadCase, string[] stepType,
        double[] stepNum, double[] f1, double[] f2, double[] f3, double[] m1, double[] m2, double[] m3)
    {
        this.name = name;
        this.itemType = itemType;
        this.numberResults = numberResults;
        this.obj = obj;
        this.elm = elm;
        this.pointElm = pointElm;
        this.loadCase = loadCase;
        this.stepType = stepType;
        this.stepNum = stepNum;
        this.f1 = f1;
        this.f2 = f2;
        this.f3 = f3;
        this.m1 = m1;
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
        result = result + "elm={" + string.Join(", ", elm) + "}\n";
        result = result + "pointElm={" + string.Join(", ", pointElm) + "}\n";
        result = result + "loadCase={" + string.Join(", ", loadCase) + "}\n";
        result = result + "stepType={" + string.Join(", ", stepType) + "}\n";
        result = result + "stepNum={" + string.Join(", ", stepNum) + "}\n";
        result = result + "f1={" + string.Join(", ", f1) + "}\n";
        result = result + "f2={" + string.Join(", ", f2) + "}\n";
        result = result + "f3={" + string.Join(", ", f3) + "}\n";
        result = result + "m1={" + string.Join(", ", m1) + "}\n";
        result = result + "m2={" + string.Join(", ", m2) + "}\n";
        result = result + "m3={" + string.Join(", ", m3) + "}\n";

        return result;
    }
}
