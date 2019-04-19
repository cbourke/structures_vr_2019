using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* doesn't work, or not implimentation not finished */
public class deformationScaleFactorUI : MonoBehaviour
{
    public TMPro.TMP_InputField myField;
    public analysisController myAnalysisController;

    public void setDeformationScaleFromTextField()
    {
        string input = myField.text;
        if (!input.Contains("."))
        {
            input = input + ".0";
        }
        float newScale = (float)System.Double.Parse(myField.text);
        myAnalysisController.visualizationScale = newScale;
    }

}
