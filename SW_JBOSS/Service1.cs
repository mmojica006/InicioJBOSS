﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SW_JBOSS
{
    public partial class SW_JBOSS : ServiceBase
    {

        private string ipDesarrollo = "10.10.100.38";
        private string ipProduccion = "10.10.100.30";
        private int puertoJboss = 8643; 
        private string dirDesarrollo = @"C:\jboss-4.2.2.GA\bin";
        private string dirProduccion = @"C:\Topaz\jboss-4.2.2.GA\bin";
     

       
        public SW_JBOSS()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MySource")){
                System.Diagnostics.EventLog.CreateEventSource("MySource","MyNewLog");

            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {            
            eventLog1.WriteEntry("Servicio iniciado!");              
            //System.Diagnostics.Process.Start(@"C:\jboss-4.2.2.GA\bin\run.sh");
            try
            {           
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo("run-topaz.bat");
            process.StartInfo.WorkingDirectory = dirProduccion;
            process.StartInfo.CreateNoWindow = true;     
            
            process.Start();
            }
            catch (Exception ex)
            {
                ExceptionLog.SendErrorToText(ex, false);

            }

        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Servicio Finalizado!");
            StopJboss();
            
        }

        internal void initservice(string[] args)
        {
          
            TimeSpan theTime = new TimeSpan(13, 0, 0);
            if (!this.IsPortOpen(ipDesarrollo, puertoJboss, theTime))         
            this.OnStart(args);    
            
        }

        internal void endService()
        {
            this.OnStop();
        }
     
        private   bool IsPortOpen(string host, int port, TimeSpan timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(timeout);
                    if (!success)
                    {
                        return false;
                    }

                    client.EndConnect(result);
                }

            }
            catch
            {
                return false;
            }
            return true;
        }

        private bool StopJboss()
        {
            bool response = false;
            try
            {
                Process currentProcess = Process.GetCurrentProcess();

                Process[] localAll = Process.GetProcesses();

                Process localByName = Process.GetProcessesByName("java").FirstOrDefault();

                if (localByName != null)
                {
                    localByName.Kill();
                    response = true;
                }
            }
            catch (Exception ex)
            {               
                ExceptionLog.SendErrorToText(ex, false);             
            }
       

            return response;
        } 
    
    }
}
