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
    static NamedPipeServerStream pipeServer;
    static StreamString pipeStreamString;

    private Queue<string> outputBuffer = new Queue<string>();

    public void Start()
    {
        String[] args = new string[0];
        // If this is commented out, SAPTranslator will not open
        //createPipeServer(args);
        
    }

    public void OnDestroy()
    {
        if (pipeServer != null) {
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
        string appPath = Application.streamingAssetsPath + "/SapTranslator.exe";
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

    public void sendString(string message)
    {
        if (pipeStreamString == null)
        {
            Debug.Log("Tried to send message \"" + message + "\" to SAPTranslator, but SAPTranslator has not connected to pipe.");
            return;
        }
        string pipeContent = pipeStreamString.ReadString();
        if (pipeContent != null)
        {
            Debug.Log(pipeContent);
        }
        //pipeServer.Flush();
        if (pipeContent.Equals("SAPTranslator to VRE: awaiting command")) {
            Debug.Log(message);
            pipeStreamString.WriteString(message);
        }
        
    }

    public void enqueueToOutputBuffer(string message)
    {
        outputBuffer.Enqueue(message);
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