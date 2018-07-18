using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SW_JBOSS
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new SW_JBOSS() 
            //};
            //ServiceBase.Run(ServicesToRun);


                 if (Environment.UserInteractive)
            {
                //Creamos una instancia del servicio
                SW_JBOSS service1 = new SW_JBOSS();
                //Iniciamos el método inicial del servicio
                service1.initservice(args);

                //Creamos una condición de parada para el servicio
                Console.WriteLine("Servicio para iniciar el Jboss ");
                Console.WriteLine("Stop para detener el servicio");
                string ingreso = Console.ReadLine();

                if (ingreso.Equals("STOP", StringComparison.OrdinalIgnoreCase))
                    service1.endService();
            }
            else
            { 
                //Flujo normal de un servicio en windows

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new SW_JBOSS() 
            };
            ServiceBase.Run(ServicesToRun);

            }
         

        
        }
    }
}
