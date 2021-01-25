using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Mikro_vaylä_ennakkotehtävä
{
    public partial class Form1 : Form
    {
        //string from = "64";

        string status = "[102, 64, 1, crc16]";
        string version = "[103, 64, 1, crc16]";
        string output1On = "[140,64,1,4.1,crc16]";
        string output4On = "[140,64,1,1.1,crc16]";
        //float[] status2 = { 102, from, to };


        //static byte from = 64;
        //static byte to = 1;

        string dataOut;
        string dataIn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPORT.Items.AddRange(ports);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            try
            {
                serialPort1.PortName = cBoxCOMPORT.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);
                serialPort1.Handshake = Handshake.None;
                serialPort1.Open();

                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceiveHandler);

                if (serialPort1.IsOpen)
                {
                    MessageBox.Show("Serial port opened succesfully");
                }
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.tBoxDataIn.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tBoxDataIn.Text = text;
            }
        }

        //private void DataReceiveHandler(object sender,System.IO.Ports.SerialDataReceivedEventArgs e)
        //{
        //    SerialPort sp = (SerialPort)sender;
        //    string inData = sp.ReadExisting();
        //    //tBoxDataOut.Text = inData.ToString();
        //    //SetText(inData.ToString());

        //    inData += serialPort1.ReadExisting().ToString();
        //    SetText(inData.ToString());

        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                MessageBox.Show("Serial port closed succesfully");
            }
        }

        //Lähettää tekstilaatikkoon kirjoitetun datan
        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                dataOut = tBoxDataOut.Text;
                serialPort1.Write(dataOut);
            }
        }

        //Vastaanottaa ja näyttää datan
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIn = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            tBoxDataIn.Text = dataIn;
        }

        //Lukee laitteen statuksen
        private void btnReadStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {

                    tBoxDataOut.Text = status;
                    serialPort1.Write(status);
                }
                else
                {
                    MessageBox.Show("Error: Open serial port and try again");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error writing to serial port:" + err.Message, "Error");
            }
        }

        //Lukee laitteen statuksen
        private void btnVersionNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    tBoxDataOut.Text = version;
                    serialPort1.Write(version);

                }

                else
                {
                    MessageBox.Show("Error: Open serial port and try again");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error writing to serial port:" + err.Message, "Error");
            }
        }

        private void btnOutput1On_Click(object sender, EventArgs e)
        {
             try
            {
                if (serialPort1.IsOpen)
                {
                    tBoxDataOut.Text = output1On;
                    serialPort1.Write(output1On);

                }

                else
                {
                    MessageBox.Show("Error: Open serial port and try again");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error writing to serial port:" + err.Message, "Error");
            }
        }
        

        private void btnClearDataIn_Click(object sender, EventArgs e)
        {
            tBoxDataIn.Text = string.Empty;
        }

        private void btnOutput4On_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    tBoxDataOut.Text = output4On;
                    serialPort1.Write(output4On);

                }

                else
                {
                    MessageBox.Show("Error: Open serial port and try again");
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("Error writing to serial port:" + err.Message, "Error");
            }
        }
    }
    }
