using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectProps : MonoBehaviour
{
    public SectProps(string name, double area, double as2, double as3, double torsion, double i22, double i33, double s22, double s33, double z22, double z33, double r22, double r33)
    {
        Name = name;
        Area = area;
        As2 = as2;
        As3 = as3;
        Torsion = torsion;
        I22 = i22;
        I33 = i33;
        S22 = s22;
        S33 = s33;
        Z22 = z22;
        Z33 = z33;
        R22 = r22;
        R33 = r33;
    }

    /// <summary>The name of this section property.</summary> 
    public string Name { get; set; }

    /// <summary>The section's cross-sectional area. [L^2]</summary> 
    public double Area { get; set; }

    /// <summary>The shear area for forces in the section local 2-axis direction. [L^2]</summary> 
    public double As2 { get; set; }

    /// <summary>The shear area for forces in the section local 3-axis direction. [L^2]</summary> 
    public double As3 { get; set; }

    /// <summary>The torsional constant. [L^4]</summary> 
    public double Torsion { get; set; }

    /// <summary>The moment of inertia for bending about the local 2 axis. [L^4]</summary> 
    public double I22 { get; set; }

    /// <summary>The moment of inertia for bending about the local 3 axis. [L^4]</summary> 
    public double I33 { get; set; }

    /// <summary>The section modulus for bending about the local 2 axis. [L^3]</summary> 
    public double S22 { get; set; }

    /// <summary>The section modulus for bending about the local 3 axis. [L^3]</summary> 
    public double S33 { get; set; }

    /// <summary>The plastic modulus for bending about the local 2 axis. [L^3]</summary> 
    public double Z22 { get; set; }

    /// <summary>The plastic modulus for bending about the local 3 axis. [L^3]</summary> 
    public double Z33 { get; set; }

    /// <summary>The radius of gyration about the local 2 axis. [L]</summary> 
    public double R22 { get; set; }

    /// <summary>The radius of gyration about the local 3 axis. [L]</summary> 
    public double R33 { get; set; }
}
