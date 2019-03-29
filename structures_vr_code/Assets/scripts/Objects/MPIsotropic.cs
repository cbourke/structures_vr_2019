using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPIsotropic : MonoBehaviour
{
    public MPIsotropic(string name, double e, double u, double a, double g, double temp = 0.0) {
        Name = name;
        E = e;
        U = u;
        G = g;
        Temp = temp;
    }

    /// <summary>The name of this material property.</summary> 
    public string Name { get; set; }

    /// <summary>The modulus of elasticity. [F/(L^2)]</summary> 
    public double E { get; set; }

    /// <summary>Poisson's ratio.</summary> 
    public double U { get; set; }

    /// <summary>The thermal coefficient. [1/T]</summary> 
    public double A { get; set; }

    /// <summary>The shear modulus. [F/(L^2)]</summary> 
    public double G { get; set; }

    /// <summary>The optional temperature at which the other properties of this material are specified. [T]</summary> 
    public double Temp { get; set; }
}
