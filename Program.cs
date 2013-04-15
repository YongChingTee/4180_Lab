using System;
using System.Collections.Generic;
using System.Text;
using Phidgets; //Needed for the RFID class and the PhidgetException class
using Phidgets.Events; //Needed for the phidget event handling classes

namespace RFID_simple
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RFID rfid = new RFID(); //Declare an RFID object

                TextLCD tLCD = new TextLCD();

                // set up the interfacekit object
                InterfaceKit ifKit = new InterfaceKit();

                // link the new interfacekit object to the connected board


                // Get sensorvalue from analog input zero
                //Hook the basica event handlers
                ifKit.Attach += new AttachEventHandler(ifKit_Attach);
                ifKit.Detach += new DetachEventHandler(ifKit_Detach);
                ifKit.Error += new ErrorEventHandler(ifKit_Error);

                //Hook the phidget spcific event handlers
                ifKit.InputChange += new InputChangeEventHandler(ifKit_InputChange);
                ifKit.OutputChange += new OutputChangeEventHandler(ifKit_OutputChange);
                ifKit.SensorChange += new SensorChangeEventHandler(ifKit_SensorChange);


                tLCD.Attach += new AttachEventHandler(tLCD_Attach);
                tLCD.Detach += new DetachEventHandler(tLCD_Detach);
                tLCD.Error += new ErrorEventHandler(tLCD_Error);

                //initialize our Phidgets RFID reader and hook the event handlers
                rfid.Attach += new AttachEventHandler(rfid_Attach);
                rfid.Detach += new DetachEventHandler(rfid_Detach);
                rfid.Error += new ErrorEventHandler(rfid_Error);

                rfid.Tag += new TagEventHandler(rfid_Tag);
                rfid.TagLost += new TagEventHandler(rfid_TagLost);
                rfid.open();
                tLCD.open();
                ifKit.open();

                tLCD.waitForAttachment();
                ifKit.waitForAttachment();
                rfid.waitForAttachment();

                while (true)
                {
                    if (ifKit.sensors[0].Value > 600 || ifKit.sensors[0].Value < 400)
                        tLCD.rows[0].DisplayString = "Motion detected";
                    else
                        tLCD.rows[0].DisplayString = " ";
                }

            }
            catch (PhidgetException ex)
            {
                Console.WriteLine(ex.Description);
            }
        }
        //attach event handler, we'll output the name and serial number of the TextLCD
        //that was attached
        static void tLCD_Attach(object sender, AttachEventArgs e)
        {
            TextLCD attached = (TextLCD)sender;
            string name = attached.Name;
            string serialNo = attached.SerialNumber.ToString();

            Console.WriteLine("TextLCD name:{0} serial No.: {1} Attached!", name,
                                    serialNo);
        }

        //Detach event handler, we'll output the name and serial of the phidget that is
        //detached
        static void tLCD_Detach(object sender, DetachEventArgs e)
        {
            TextLCD detached = (TextLCD)sender;
            string name = detached.Name;
            string serialNo = detached.SerialNumber.ToString();

            Console.WriteLine("TextLCD name:{0} serial No.: {1} Detached!", name,
                                    serialNo);
        }

        //TextLCD error event handler, we'll just output any error data to the console
        static void tLCD_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("LCD Error: e.Description");
        }

        //attach event handler...display the serial number of the attached RFID phidget
        static void rfid_Attach(object sender, AttachEventArgs e)
        {
            //tLCD.rows[0].DisplayString = "RFID TAG: " + e.Device.SerialNumber.ToString();
        }

        //detach event handler...display the serial number of the detached RFID phidget
        static void rfid_Detach(object sender, DetachEventArgs e)
        {
            //tLCD.rows[0].DisplayString = "";
        }

        //Error event handler...display the error description string
        static void rfid_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Description);
        }

        //Print the tag code of the scanned tag
        static void rfid_Tag(object sender, TagEventArgs e)
        {
            Console.WriteLine("Tag {0} scanned", e.Tag);
        }

        //print the tag code for the tag that was just lost
        static void rfid_TagLost(object sender, TagEventArgs e)
        {
            Console.WriteLine("Tag {0} lost", e.Tag);
        }

        //Attach event handler...Display the serial number of the attached InterfaceKit 
        //to the console
        static void ifKit_Attach(object sender, AttachEventArgs e)
        {
            Console.WriteLine("InterfaceKit {0} attached!",
                                e.Device.SerialNumber.ToString());
        }

        //Detach event handler...Display the serial number of the detached InterfaceKit 
        //to the console
        static void ifKit_Detach(object sender, DetachEventArgs e)
        {
            Console.WriteLine("InterfaceKit {0} detached!",
                                e.Device.SerialNumber.ToString());
        }

        //Error event handler...Display the error description to the console
        static void ifKit_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.Description);
        }

        //Input Change event handler...Display the input index and the new value to the 
        //console
        static void ifKit_InputChange(object sender, InputChangeEventArgs e)
        {
            Console.WriteLine("Input index {0} value (1)", e.Index, e.Value.ToString());
        }

        //Output change event handler...Display the output index and the new valu to 
        //the console
        static void ifKit_OutputChange(object sender, OutputChangeEventArgs e)
        {
            Console.WriteLine("Output index {0} value {0}", e.Index, e.Value.ToString());
        }

        //Sensor Change event handler...Display the sensor index and it's new value to 
        //the console
        static void ifKit_SensorChange(object sender, SensorChangeEventArgs e)
        {
            
        }
    }
}
