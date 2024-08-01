using System.Diagnostics;
using System.Runtime.InteropServices;
using SystemCalls;

MSG msg = new MSG();



while (Hooks.GetMessageW(msg, IntPtr.Zero, 0, 0))
{
	Console.WriteLine("Message");

	Hooks.TranslateMessage(msg);
	Hooks.DispatchMessageW(msg);
}

public partial class Program
{
	public static void ShowMessageBox()
	{
		MessageBoxFunctions.MyShow(IntPtr.Zero, "Hello from winapi32", "MyTitle", (uint)MessageBoxFunctions.MB_OK);
	}

	public static void ChangeWindowTitle()
	{
		var handle = WindowFunctions.GetWindowByTitle("C:\\Users\\Alex\\Desktop\\fgrades.txt - Notepad++");

		if (handle != IntPtr.Zero)
		{
			WindowFunctions.SetWindowTitle(handle, "MyWindowTitle");
		}
	}

	public static void ChangeCursorPosition()
	{
		for (int i = 0; i < 10; i++)
		{
			int x = Random.Shared.Next(0, 1440);
			int y = Random.Shared.Next(0, 810);

			CursorFunctions.SetCursorPosition(x, y);

			Thread.Sleep(100);
		}
	}
}

[StructLayout(LayoutKind.Sequential)]
public class POINT
{
	public int x;
	public int y;
}

[StructLayout(LayoutKind.Sequential)]
public class MSG
{
	public IntPtr hwnd;
	public uint message;
	public long wParam;
	public long lParam;
	public uint time;
	public POINT pt;
}

public static class Hooks
{
	[DllImport("user32.dll")]
	public static extern bool TranslateMessage([MarshalAs(UnmanagedType.Struct)] MSG msg);
	
	[DllImport("user32.dll")]
	public static extern bool DispatchMessageW([MarshalAs(UnmanagedType.Struct)] MSG msg);

	[DllImport("user32.dll")]
	public static extern bool GetMessageW([MarshalAs(UnmanagedType.LPStruct)] MSG msg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
}

/*
 * BOOL GetMessage(
  [out]          LPMSG lpMsg,
  [in, optional] HWND  hWnd,
  [in]           UINT  wMsgFilterMin,
  [in]           UINT  wMsgFilterMax
);
 */