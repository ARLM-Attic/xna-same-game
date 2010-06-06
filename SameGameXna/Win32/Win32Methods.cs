using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SameGameXna.Win32
{
	public class Win32Methods
	{
		[SuppressUnmanagedCodeSecurity]
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool PeekMessage(out Win32Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags); 
	}
}
