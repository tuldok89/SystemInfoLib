using System;
using Microsoft.Win32;

namespace SystemInfoLib.Windows
{
    /// <summary>
    /// Class for getting information related to the OS.
    /// </summary>
    public sealed class OS
    {
        #region Properties
        
        // TODO: Add much more properties related to retrieving information, this is way too small
        // for a class related to an OS. There are multiple possibilities here.

        /// <summary>
        /// Gets the name of the edition of Windows that is installed
        /// </summary>
        public static string WindowsEdition
        {
            get
            {
                return RetrieveWindowsInfo("ProductName");
            }
        }

        /// <summary>
        /// Gets the current build number of Windows
        /// </summary>
        public static string CurrentBuild
        {
            get
            {
                return RetrieveWindowsInfo("CurrentBuild");
            }
        }

        /// <summary>
        /// Gets a string for which service pack is installed on the system.
        /// </summary>
        public static string CSDVersion
        {
            get
            {
                return GetCSDVersion();
            }
        }

        /// <summary>
        /// Checks if the system is 64-bit or 32-bit
        /// </summary>
        public static string Is64BitOperatingSystem
        {
            get
            {
                return Environment.Is64BitOperatingSystem ? "Yes" : "No";
            }
        }

        /// <summary>
        /// Gets the Windows product key
        /// </summary>
        public static string ProductKey
        {
            get
            {
                return DecodeProductKey();
            }
        }

        /// <summary>
        /// Gets the system folder path for Windows
        /// </summary>
        public static string SystemFolder
        {
            get
            {
                return Environment.GetEnvironmentVariable("SystemRoot");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets Windows-related values from specified keys
        /// </summary>
        /// <param name="key">Key to get value for</param>
        /// <returns>Value for the specified key</returns>
        private static string RetrieveWindowsInfo(string key)
        {
            using (RegistryKey rkey =  Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\"))
            {
                if (rkey != null)
                {   
                   return rkey.GetValue(key).ToString();
                }

                return "";
            }
        }

        /// <summary>
        /// Gets the CSDVersion (service pack identifier) from the registry 
        /// and returns it as a string
        /// </summary>
        private static string GetCSDVersion()
        {
            RegistryView view = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            RegistryKey rkey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view);
            rkey = rkey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            
            if (rkey != null)
            {
                string csdVersion = rkey.GetValue("CSDVersion").ToString();
                
                // Close registry key.
                rkey.Close();

                return csdVersion;
            }

            return "";
        }


        /// <summary>
        /// Decodes the encoded  binary registry key that contains Windows' product key 
        /// </summary>
        /// <returns>The decoded product key as a string</returns>
        private static string DecodeProductKey()
        {
            // This view is basically what determines which version of the registry the program will access depending
            // on what [x]-bit version of the OS you are running.
            RegistryView view = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
           
            RegistryKey rkey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view); 
            byte[] digitalProductId = (byte[]) rkey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue("DigitalProductId");
            
            // Close the key, since the data we need is retrieved
            rkey.Close();

            const string allowedChars = "BCDFGHJKMPQRTVWXY2346789";
            char[] decodedChars = new char[29];
            byte[] hexPid = new byte[15];
            
            Array.Copy(digitalProductId, 52, hexPid, 0, 15);
            
            for (int i = 29 - 1; i >= 0; i--)
            {
                if ((i + 1) % 6 == 0)
                {
                    decodedChars[i] = '-';
                }
                else
                {
                    int digitMapIndex = 0;
                    for (int j = 14; j >= 0; j--)
                    {
                        int byteValue = (digitMapIndex << 8) | hexPid[j];
                        hexPid[j] = (byte) (byteValue / 24);
                        digitMapIndex = byteValue % 24;
                        decodedChars[i] = allowedChars[digitMapIndex];
                    }
                }
            }
            
            return new string(decodedChars);
        }

        #endregion
    }
}
