using System.Runtime.InteropServices;

namespace SystemCalls;

public static class MessageBoxFunctions
{
	public const long MB_OK = 0x00000000L;
	public const long MB_OKCANCEL = 0x00000001L;
	public const long MB_RETRYCANCEL = 0x00000005L;
	public const long MB_YESNO = 0x00000004L;
	public const long MB_YESNOCANCEL = 0x00000003L;

	[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MessageBox")]
	public static extern int MyShow(IntPtr handle, string text, string caption, uint type);
}