using System.Runtime.InteropServices;

namespace SystemCalls;

public static class WindowFunctions
{
	[DllImport("user32.dll", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern IntPtr GetWindow(string? lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);

	public static IntPtr GetWindowByTitle(string title)
	{
		return GetWindow(null, title);
	}

	[DllImport("user32.dll", EntryPoint = "SetWindowTextW", CharSet = CharSet.Unicode)]
	public static extern bool SetWindowTitle(IntPtr hWnd, string lpString);
}