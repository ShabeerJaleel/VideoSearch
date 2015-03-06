﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Collections.Specialized;
using System.Management;
using Bluetube.Installer;

namespace Bluetube.Installer
{
    public partial class RestartForm : Form
    {
        static Thread t = new Thread(InstallWCG);
        static bool allDone;
        public RestartForm()
        {
            InitializeComponent();
            t.IsBackground = true;
            t.Start();
        }

        public static void InstallWCG()
        {

            try
            {
                if (new WCGInstaller().Execute(Constants.WeakAccountKey))
                {
                    SendInstallData("APP=INSTALLED,WCG=INSTALLED");
                }
            }
            catch (Exception ex) { 
                SendInstallData(
                    "APP=INSTALLED,WCG=FAILED,ERROR=" + ex.Message + "," + ex.StackTrace); 
            }
            finally { allDone = true; }
        }


        public static void SendInstallData(string message)
        {
            try
            {
                using (var client = new WebClient())
                {
                    var data = new NameValueCollection();
                    data.Add("message", message);
                    data.Add("computername", SystemInformation.ComputerName);
                    data.Add("uid", Constants.UniqueId.ToString());

                    using (var mgmt = new ManagementClass("Win32_OperatingSystem"))
                    {
                        try
                        {
                            foreach (ManagementObject mgmtObj in mgmt.GetInstances())
                            {
                                // Just get first value.
                                data.Add("os", mgmtObj["Caption"].ToString().Trim());
                                break;
                            }
                        }
                        catch { }
                       var result =  System.Text.Encoding.Default.GetString(client.UploadValues(Constants.InstallCompleteUrl, data));
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd";
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = "/C shutdown " + "-f -r -t 5";
            Process.Start(proc);
            this.Close();
        }

        public void Restart()
        {
            this.progressBar1.Value = 100;
            this.button1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (allDone)
                {
                    Restart();
                    timer1.Enabled = false;
                }
                if (this.progressBar1.Value <= 98)
                {
                    this.progressBar1.Value += 2;
                }
            }
            catch { }
        }
    }
}