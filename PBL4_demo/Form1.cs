using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;

namespace PBL4_demo
{
    public partial class Form1 : Form
    {
        PerformanceCounter cpuperf = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        PerformanceCounter ramCount = new PerformanceCounter("Memory", "Available MBytes");
        PerformanceCounter uptime = new PerformanceCounter("System", "System Up Time");
        public Form1()
        {

            InitializeComponent();
            String ipadress = "192.168.1.7";
            Ping myping = new Ping();
            PingReply reply = myping.Send(ipadress, 5000);
            if(reply.Status == IPStatus.Success)
            {
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ipadress);
                    String machineName = hostEntry.HostName;
                    label4.Text = machineName;
                }
                catch(Exception e)
                {
                    label4.Text = "Ping được nhưng ko có tên device";
                }
            }
            else
            {
                label4.Text = "cannot connect to this IP";
            }
 

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (!NetworkInterface.GetIsNetworkAvailable())
            //    return;

            //NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            //foreach (NetworkInterface ni in interfaces)
            //{
            //    label5.Text = "Recieve:  " + ni.GetIPv4Statistics().BytesSent + " ,Send:  " + ni.GetIPv4Statistics().BytesReceived + " (kbps)";
            //}
            label1.Text = "CPU Usage " + (int)cpuperf.NextValue() +" %";
            label2.Text = "RAM Available " + (int)ramCount.NextValue() + " MB";
            label3.Text = "Uptime: " + (int)uptime.NextValue() + " second";

            PerformanceCounterCategory pcg = new PerformanceCounterCategory("Network Interface");
            string instance = pcg.GetInstanceNames()[0];
            PerformanceCounter pcsent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);
            PerformanceCounter pcreceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", instance);
            label5.Text = "Sent: " + pcsent.NextValue().ToString() + ", recieve: " + pcreceived.NextValue().ToString() +" (kbps)";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
