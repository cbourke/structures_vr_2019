using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Security.Permissions;
using System.Text;
using UnityEngine;

public class SapTranslatorIpcHandler : MonoBehaviour
{
    public analysisController myAnalysisController;
    static NamedPipeServerStream pipeServer;
    static StreamString pipeStreamString;

    private Queue<string> outputBuffer = new Queue<string>();

    public void Start()
    {
        String[] args = new string[0];
        // If this is commented out, SAPTranslator will not open
        createPipeServer(args);

    }

    public void OnDestroy()
    {
        if (pipeServer != null)
        {
            pipeServer.Close();
        }
    }

    public void createPipeServer(string[] args)
    {
        pipeServer = new NamedPipeServerStream("vrPipe", PipeDirection.InOut, 1);
        Debug.Log("Created pipeServer.");
        System.Object connectionObject = new System.Object();
        AsyncCallback connectionCallback = new AsyncCallback(onClientConnect);
        pipeServer.BeginWaitForConnection(connectionCallback, connectionObject);
        Debug.Log("Waiting for client connection to pipeServer.");
        //string filePath = Application.persistentDataPath + "/" + structureSaveFileName + ".xml";
        string appPath = System.IO.Path.Combine(Application.streamingAssetsPath, "SapTranslator.exe");
        System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
        myProcess.StartInfo.FileName = appPath;
        //myProcess.StartInfo.Arguments = "\"" + filePath + "\"";
        myProcess.Start();
    }

    public void onClientConnect(IAsyncResult result)
    {
        Debug.Log("Client has connected to pipeServer.");

        pipeStreamString = new StreamString(pipeServer);

        String message = "VRE to SAPTranslator: Beginning inter-process communication. Do you read me?";
        Debug.Log(message);
        pipeStreamString.WriteString(message);

        sendString("VRE to SAPTranslator: initialize(false)");
    }

    public void enqueueToOutputBuffer(string message)
    {
        outputBuffer.Enqueue(message);
    }

    public void sendString(string message)
    {
        if (pipeStreamString == null)
        {

            Debug.Log("Tried to send message \"" + message + "\" to SAPTranslator, but SAPTranslator has not connected to pipe.");
            return;
        }
        else
        {
            string pipeContent = pipeStreamString.ReadString();
            if (pipeContent != null)
            {
                Debug.Log(pipeContent);
            }
            if (pipeContent.Equals("SAPTranslator to VRE: awaiting command"))
            {
                Debug.Log(message);
                pipeStreamString.WriteString(message);
                pipeServer.WaitForPipeDrain();

                if (message.Contains("resultsFrameJointForce"))
                {
                    frameJointForce frameJointForceObject = readResponseResultsFrameJointForce();
                    myAnalysisController.setLatestFrameJointForceResult(frameJointForceObject);
                }
                else if (message.Contains("resultsFrameForce"))
                {
                    frameForce frameForceObject = readResponseResultsFrameForce();
                    myAnalysisController.setLatestFrameForceResult(frameForceObject);
                }
                else if (message.Contains("resultsJointDispl"))
                {
                    jointDispl jointDisplObject = readResponseResultsJointDispl();
                    myAnalysisController.setLatestJointDisplResult(jointDisplObject);
                }
            }
        }
    }


    private frameJointForce readResponseResultsFrameJointForce()
    {
        string[] results = new string[16];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = pipeStreamString.ReadString();
        }

        //Debug:
        string resultsAsOneString = String.Join("\n", results);
        Debug.Log(resultsAsOneString);
        string messageHeader = results[0];
        string name = results[1];
        string itemType = results[2];
        int numberResults;
        try
        {
            numberResults = int.Parse(results[3], System.Globalization.NumberStyles.Integer);
        }
        catch (FormatException)
        {
            numberResults = 2;
        }
        string[] obj = results[4].Split('`');
        string[] elm = results[5].Split('`');
        string[] pointElm = results[6].Split('`');
        string[] loadCase = results[7].Split('`');
        string[] stepType = results[8].Split('`');

        string[] stepNumStrings = results[9].Split('`');
        string[] f1Strings = results[10].Split('`');
        string[] f2Strings = results[11].Split('`');
        string[] f3Strings = results[12].Split('`');
        string[] m1Strings = results[13].Split('`');
        string[] m2Strings = results[14].Split('`');
        string[] m3Strings = results[15].Split('`');

