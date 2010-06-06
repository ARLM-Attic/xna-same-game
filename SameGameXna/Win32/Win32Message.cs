using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SameGameXna.Win32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Win32Message
	{
		public IntPtr hWnd;
		public IntPtr msg;
		public IntPtr wParam;
		public IntPtr lParam;
		public uint time;
		public System.Drawing.Point p;
	}
}
