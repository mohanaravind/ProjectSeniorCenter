using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Forms;



namespace ProjectSeniorCenter
{    
    /// <summary>
    /// Exposes Win32 methods
    /// </summary>
    public static class Win32
    {
        #region Declarations

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        [DllImport("user32.dll")]
        private static extern int ExitWindowsEx(int operationFlag, int operationReason);

        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the system idle time
        /// </summary>
        /// <returns></returns>
        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }

        /// <summary>
        /// Forces the system to log off
        /// </summary>
        public static void ForceLogOff()
        {
            ExitWindowsEx(4, 0);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the tick count
        /// </summary>
        /// <returns></returns>
        private static long GetTickCount()
        {
            return Environment.TickCount;
        }

        /// <summary>
        /// Gets the last time an input has been received from user
        /// </summary>
        /// <returns></returns>
        private static long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }

            return lastInPut.dwTime;
        }

        #endregion
    }
}
