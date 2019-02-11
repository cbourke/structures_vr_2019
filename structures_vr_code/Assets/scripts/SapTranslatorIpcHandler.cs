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

    public void Start()
    {
        String[] args = new string[0];
        createPipeServer(args);
    }

    public void OnDestroy()
    {
        if (pipeServer != null) {
            pipeServer.Close();
        }
    }

    public static void createPipeServer(string[] args)
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

    public static void onClientConnect(IAsyncResult result)
    {
        Debug.Log("Client has connected to pipeServer.");

        pipeStreamString = new StreamString(pipeServer);

        String message = "VRE to SAPTranslator: Beginning inter-process communication. Do you read me?";
        Debug.Log(message);
        pipeStreamString.WriteString(message);

        String response = pipeStreamString.ReadString();
        Debug.Log(response);
    }

    public static void sendString(string message)
    {
        Debug.Log(message);
        pipeStreamString.WriteString(message);
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