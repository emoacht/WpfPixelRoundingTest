using System.Runtime.InteropServices;
using System.Windows;

namespace WpfPixelRounding.Render
{
	internal static class DpiHelper
	{
		[DllImport("User32.dll")]
		static extern uint GetDpiForSystem();

		const double DefaultPixelsPerInch = 96D;

		public static DpiScale GetSystemDpi()
		{
			var dpiScale = GetDpiForSystem() / DefaultPixelsPerInch;
			return new DpiScale(dpiScale, dpiScale);
		}
	}
}
