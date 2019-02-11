﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using SAP2000v20;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.IO.Pipes;

namespace SapTranslator
{
    class Program
    {
        private static int ret = 0; //Use ret to check if functions return successfully (ret = 0) or fail (ret = nonzero)
        private static cOAPI mySapObject = null; //dimension the SapObject as cOAPI type
        private static cHelper myHelper;
        private static cSapModel mySapModel;
        private static string modelDirectory;
        private static string modelName;


        static void Main(string[] args)
        {
            NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "vrPipe", PipeDirection.InOut, PipeOptions.None);
            Console.WriteLine("Connecting to VRE server via named pipe \"vrPipe\"...\n");
            pipeClient.Connect();
            StreamString pipeStreamString = new StreamString(pipeClient);

            while (pipeClient.IsConnected) {
                readStreamForCommand(pipeStreamString);
                
                string response = "SAPTranslator to VRE: awaiting command";
                pipeStreamString.WriteString(response);
                Console.WriteLine(response);
                pipeClient.WaitForPipeDrain();
            }
            
        }


        static void readStreamForCommand(StreamString pipeStreamString)
        {
            String message = pipeStreamString.ReadString();
            Console.WriteLine(message); //Display the incoming message in the console of SAPTranslator
            StringReader messageReader = new StringReader(message);
            StringBuilder messageParser = new StringBuilder();

            String sender;
            String reciever;
            String functionName;
            List<String> arguments = new List<string>();
            int i = 0;

            while (i < message.Length && (char)messageReader.Peek() != ' ') // capture first word as sender
            {
                messageParser.Append((char)messageReader.Read());
                i++;
            }
            sender = messageParser.ToString();
            messageParser.Clear();
            i += 4; // Skip substring " to " 
            messageReader.Read();
            messageReader.Read();
            messageReader.Read();
            messageReader.Read();
            while (i < message.Length && (char)messageReader.Peek() != ':') // capture third word as reciever
            {
                messageParser.Append((char)messageReader.Read());
                i++;
            }
            reciever = messageParser.ToString();
            messageParser.Clear();
            i += 2; // Skip substring ": "
            messageReader.Read();
            messageReader.Read();
            while (i < message.Length && (char)messageReader.Peek() != '(') // capture fourth word, up to "(", as function name
            {
                messageParser.Append((char)messageReader.Read());
                i++;
            }
            functionName = messageParser.ToString();
            messageParser.Clear();
            i += 1; // Skip substring "("
            messageReader.Read();
            while (i < message.Length && (char)messageReader.Peek() != ')') // capture arguments
            {
                while (i < message.Length && (char)messageReader.Peek() != ',' && (char)messageReader.Peek() != ')')
                {
                    messageParser.Append((char)messageReader.Read());
                    i++;
                }
                arguments.Add(messageParser.ToString());
                messageParser.Clear();

                i += 2; // Skip substring ", "
                messageReader.Read();
                messageReader.Read();
            }


            if (reciever.Equals("SAPTranslator")) {
                Console.WriteLine("SAPTranslator: I recognize that I am being addressed.");
                if (message.Substring(22).Equals("Beginning inter-process communication. Do you read me?"))
                {
                    respondToPipeConnectionStart(pipeStreamString);
                }
                else if (functionName.Equals("initialize"))
                {
                    Console.WriteLine("SAPTranslator: I recognize command: initialize.");
                    initialize(arguments);
                }
                else if (functionName.Equals("saveAs"))
                {
                    saveAs(arguments);
                }
                else if (functionName.Equals("save"))
                {
                    save();
                }
                else if (functionName.Equals("frameObjAddByCoord"))
                {
                    Console.WriteLine("SAPTranslator: I recognize command: frameObjAddByCoord.");
                    frameObjAddByCoord(arguments);
                    ret = mySapModel.View.RefreshView(0, false);
                }
                else if (functionName.Equals("frameObjDelete"))
                {
                    frameObjDelete(arguments);
                }
                else if (functionName.Equals("frameObjSetSection"))
                {
                    frameObjSetSection(arguments);
                }
                else if (functionName.Equals("frameObjSetSelected"))
                {
                    frameObjSetSelected(arguments);
                }
                else if (functionName.Equals("pointCoordSetRestraint"))
                {
                    pointCoordSetRestraint(arguments);
                }
                


                //TODO: ... add more possible messages to respond to
            }
        }


