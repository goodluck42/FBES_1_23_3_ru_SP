namespace Stopwatch_WPF;

internal class Stopwatch : IStopwatch
{
	private Task? _activeTask;
	private DateTime _startTime;
	private CancellationTokenSource _cts = new();
	private ManualResetEventSlim _manualResetEvent = new(true);

	public void Start()
	{
		switch (State)
		{
			case StopwatchState.Started:
			{
				throw new InvalidOperationException(
					string.Format(RS.StopwatchStartingEx_Format, nameof(Stopwatch)));	
			}
			case StopwatchState.Paused:
			{
				_manualResetEvent.Set();
				State = StopwatchState.Started;
				
				break;
			}
			case StopwatchState.Stopped:
			{
				_cts.Dispose();
				_cts = new();
				goto case StopwatchState.Idle;
			}
			case StopwatchState.Idle:
			{
				_activeTask = Task.Factory.StartNew(StopwatchAction, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
				_startTime = DateTime.Now;
				State = StopwatchState.Started;
				OnStart?.Invoke();
				
				break;
			}
		}
	}

	public void Pause()
	{
		if (State is StopwatchState.Started)
		{
			_manualResetEvent.Reset();
			State = StopwatchState.Paused;
			OnPause?.Invoke();
			
			return;
		}

		throw new InvalidOperationException();
	}

	public void Stop()
	{
		if (State is StopwatchState.Started or StopwatchState.Paused)
		{
			_manualResetEvent.Set();
			_cts.Cancel();
			State = StopwatchState.Stopped;
			
			OnStop?.Invoke();
			
			return;
		}

		throw new InvalidOperationException();
	}

	public Action<TimeSpan>? Callback { get; set; }
	public int Interval { get; set; } = 100;
	
	private async Task StopwatchAction()
	{
		while (true)
		{
			_manualResetEvent.Wait();

			if (_cts.Token.IsCancellationRequested)
			{
				break;
			}
			
			StopwatchValue = DateTime.Now - _startTime;

			Callback?.Invoke(StopwatchValue);

			await Task.Delay(Interval);
		}
	}


	public TimeSpan StopwatchValue { get; private set; } = TimeSpan.Zero;
	public StopwatchState State { get; private set; }
	
	public event Action? OnStop;
	public event Action? OnStart;
	public event Action? OnPause;
}