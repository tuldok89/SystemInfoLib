using Microsoft.Win32;

namespace SystemInfoLib.Windows
{
    /// <summary>
    /// Class for retrieving information related to the BIOS
    /// </summary>
    public sealed class Bios
    {
        #region Properties

        // TODO: Add more properties for retrieving information, this is somewhat lacking.
        // Also, DO IT WITHOUT WMI.

        /// <summary>
        /// Gets the name of the manufacturer of the motherboard the BIOS is in
        /// </summary>
        public static string MotherboardManufacturer
        {
            get
            {
                return RetrieveBiosInfo("BaseBoardManufacturer");
            }
        }

        /// <summary>
        /// Gets the model string for the motherboard
        /// </summary>
        public static string MotherboardModel
        {
            get
            {
                return RetrieveBiosInfo("BaseBoardProduct");
            }
        }

        /// <summary>
        /// Gets the version number of the motherboard.
        /// <remarks>Sometimes, vendors don't specify this at all</remarks>
        /// </summary>
        public static string MotherboardVersion
        {
            get
            {
                return RetrieveBiosInfo("BaseBoardVersion");
            }
        }

        /// <summary>
        /// Gets the release date for the current BIOS firmware
        /// </summary>
        public static string BiosReleaseDate
        {
            get
            {
                return RetrieveBiosInfo("BIOSReleaseDate");
            }
        }

        /// <summary>
        /// Gets the name of the vendor for the current BIOS firmware
        /// </summary>
        public static string BiosVendor
        {
            get
            {
                return RetrieveBiosInfo("BIOSVendor");
            }
        }

        /// <summary>
        /// Retrieves the version number/version letter of the current BIOS firmware
        /// </summary>
        public static string BiosVersion
        {
            get
            {
                return RetrieveBiosInfo("BIOSVersion");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets BIOS values from specified keys
        /// </summary>
        /// <param name="key">Key to get value for</param>
        /// <returns>Value for the specified key</returns>
        private static string RetrieveBiosInfo(string key)
        {
            using (RegistryKey rkey = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\BIOS\\"))
            {
                if (rkey != null)
                {
                    return rkey.GetValue(key).ToString();
                }

                return "";
            }
        }

        #endregion
    }
}
