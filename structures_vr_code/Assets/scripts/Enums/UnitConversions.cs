using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitConversions {

    public static double InchesToMeters(double inches)
    {
        return inches * 0.0254;
    }

    public static double MetersToInches(double meters)
    {
        return meters / 0.0254;
    }
}
