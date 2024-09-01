namespace Stopwatch_WPF;

internal interface IStopwatch
{
	void Start();
	void Pause();
	void Stop();
	Action<TimeSpan>? Callback { set; }

	event Action? OnStop;
	event Action? OnStart;
	event Action? OnPause;

	int Interval { set; }
	TimeSpan StopwatchValue { get; }
	StopwatchState State { get; }
}