        static void respondToPipeConnectionStart(StreamString pipeStreamString)
        {
            //String response = "SAPTranslator to VRE: awaiting command.";
            //pipeStreamString.WriteString(response);
            return;
        }

        
        static void initialize(List<string> arguments) // Initialize with auto-find latest SAP installation
        {
            bool attachToInstance = Boolean.Parse(arguments[0]);
            string programPath = null;
            if (arguments.Count >= 2)
            {
                programPath = arguments[1];
            }
            

            if (attachToInstance)
            {
                //attach to a running instance of SAP2000
                try
                {
                    //get the active SapObject
                    mySapObject = (cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No running instance of the program found or failed to attach.");
                    return;
                }
            }
            else
            {
                //create API helper object
                try
                {
                    myHelper = new Helper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot create an instance of the Helper object");
                    return;
                }

                //try to create an instance of the SapObject from the specified path
                if (programPath == null)
                {
                    //try to create an instance of the SapObject from the latest installed SAP2000
                    try
                    {
                        //create SapObject
                        mySapObject = myHelper.CreateObjectProgID("CSI.SAP2000.API.SapObject");
                    }
                    catch (Exception exceptionAutoPath)
                    {
                        Console.WriteLine("Cannot start a new instance of the program.");
                        return;
                    }
                } else
                {
                    try
                    {
                        //create SapObject
                        mySapObject = myHelper.CreateObject(programPath);
                    }
                    catch (Exception exceptionSpecifyPath)
                    {
                        Console.WriteLine("Cannot start a new instance of the program from " + programPath);
                        //try to create an instance of the SapObject from the latest installed SAP2000
                        try
                        {
                            //create SapObject
                            mySapObject = myHelper.CreateObjectProgID("CSI.SAP2000.API.SapObject");
                        }
                        catch (Exception exceptionAutoPath)
                        {
                            Console.WriteLine("Cannot start a new instance of the program.");
                            return;
                        }
                    }
                }
                
            }
            //start SAP2000 application
            ret = mySapObject.ApplicationStart();

            //create SapModel object
            mySapModel = mySapObject.SapModel;

            //initialize model
            ret = mySapModel.InitializeNewModel((eUnits.N_m_C));

            //create new blank model
            ret = mySapModel.File.NewBlank();

            //switch to kip-ft-F units
            ret = mySapModel.SetPresentUnits(eUnits.kip_ft_F);
        }

        
        static void saveAs(List<string> arguments)
        {
            string newModelDirectory = arguments[0];
            string newModelName = arguments[1];
            try
            {
                System.IO.Directory.CreateDirectory(newModelDirectory);
                modelDirectory = newModelDirectory;
                modelName = newModelName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create directory: " + newModelDirectory);
            }
            string ModelPath = modelDirectory + System.IO.Path.DirectorySeparatorChar + modelName + ".sdb";
            ret = mySapModel.File.Save(ModelPath);
        }


        static void save()
        {
            string ModelPath = modelDirectory + System.IO.Path.DirectorySeparatorChar + modelName + ".sdb";
            ret = mySapModel.File.Save(ModelPath);
        }


        static void applicationExit(bool fileSave)
        {
            if (fileSave)
            {
                save();
            }
            mySapObject.ApplicationExit(false);
            mySapModel = null;
            mySapObject = null;
        }


        static void frameObjAddByCoord(List<string> arguments)
        {
            double xi = Double.Parse(arguments[0]);
            double yi = Double.Parse(arguments[1]);
            double zi = Double.Parse(arguments[2]);
            double xj = Double.Parse(arguments[3]);
            double yj = Double.Parse(arguments[4]);
            double zj = Double.Parse(arguments[5]);
            string propName = "Default";
            if (arguments.Count >= 7)
            {
                propName = arguments[6];
            }
            string userName = "";
            if (arguments.Count >= 8)
            {
                userName = arguments[7];
            }

            string tempFrameName = "";
            mySapModel.FrameObj.AddByCoord(xi, yi, zi, xj, yj, zj, ref tempFrameName, propName, userName);
        }

        static void frameObjDelete(List<string> arguments)
        {
            string name = arguments[0];
            eItemType itemType = eItemType.Objects;
            if (arguments.Count >= 2)
            {
                itemType = (eItemType) Int32.Parse(arguments[1]);
            }

            mySapModel.FrameObj.Delete(name, itemType);
        } 

        static void frameObjSetSection(List<string> arguments)
        {
            string name = arguments[0];
            string propName = arguments[1];
            eItemType itemType = eItemType.Objects;
            if (arguments.Count >= 3)
            {
                itemType = (eItemType)Int32.Parse(arguments[2]);
            }
            double sVarRelStartLoc = 0.0;
            if (arguments.Count >= 4)
            {
                sVarRelStartLoc = Double.Parse(arguments[3]);
            }
            double sVarTotalLength = 0.0;
            if (arguments.Count >= 5)
            {
                sVarTotalLength = Double.Parse(arguments[4]);
            }

            mySapModel.FrameObj.SetSection(name, propName, itemType, sVarRelStartLoc, sVarTotalLength);
        }

        static void frameObjSetSelected(List<string> arguments)
        {
            string name = arguments[0];
            bool selected = Boolean.Parse(arguments[1]);
            eItemType itemType = eItemType.Objects;
            if (arguments.Count >= 3)
            {
                itemType = (eItemType) Int32.Parse(arguments[2]);
            }

            mySapModel.FrameObj.SetSelected(name, selected, itemType);
        }

        static void pointObjAddCartesian(List<string> arguments)
        {
            double x = Double.Parse(arguments[0]);
            double y = Double.Parse(arguments[1]);
            double z = Double.Parse(arguments[2]);
            string pointName = "";
            string userName = "";
            string cSys = "";
            bool mergeOff = false;
            int mergeNumber = 0;
            if (arguments.Count >= 4)
            {
                userName = arguments[3];
                if (arguments.Count >= 5)
                {
                    cSys = arguments[4];
                    if (arguments.Count >= 6)
                    {
                        mergeOff = Boolean.Parse(arguments[5]);
                        if (arguments.Count >= 7)
                        {
                            mergeNumber = Int32.Parse(arguments[6]);
                        }
                    }
                }

            }
            mySapModel.PointObj.AddCartesian(x, y, z, ref pointName, userName, cSys, mergeOff, mergeNumber);
        }

        static void pointCoordSetRestraint(List<string> arguments)
        {
            double x = Double.Parse(arguments[0]);
            double y = Double.Parse(arguments[1]);
            double z = Double.Parse(arguments[2]);
            string pointName = "";
            mySapModel.PointObj.AddCartesian(x, y, z, ref pointName);

            bool u1 = Boolean.Parse(arguments[3]);
            bool u2 = Boolean.Parse(arguments[4]);
            bool u3 = Boolean.Parse(arguments[5]);
            bool r1 = Boolean.Parse(arguments[6]);
            bool r2 = Boolean.Parse(arguments[7]);
            bool r3 = Boolean.Parse(arguments[8]);
            bool[] value = { u1, u2, u3, r1, r2, r3 };

            eItemType itemType = 0;

            if(arguments.Count >= 10)
            {
                itemType = (eItemType)Int32.Parse(arguments[9]);
            }

            mySapModel.PointObj.SetRestraint(pointName, ref value, itemType);
        }


        static void generateStructure(string[] args) //args[0] should be the xml file path to pull structure from
        {
            //EXAMPLE CODE START

            //set the following flag to true to attach to an existing instance of the program
            //otherwise a new instance of the program will be started
            bool AttachToInstance;
            AttachToInstance = false;


            //set the following flag to true to manually specify the path to SAP2000.exe
            //this allows for a connection to a version of SAP2000 other than the latest installation
            //otherwise the latest installed version of SAP2000 will be launched
            bool SpecifyPath;
            SpecifyPath = false;


            //if the above flag is set to true, specify the path to SAP2000 below
            string ProgramPath;
            ProgramPath = "C:\\Program Files (x86)\\Computers and Structures\\SAP2000 20\\SAP2000.exe";


            //full path to the model
            //set it to the desired path of your model
            string ModelDirectory = "C:\\CSiAPIexample";

            try
            {
                System.IO.Directory.CreateDirectory(ModelDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create directory: " + ModelDirectory);
            }

            string ModelName = "API_1-001.sdb";
            string ModelPath = ModelDirectory + System.IO.Path.DirectorySeparatorChar + ModelName;


            //dimension the SapObject as cOAPI type
            cOAPI mySapObject = null;


            //Use ret to check if functions return successfully (ret = 0) or fail (ret = nonzero)
            int ret = 0;


            if (AttachToInstance)
            {
                //attach to a running instance of SAP2000
                try
                {
                    //get the active SapObject
                    mySapObject = (cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No running instance of the program found or failed to attach.");
                    return;
                }
            }
            else
            {
                //create API helper object
                cHelper myHelper;

                try
                {
                    myHelper = new Helper();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot create an instance of the Helper object");
                    return;
                }

                if (SpecifyPath)
                {
                    //'create an instance of the SapObject from the specified path
                    try
                    {
                        //create SapObject
                        mySapObject = myHelper.CreateObject(ProgramPath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Cannot start a new instance of the program from " + ProgramPath);
                        return;
                    }
                }
                else
                {
                    //'create an instance of the SapObject from the latest installed SAP2000
                    try
                    {
                        //create SapObject
                        mySapObject = myHelper.CreateObjectProgID("CSI.SAP2000.API.SapObject");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Cannot start a new instance of the program.");
                        return;
                    }
                }
                //start SAP2000 application
                ret = mySapObject.ApplicationStart();
            }


            //create SapModel object
            cSapModel mySapModel;
            mySapModel = mySapObject.SapModel;


            //initialize model
            ret = mySapModel.InitializeNewModel((eUnits.N_m_C));


            //create new blank model
            ret = mySapModel.File.NewBlank();

            /*
            //define material property
            ret = mySapModel.PropMaterial.SetMaterial("CONC", eMatType.Concrete, -1, "", "");


            //assign isotropic mechanical properties to material
            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0);


            //define rectangular frame section property
            ret = mySapModel.PropFrame.SetRectangle("R1", "CONC", 12, 12, -1, "", "");


            //define frame section property modifiers
            double[] ModValue = new double[8];
            int i;

            for (i = 0; i <= 7; i++)
            {
                ModValue[i] = 1;
            }

            ModValue[0] = 1000;
            ModValue[1] = 0;
            ModValue[2] = 0;

            ret = mySapModel.PropFrame.SetModifiers("R1", ref ModValue);
            */

            //switch to kN-m units
            ret = mySapModel.SetPresentUnits(eUnits.kN_m_C);

            //Read xml file
            XmlSerializer serializer = new XmlSerializer(typeof(StructuralElementsLists));
            try
            {
                string xmlfilepath = args[0];
                Console.WriteLine("XML File Path = " + args[0]);
                StreamReader reader = new StreamReader(xmlfilepath);
                StructuralElementsLists elementsLists = (StructuralElementsLists)serializer.Deserialize(reader);
                reader.Close();

                // Define material properties:
                foreach (BuildingMaterialForXML mat in elementsLists.buildingMaterialForXMLList)
                {
                    eMatType sapType = eMatType.Steel;
                    switch (mat.type)
                    {
                        case "steel": { sapType = eMatType.Steel; break; }
                        case "concrete": { sapType = eMatType.Concrete; break; }
                        case "aluminum": { sapType = eMatType.Aluminum; break; }
                        case "coldformed": { sapType = eMatType.ColdFormed; break; }
                        case "rebar": { sapType = eMatType.Rebar; break; }
                        case "tendon": { sapType = eMatType.Tendon; break; }
                    }
                    string retName = "";
                    ret = mySapModel.PropMaterial.AddMaterial(ref retName, sapType, mat.region, mat.standard, mat.grade, mat.name);
                    Console.WriteLine("Adding material \"" + mat.name + "\"...");
                }


                // Define section properties:
                foreach (FrameSectionForXML fsec in elementsLists.frameSectionForXMLList)
                {
                    switch (fsec.type)
                    {
                        case (int)FrameSectionType.I:
                            {
                                mySapModel.PropFrame.SetISection(fsec.name, fsec.buildingMaterialName, fsec.dimensions[0], fsec.dimensions[1], fsec.dimensions[2], fsec.dimensions[3], fsec.dimensions[4], fsec.dimensions[5]);
                                break;
                            }
                        case (int)FrameSectionType.Pipe:
                            {
                                mySapModel.PropFrame.SetPipe(fsec.name, fsec.buildingMaterialName, fsec.dimensions[0], fsec.dimensions[1]);
                                break;
                            }
                        case (int)FrameSectionType.Tube:
                            {
                                mySapModel.PropFrame.SetTube(fsec.name, fsec.buildingMaterialName, fsec.dimensions[0], fsec.dimensions[1], fsec.dimensions[2], fsec.dimensions[3]);
                                break;
                            }
                    }
                    Console.WriteLine("Adding section \"" +  fsec.name + "\"...");
                }

                // Put Frames in:

                int numberOfFrames = elementsLists.frameForXMLList.Count();

                //foreach (FrameForXML frame in elementsLists.frameForXMLList)
                string[] FrameName = new string[numberOfFrames];
                int i;
                for (i = 0; i < numberOfFrames; i++)
                {
                    FrameForXML frame = elementsLists.frameForXMLList.ElementAt(i);
                    Console.WriteLine("Adding frame " + (i + 1) + " of " + numberOfFrames + "...");
                    string temp_string1 = "";
                    ret = mySapModel.FrameObj.AddByCoord(frame.startPos.x, frame.startPos.z, frame.startPos.y, frame.endPos.x, frame.endPos.z, frame.endPos.y, ref temp_string1, frame.sectionPropertyName, "", "Global");
                    FrameName[i] = temp_string1;
                }

                // Put Joint Restraints in:
                //foreach (jointRestraintForXML jr in elementsLists.jointRestraintForXMLList)
                int numberOfJointRestraints = elementsLists.jointRestraintForXMLList.Count();
                for (i = 0; i < numberOfJointRestraints; i++)
                {
                    jointRestraintForXML jr = elementsLists.jointRestraintForXMLList.ElementAt(i);

                    bool[] dof = new bool[6]; // dof = degrees of freedom
                    dof[0] = !jr.transX;
                    dof[1] = !jr.transZ;
                    dof[2] = !jr.transY;
                    dof[3] = !jr.rotX;
                    dof[4] = !jr.rotZ;
                    dof[5] = !jr.rotY;

                    Vector3 restraintCoordinates = jr.position;

                    string targetPointObjectName = "";
                    string[] pointObjectName = { "", "" };
                    double x1 = 0.0;
                    double y1 = 0.0;
                    double z1 = 0.0;
                    for (int j = 0; j < FrameName.Length; j++)
                    {
                        ret = mySapModel.FrameObj.GetPoints(FrameName[j], ref pointObjectName[0], ref pointObjectName[1]);

                        for (int k = 0; k < pointObjectName.Length; k++)
                        {
                            ret = mySapModel.PointObj.GetCoordCartesian(pointObjectName[k], ref x1, ref y1, ref z1, "Global");
                            if (restraintCoordinates.x == x1 && restraintCoordinates.y == z1 && restraintCoordinates.z == y1)
                            {
                                targetPointObjectName = pointObjectName[k];
                                break;
                            }
                        }
                        if (!targetPointObjectName.Equals(""))
                        {
                            break;
                        }
                    }

                    ret = mySapModel.PointObj.SetRestraint(targetPointObjectName, ref dof);
                    Console.WriteLine("Adding joint restraint " + (i + 1) + " of " + numberOfJointRestraints + "...");
                }

            }
            catch (Exception e)
            {
                Console.Write("Exception was thrown when trying to deserialzie XML file.");
                Console.WriteLine(e.ToString());
            }



            //add frame object by coordinates
            /*
                        string[] FrameName = new string[3];

                        string temp_string1 = FrameName[0];
                        string temp_string2 = FrameName[0];

                        ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, 10, ref temp_string1, "R1", "1", "Global");
                        FrameName[0] = temp_string1;

                        ret = mySapModel.FrameObj.AddByCoord(0, 0, 10, 8, 0, 16, ref temp_string1, "R1", "2", "Global");
                        FrameName[1] = temp_string1;

                        ret = mySapModel.FrameObj.AddByCoord(-4, 0, 10, 0, 0, 10, ref temp_string1, "R1", "3", "Global");
                        FrameName[2] = temp_string1;
            */
            /*
            //assign point object restraint at base
            string[] PointName = new string[2];
            bool[] Restraint = new bool[6];

            for (i = 0; i <= 3; i++)
            {
                Restraint[i] = true;
            }

            for (i = 4; i <= 5; i++)
            {
                Restraint[i] = false;
            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;
            PointName[1] = temp_string2;

            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref Restraint, 0);

        
            //assign point object restraint at top
            for (i = 0; i <= 1; i++)
            {
                Restraint[i] = true;
            }

            for (i = 2; i <= 5; i++)
            {
                Restraint[i] = false;
            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;
            PointName[1] = temp_string2;

            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref Restraint, 0);
            */

            //refresh view, update (initialize) zoom
            bool temp_bool = false;
            ret = mySapModel.View.RefreshView(0, temp_bool);

            /*
            //add load patterns
            temp_bool = true;

            ret = mySapModel.LoadPatterns.Add("1", eLoadPatternType.Other, 1, temp_bool);
            ret = mySapModel.LoadPatterns.Add("2", eLoadPatternType.Other, 0, temp_bool);
            ret = mySapModel.LoadPatterns.Add("3", eLoadPatternType.Other, 0, temp_bool);
            ret = mySapModel.LoadPatterns.Add("4", eLoadPatternType.Other, 0, temp_bool);
            ret = mySapModel.LoadPatterns.Add("5", eLoadPatternType.Other, 0, temp_bool);
            ret = mySapModel.LoadPatterns.Add("6", eLoadPatternType.Other, 0, temp_bool);
            ret = mySapModel.LoadPatterns.Add("7", eLoadPatternType.Other, 0, temp_bool);


            //assign loading for load pattern 2
            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;
            PointName[1] = temp_string2;

            double[] PointLoadValue = new double[6];

            PointLoadValue[2] = -10;

            ret = mySapModel.PointObj.SetLoadForce(PointName[0], "2", ref PointLoadValue, false, "Global", 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "2", 1, 10, 0, 1, 1.8, 1.8, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);


            //assign loading for load pattern 3
            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;
            PointName[1] = temp_string2;

            PointLoadValue = new double[6];

            PointLoadValue[2] = -17.2;
            PointLoadValue[4] = -54.4;
            ret = mySapModel.PointObj.SetLoadForce(PointName[1], "3", ref PointLoadValue, false, "Global", 0);


            //assign loading for load pattern 4
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "4", 1, 11, 0, 1, 2, 2, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);


            //assign loading for load pattern 5
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "5", 1, 2, 0, 1, 2, 2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "5", 1, 2, 0, 1, -2, -2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);


            //assign loading for load pattern 6
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "6", 1, 2, 0, 1, 0.9984, 0.3744, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "6", 1, 2, 0, 1, -0.3744, 0, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);


            //assign loading for load pattern 7
            ret = mySapModel.FrameObj.SetLoadPoint(FrameName[1], "7", 1, 2, 0.5, -15, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            */

            //switch to k-in units
            //ret = mySapModel.SetPresentUnits(eUnits.kip_in_F);


            //save model
            ret = mySapModel.File.Save(ModelPath);

            /*
            //run model (this will create the analysis model)
            ret = mySapModel.Analyze.RunAnalysis();


            //initialize for SAP2000 results
            double[] SapResult = new double[7];

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;
            PointName[1] = temp_string2;


            //get SAP2000 results for load patterns 1 through 7           
            int NumberResults = 0;

            string[] Obj = new string[1];
            string[] Elm = new string[1];
            string[] LoadCase = new string[1];
            string[] StepType = new string[1];

            double[] StepNum = new double[1];
            double[] U1 = new double[1];
            double[] U2 = new double[1];
            double[] U3 = new double[1];
            double[] R1 = new double[1];
            double[] R2 = new double[1];
            double[] R3 = new double[1];

            for (i = 0; i <= 6; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));

                if (i <= 3)
                {
                    ret = mySapModel.Results.JointDispl(PointName[1], eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref Elm, ref LoadCase, ref StepType, ref StepNum, ref U1, ref U2, ref U3, ref R1, ref R2, ref R3);
                    U3.CopyTo(U3, 0);
                    SapResult[i] = U3[0];
                }
                else
                {
                    ret = mySapModel.Results.JointDispl(PointName[0], eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref Elm, ref LoadCase, ref StepType, ref StepNum, ref U1, ref U2, ref U3, ref R1, ref R2, ref R3);
                    U1.CopyTo(U1, 0);
                    SapResult[i] = U1[0];
                }
            }


            //close SAP2000
            mySapObject.ApplicationExit(false);
            mySapModel = null;
            mySapObject = null;


            //fill SAP2000 result strings
            string[] SapResultString = new string[7];

            for (i = 0; i <= 6; i++)
            {
                SapResultString[i] = string.Format("{0:0.00000}", SapResult[i]);

                ret = (string.Compare(SapResultString[i], 1, "-", 1, 1, true));

                if (ret != 0)
                {
                    SapResultString[i] = " " + SapResultString[i];
                }
            }


            //fill independent results
            double[] IndResult = new double[7];
            string[] IndResultString = new string[7];

            IndResult[0] = -0.02639;
            IndResult[1] = 0.06296;
            IndResult[2] = 0.06296;
            IndResult[3] = -0.2963;
            IndResult[4] = 0.3125;
            IndResult[5] = 0.11556;
            IndResult[6] = 0.00651;

            for (i = 0; i <= 6; i++)
            {
                IndResultString[i] = string.Format("{0:0.00000}", IndResult[i]);

                ret = (string.Compare(IndResultString[i], 1, "-", 1, 1, true));

                if (ret != 0)
                {
                    IndResultString[i] = " " + IndResultString[i];
                }
            }


            //fill percent difference
            double[] PercentDiff = new double[7];
            string[] PercentDiffString = new string[7];

            for (i = 0; i <= 6; i++)
            {
                PercentDiff[i] = (SapResult[i] / IndResult[i]) - 1;
                PercentDiffString[i] = string.Format("{0:0%}", PercentDiff[i]);

                ret = (string.Compare(PercentDiffString[i], 1, "-", 1, 1, true));

                if (ret != 0)
                {
                    PercentDiffString[i] = " " + PercentDiffString[i];
                }
            }


            //display message box comparing results
            string msg = "";
            msg = msg + "LC  Sap2000  Independent  %Diff\r\n";

            for (i = 0; i <= 5; i++)
            {
                msg = msg + string.Format("{0:0}", i + 1) + "    " + SapResultString[i] + "   " + IndResultString[i] + "       " + PercentDiffString[i] + "\r\n";
            }

            msg = msg + string.Format("{0:0}", i + 1) + "    " + SapResultString[i] + "   " + IndResultString[i] + "       " + PercentDiffString[i];

            Console.WriteLine(msg);
            Console.ReadKey();
            */

            //EXAMPLE CODE END

            Console.ReadKey();

        }
    }

    //[XmlRoot(ElementName = "StructuralElementsLists")]
    [XmlRoot("StructuralElementsLists")]
    public class StructuralElementsLists
    {
        [XmlArray("buildingMaterialForXMLArray"), XmlArrayItem(typeof(BuildingMaterialForXML), ElementName = "BuildingMaterialForXML")]
        public List<BuildingMaterialForXML> buildingMaterialForXMLList { get; set; }

        [XmlArray("frameSectionForXMLArray"), XmlArrayItem(typeof(FrameSectionForXML), ElementName = "FrameSectionForXML")]
        public List<FrameSectionForXML> frameSectionForXMLList { get; set; }

        [XmlArray("frameForXMLArray"), XmlArrayItem(typeof(FrameForXML), ElementName = "FrameForXML")]
        public List<FrameForXML> frameForXMLList { get; set; }

        [XmlArray("jointRestraintForXMLArray"), XmlArrayItem(typeof(jointRestraintForXML), ElementName = "jointRestraintForXML")]
        public List<jointRestraintForXML> jointRestraintForXMLList { get; set; }

        public StructuralElementsLists()
        {
            frameForXMLList = new List<FrameForXML>();
            jointRestraintForXMLList = new List<jointRestraintForXML>();
        }
    }

    [XmlType("FrameForXML")]
    public class FrameForXML
    {
        public Vector3 startPos { get; set; }
        public Vector3 endPos { get; set; }
        public string sectionPropertyName { get; set; }

        public FrameForXML()
        {
            // Empty constructor needed for XML serialization
        }
        public FrameForXML(Vector3 pointA, Vector3 pointB, string sectionName)
        {
            startPos = pointA;
            endPos = pointB;
            sectionPropertyName = sectionName;
        }
    }

    [XmlType("jointRestraintForXML")]
    public class jointRestraintForXML
    {
        public Vector3 position { get; set; }
        public bool transX { get; set; }
        public bool transY { get; set; }
        public bool transZ { get; set; }
        public bool rotX { get; set; }
        public bool rotY { get; set; }
        public bool rotZ { get; set; }

        public jointRestraintForXML()
        {
            // Empty constructor needed for XML serialization
        }

        public jointRestraintForXML(Vector3 pos, bool tX, bool tY, bool tZ, bool rX, bool rY, bool rZ)
        {
            position = pos;
            transX = tX;
            transY = tY;
            transZ = tZ;
            rotX = rX;
            rotY = rY;
            rotZ = rZ;
        }
    }

    [XmlType("BuildingMaterialForXML")]
    public class BuildingMaterialForXML
    {
        public string name { get; set; }
        public string region { get; set; }
        public string type { get; set; }
        public string standard { get; set; }
        public string grade { get; set; }

        public BuildingMaterialForXML() // If constructed with no arguments (This is needed for xml serialization, I think?)
        {
            // Empty constructor needed for XML serialization
        }

        public BuildingMaterialForXML(string givenName, string region, string type, string standard, string grade)
        {
            name = givenName;
            this.region = region;
            this.type = type;
            this.standard = standard;
            this.grade = grade;
        }
    }

    [XmlType("FrameSectionForXML")]
    public class FrameSectionForXML
    {
        public string name;
        public string buildingMaterialName;
        public int type;
        public double[] dimensions = new double[6];

        public FrameSectionForXML()
        {

        }

        public FrameSectionForXML(string name, string buildingMaterialName, int type, double[] dimensions)
        {
            this.name = name;
            this.buildingMaterialName = buildingMaterialName;
            this.type = type;
            for (int i = 0; i < 6; i++)
            {
                this.dimensions[i] = dimensions[i];
            }
        }
    }

    public enum FrameSectionType
    {
        I,
        Channel,
        Tee,
        Angle,
        DoubleAngle,
        DoubleChannel,
        Pipe,
        Tube,
        Rectangular,
        Circular
    }

    [XmlType("FrameForces")]
    public class FrameForces
    {
        public String name;
        public eItemTypeElm itemTypeElm;
        public long numberResults;
        public String[] obj;
        public double[] objSta;
        public String[] elm;
        public String[] elmSta;
        public String[] loadCase;
        public String[] stepType;
        public double[] stepNum;
        public double[] p;
        public double[] v2;
        public double[] v3;
        public double[] t;
        public double[] m2;
        public double[] m3;

    }
}

