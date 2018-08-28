using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Management;
using System.Drawing.Printing;
//using System.Runtime.InteropServices;
using System.ComponentModel;
//using System.Printing;
using System.IO.Ports;
using System.Net;
//using System.Management;

            


namespace Falp
{
    public class ValidaMenu
    {
        # region Variables privadas
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int SW_MAXIMIZE = 3;
        private const int SW_RESTAURA = 1;
        # endregion

        # region Levanta EXE
        public static void SwitchToCurrent()
        {
            var hWnd = IntPtr.Zero;
            var thisProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(thisProcess.ProcessName)
                                    .Where(p => p.Id != thisProcess.Id);
            foreach (Process runningProcesses in processes)
            {
                if (runningProcesses.MainModule.FileName == thisProcess.MainModule.FileName &&
                    runningProcesses.MainWindowHandle != IntPtr.Zero)
                {
                    hWnd = runningProcesses.MainWindowHandle;
                    ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTAURA);
                    SetForegroundWindow(hWnd);
                    break;
                }
            }
        }
        # endregion

        # region Crea archivo Falp.config e inserta usuario
        public static void CreaFalp(string DBUSER)
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);

            using (StreamWriter fileWrite = new StreamWriter(RutaTemp))
            {
                fileWrite.WriteLine("[USUARIO]\n");
                fileWrite.WriteLine(DBUSER + "\n");
            }
        }
        # endregion

        # region Escribe en archivo Falp.config, el nombre de la instancia (.exe)
        public static void InsertaExeFalp(DataTable DtExe)
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);

            using (StreamWriter sw = File.AppendText(RutaTemp))
            {
                sw.WriteLine("[EJECUTABLES]\n");
                foreach (DataRow FilaExe in DtExe.Rows)
                {
                    sw.WriteLine(FilaExe["NOM_EXE"].ToString().Trim() + "\n");
                }
            } 
        }
        # endregion

        # region Retorna Usuario
        public static string LeeUsuarioMenu()
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);
            bool OkUser = false;
            string DBUSER = string.Empty;

            try
            {
                using (StreamReader fielRead = new StreamReader(RutaTemp))
                {
                    String Linea;
                    while ((Linea = fielRead.ReadLine()) != null)
                    {
                        if (Linea != "")
                        {
                            if (OkUser == true)
                            {
                                DBUSER = Linea;
                                break;
                            }

                            if (Linea == "[USUARIO]")
                            {
                                OkUser = true;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Debe iniciar sesión", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //foreach (Process process in Process.GetProcesses())
                //{
                //    if (process.ProcessName.ToString() == NomExe)
                //    {
                //        process.Kill();
                //    }
                //}


            }

            return DBUSER;
        }
        # endregion

        # region Cierra todas las instancias y borra archivo Falp.config
        public static void BorrarFalp()
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);

            if (File.Exists(RutaTemp))
            {
                using (StreamReader fielRead = new StreamReader(RutaTemp))
                {
                    String Linea;
                    while ((Linea = fielRead.ReadLine()) != null)
                    {
                        if (Linea != "")
                        {
                            if (Linea.Contains(".exe"))
                            {
                                foreach (Process process in Process.GetProcesses())
                                {
                                    if (process.ProcessName.ToString() == Linea.Replace(".exe", ""))
                                    {
                                        process.Kill();
                                    }
                                }
                            }
                        }
                    }
                }

                File.Delete(RutaTemp);
            }
        }
        # endregion

        # region Valida Exe en perfil usuario
        public static bool ValidaExe(string NomExe)
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);
            bool Ok = false;

            if (File.Exists(RutaTemp))
            {
                using (StreamReader fileRead = new StreamReader(RutaTemp))
                {
                    String Linea;
                    while ((Linea = fileRead.ReadLine()) != null)
                    {
                        if (Linea.Replace(".exe", "").ToUpper() == NomExe.ToUpper())
                        {
                            Ok = true;
                        }
                    }
                }
            }

            return Ok;
        }
        # endregion

        # region Valida si existe archivo
        public static bool ValidaArchivo(string NomExe)
        {
            string RutaTemp = System.IO.Path.GetTempPath();
            string Archivo = "Falp.config";
            RutaTemp = System.IO.Path.Combine(RutaTemp, Archivo);
            bool Ok = false;
            if (File.Exists(RutaTemp))
            {
                Ok = true;
            }

            return Ok;
        }
        # endregion

    }


    public class Impresoras
    {
        private string v_Puerto;
        public string Puerto
        {
            get { return v_Puerto; }
            set { v_Puerto = value; }
        }
        
        private string v_Nombre;
        public string Nombre
        {
            get { return v_Nombre; }
            set { v_Nombre = value; }
        }

        private string v_Drivers;

        public string Drivers
        {
            get { return v_Drivers; }
            set { v_Drivers = value; }
        }

        public Impresoras(string Impresora)
        {
            RecuperaDatosImpresora(Impresora);
        }

        private void RecuperaDatosImpresora(string Impresora)
        {
            //aqui

           // //String query = String.Format("Select Name, PortName from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
           //String query = String.Format("Select * from Win32_Printer WHERE Name LIKE '%{0}'", Impresora);
           // ManagementObjectSearcher printers = new ManagementObjectSearcher(query);
           // foreach (ManagementObject printer in printers.Get())
           // {
           //     v_Nombre = (string)printer.GetPropertyValue("Name");
           //     //Console.WriteLine(DeviceName);
           //     v_Puerto = (string)printer.GetPropertyValue("PortName");   
           //     //Console.WriteLine(PortName);
           //     v_Drivers = (string)printer.GetPropertyValue("DriverName");

                
           // }

        }
    }
}