        double[] stepNum = new double[numberResults];
        double[] f1 = new double[numberResults];
        double[] f2 = new double[numberResults];
        double[] f3 = new double[numberResults];
        double[] m1 = new double[numberResults];
        double[] m2 = new double[numberResults];
        double[] m3 = new double[numberResults];

        for (int i = 0; i < numberResults; i++)
        {
            stepNum[i] = Double.Parse(stepNumStrings[i]);
            f1[i] = Double.Parse(f1Strings[i]);
            f2[i] = Double.Parse(f2Strings[i]);
            f3[i] = Double.Parse(f3Strings[i]);
            m1[i] = Double.Parse(m1Strings[i]);
            m2[i] = Double.Parse(m2Strings[i]);
            m3[i] = Double.Parse(m3Strings[i]);
        }

        frameJointForce result = new frameJointForce(name,
            itemType, numberResults, obj, elm, pointElm,
            loadCase, stepType, stepNum, f1, f2, f3, m1, m2, m3);

        return result;
    }

    private frameForce readResponseResultsFrameForce()
    {
        string[] results = new string[17];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = pipeStreamString.ReadString();
        }

        //Debug:
        string resultsAsOneString = String.Join("\n", results);
        Debug.Log(resultsAsOneString);
        string messageHeader = results[0];
        string name = results[1];
        string itemType = results[2];
        int numberResults;
        try
        {
            numberResults = int.Parse(results[3], System.Globalization.NumberStyles.Integer);
        }
        catch (FormatException)
        {
            numberResults = 9;
        }
        string[] obj = results[4].Split('`');
        string[] objStaStrings = results[5].Split('`');
        string[] elm = results[6].Split('`');
        string[] elmStaStrings = results[7].Split('`');
        string[] loadCase = results[8].Split('`');
        string[] stepType = results[9].Split('`');
        string[] stepNumStrings = results[10].Split('`');
        string[] pStrings = results[11].Split('`');
        string[] v2Strings = results[12].Split('`');
        string[] v3Strings = results[13].Split('`');
        string[] tStrings = results[14].Split('`');
        string[] m2Strings = results[15].Split('`');
        string[] m3Strings = results[16].Split('`');

        double[] objSta = new double[numberResults];
        double[] elmSta = new double[numberResults];
        double[] stepNum = new double[numberResults];
        double[] p = new double[numberResults];
        double[] v2 = new double[numberResults];
        double[] v3 = new double[numberResults];
        double[] t = new double[numberResults];
        double[] m2 = new double[numberResults];
        double[] m3 = new double[numberResults];

        for (int i = 0; i < numberResults; i++)
        {
            objSta[i] = Double.Parse(objStaStrings[i]);
            elmSta[i] = Double.Parse(elmStaStrings[i]);
            stepNum[i] = Double.Parse(stepNumStrings[i]);
            p[i] = Double.Parse(pStrings[i]);
            v2[i] = Double.Parse(v2Strings[i]);
            v3[i] = Double.Parse(v3Strings[i]);
            t[i] = Double.Parse(tStrings[i]);
            m2[i] = Double.Parse(m2Strings[i]);
            m3[i] = Double.Parse(m3Strings[i]);
        }

        frameForce result = new frameForce(name,
            itemType, numberResults, obj, objSta, elm, elmSta,
            loadCase, stepType, stepNum, p, v2, v3, t, m2, m3);

        return result;
    }

    private jointDispl readResponseResultsJointDispl()
    {
        string[] results = new string[15];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = pipeStreamString.ReadString();
        }

        //Debug:
        string resultsAsOneString = String.Join("\n", results);
        Debug.Log(resultsAsOneString);
        string messageHeader = results[0];
        string name = results[1];
        string itemType = results[2];
        int numberResults;
        try
        {
            numberResults = int.Parse(results[3], System.Globalization.NumberStyles.Integer);
        }
        catch (FormatException)
        {
            numberResults = 2;
        }
        string[] obj = results[4].Split('`');
        string[] elm = results[5].Split('`');
        string[] loadCase = results[6].Split('`');
        string[] stepType = results[7].Split('`');

        string[] stepNumStrings = results[8].Split('`');
        string[] u1Strings = results[9].Split('`');
        string[] u2Strings = results[10].Split('`');
        string[] u3Strings = results[11].Split('`');
        string[] r1Strings = results[12].Split('`');
        string[] r2Strings = results[13].Split('`');
        string[] r3Strings = results[14].Split('`');

        double[] stepNum = new double[numberResults];
        double[] u1 = new double[numberResults];
        double[] u2 = new double[numberResults];
        double[] u3 = new double[numberResults];
        double[] r1 = new double[numberResults];
        double[] r2 = new double[numberResults];
        double[] r3 = new double[numberResults];

        for (int i = 0; i < numberResults; i++)
        {
            stepNum[i] = Double.Parse(stepNumStrings[i]);
            u1[i] = Double.Parse(u1Strings[i]);
            u2[i] = Double.Parse(u2Strings[i]);
            u3[i] = Double.Parse(u3Strings[i]);
            r1[i] = Double.Parse(r1Strings[i]);
            r2[i] = Double.Parse(r2Strings[i]);
            r3[i] = Double.Parse(r3Strings[i]);
        }

        jointDispl result = new jointDispl(name,
            itemType, numberResults, obj, elm,
            loadCase, stepType, stepNum, u1, u2, u3, r1, r2, r3);

        return result;
    }

    public void propMaterialGetMPIsotropic(BuildingMaterial material)
    {
        string message = "VRE to SAPTranslator: propMaterialGetMPIsotropic(" + material.GetName() + ")";
        sendString(message);
        material.MPIsotropic = readPropMaterialGetMPIsotropic();
    }

    private MPIsotropic readPropMaterialGetMPIsotropic()
    {
        string[] results = new string[6];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = pipeStreamString.ReadString();
        }

        double e, u, a, g, temp;

        string messageHeader = results[0];
        string name = results[1];

        Double.TryParse(results[2], out e);
        Double.TryParse(results[3], out u);
        Double.TryParse(results[4], out a);
        Double.TryParse(results[5], out g);

        MPIsotropic newMPIsotropic = new MPIsotropic(name, e, u, a, g);
        return newMPIsotropic;
    }

    public void propFrameGetSectProps(FrameSection section)
    {
        string message = "VRE to SAPTranslator: propFrameGetSectProps(" + section.GetName() + ")";
        sendString(message);
        section.SectProps = readPropFrameGetSectProps();
    }

    private SectProps readPropFrameGetSectProps()
    {
        string[] results = new string[14];
        for (int i = 0; i < results.Length; i++)
        {
            results[i] = pipeStreamString.ReadString();
        }

        double area, as2, as3, torsion, i22, i33, s22, s33, z22, z33, r22, r33;

        string messageHeader = results[0];
        string name = results[1];

        Double.TryParse(results[2], out area);
        Double.TryParse(results[3], out as2);
        Double.TryParse(results[4], out as3);
        Double.TryParse(results[5], out torsion);
        Double.TryParse(results[6], out i22);
        Double.TryParse(results[7], out i33);
        Double.TryParse(results[8], out s22);
        Double.TryParse(results[9], out s33);
        Double.TryParse(results[10], out z22);
        Double.TryParse(results[11], out z33);
        Double.TryParse(results[12], out r22);
        Double.TryParse(results[13], out r33);

        SectProps newSectProps = new SectProps(name, area, as2, as3, torsion, i22, i33, s22, s33, z22, z33, r22, r33);
        return newSectProps;
    }

    public void Update()
    {
        if (pipeStreamString != null && outputBuffer.Count >= 1)
        {
            string message = outputBuffer.Dequeue();
            sendString(message);
        }
    }

}


public class StreamString
{
    private Stream ioStream;
    private UnicodeEncoding streamEncoding;

    public StreamString(Stream ioStream)
    {
        this.ioStream = ioStream;
        streamEncoding = new UnicodeEncoding();
    }

    public string ReadString()
    {
        int len = 0;

        len = ioStream.ReadByte() * 256;
        len += ioStream.ReadByte();
        byte[] inBuffer = new byte[len];
        ioStream.Read(inBuffer, 0, len);

        return streamEncoding.GetString(inBuffer);
    }

    public int WriteString(string outString)
    {
        byte[] outBuffer = streamEncoding.GetBytes(outString);
        int len = outBuffer.Length;
        if (len > UInt16.MaxValue)
        {
            len = (int)UInt16.MaxValue;
        }
        ioStream.WriteByte((byte)(len / 256));
        ioStream.WriteByte((byte)(len & 255));
        ioStream.Write(outBuffer, 0, len);
        ioStream.Flush();

        return outBuffer.Length + 2;
    }
}