using System;
using SystemInfoLib.Windows;

namespace SystemInfoExample
{
    class Program
    {
        static void Main()
        {
            // Processor:
            Console.WriteLine("Processor Related:" + Environment.NewLine);
            Console.WriteLine("    Processor Name: " + Processor.Name + Environment.NewLine);
            Console.WriteLine("    Processor Identifier: " + Processor.Identifier + Environment.NewLine);
            Console.WriteLine("    Processor Level: " + Processor.ProcessorLevel + Environment.NewLine);
            Console.WriteLine("    Processor Revision: " + Processor.ProcessorRevision + Environment.NewLine);
            Console.WriteLine("    Processor Architecture: " + Processor.ProcessorArchitecture + Environment.NewLine);
            Console.WriteLine("    Number of Logical Processor Cores: " + Processor.NumberOfLogicalCores + Environment.NewLine);
            Console.WriteLine("    Clock Speed: " + Processor.ClockSpeed + " MHz" + Environment.NewLine);
            Console.WriteLine("    Vendor Identifier: " + Processor.VendorIdentifier + Environment.NewLine + Environment.NewLine);

            // BIOS:
            Console.WriteLine("BIOS Related:" + Environment.NewLine);
            Console.WriteLine("    Motherboard Manufacturer: " + Bios.MotherboardManufacturer + Environment.NewLine);
            Console.WriteLine("    Motherboard Model: " + Bios.MotherboardModel + Environment.NewLine);
            Console.WriteLine("    Motherboard Version: " + Bios.MotherboardVersion + Environment.NewLine);
            Console.WriteLine("    BIOS Release Date: " + Bios.BiosReleaseDate + Environment.NewLine);
            Console.WriteLine("    BIOS Vendor: " + Bios.BiosVendor + Environment.NewLine);
            Console.WriteLine("    BIOS Version: " + Bios.BiosVersion + Environment.NewLine + Environment.NewLine);

            // System:
            Console.WriteLine("System Related:" + Environment.NewLine);
            Console.WriteLine("    64-Bit Operating System: " + OS.Is64BitOperatingSystem + Environment.NewLine);
            Console.WriteLine("    Windows Edition: " + OS.WindowsEdition + Environment.NewLine);
            Console.WriteLine("    Windows Product Key: " + OS.ProductKey + Environment.NewLine);
            Console.WriteLine("    Current Build Number: " + OS.CurrentBuild + Environment.NewLine);
            Console.WriteLine("    CSD Version: " + OS.CSDVersion + Environment.NewLine);
            Console.WriteLine("    System Directory: " + OS.SystemFolder + Environment.NewLine + Environment.NewLine);

            // RAM
            Console.WriteLine("RAM Related: " + Environment.NewLine);
            Console.WriteLine("    Total Physical Memory (in bytes): " + Ram.TotalPhysicalMemory + Environment.NewLine);
            Console.WriteLine("    Total Physical Memory (in megabytes): " + Ram.TotalPhysicalMemory.ToMegaBytes() + Environment.NewLine);

            Console.ReadLine();
        }
    }
}
