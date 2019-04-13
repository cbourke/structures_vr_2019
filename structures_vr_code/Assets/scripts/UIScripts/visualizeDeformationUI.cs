using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Scripts for the Delete UI button */
/* Contains the logic for deleting a selection of frames */
public class visualizeDeformationUI : MonoBehaviour
{
    public constructorController myConstructorController;
    public selectionController mySelectionController;
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;
    public analysisController myAnalysisController;


    void Start()
    {

    }

    public void onClick() {

        List<Frame> frameList = myConstructorController.getFrameList();
        StartCoroutine(pullVisualizations(frameList));
    }

    IEnumerator pullVisualizations(List<Frame> frames)
    {
        mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsSetupDeselectAllCasesAndCombosForOutput()");
        mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: resultsSetupSetCaseSelectedForOutput(DEAD, true)");

        foreach (Frame f in frames)
        {
            string frameName = f.getName();
            mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: customResultsGetFrameSpecialPointDispl(" + frameName + ")");
            yield return new WaitForSeconds((float)0.5);
            myAnalysisController.visualizeDeformation(frameName, myAnalysisController.getLatestJointDisplResult());
        }
        



    }
}
