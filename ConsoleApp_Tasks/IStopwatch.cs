namespace ConsoleApp_Tasks;

internal interface IStopwatch
{
	void Start();
	void Pause();
	void Stop();
	Action<TimeSpan>? Callback { set; }
	int Interval { set; }
	TimeSpan StopwatchValue { get; }
	StopwatchState State { get; }
}