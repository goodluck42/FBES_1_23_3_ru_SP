// using System.Threading;
using Timer = System.Timers.Timer;

var manualResetEvent = new ManualResetEventSlim(false);
var timer = new Timer
{
	Interval = 1000,
	Enabled = true
};

timer.Elapsed += (sender, eventArgs) =>
{
	Console.WriteLine($"A second passed: {Thread.CurrentThread.ManagedThreadId}");
};

timer.Start();

Console.WriteLine($"MainId: {Thread.CurrentThread.ManagedThreadId}");

manualResetEvent.Wait();

// 1111000011110000111100001111000011110000 -> 101|11110000