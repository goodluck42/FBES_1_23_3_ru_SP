using System.Runtime.InteropServices;

namespace SystemCalls;

public static class CursorFunctions
{
	[DllImport("user32.dll", EntryPoint = "SetCursorPos")]
	public static extern bool SetCursorPosition(int x, int y);
}