using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* doesn't work, or not implimentation not finished */
public class deformationScaleFactorUI : MonoBehaviour
{
    public TMPro.TMP_Text value;
    public analysisController myAnalysisController;

    public void setDeformationScaleFromTextField()
    {
        float newScale = (float)System.Double.Parse(value.text);
        myAnalysisController.visualizationScale = newScale;
    }

}
