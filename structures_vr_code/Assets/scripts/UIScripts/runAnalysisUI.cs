using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Scripts for the Delete UI button */
/* Contains the logic for deleting a selection of frames */
public class runAnalysisUI : MonoBehaviour
{
    public constructorController myConstructorController;
    public SapTranslatorIpcHandler mySapTranslatorIpcHandler;

    public void onClick() {
        string sapModelDirectory = Application.persistentDataPath;
        string sapModelName = myConstructorController.structureSaveFileName;
        mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: saveAs(" + sapModelDirectory + "," + sapModelName + ")");

        mySapTranslatorIpcHandler.enqueueToOutputBuffer("VRE to SAPTranslator: analyzeRunAnalysis("+ sapModelDirectory +  ","  + sapModelName +")");
    }
}
