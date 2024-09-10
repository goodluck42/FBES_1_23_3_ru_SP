class MyTaskScheduler : TaskScheduler
{
	protected override IEnumerable<Task>? GetScheduledTasks()
	{
		throw new NotImplementedException();
	}

	protected override void QueueTask(Task task)
	{
		throw new NotImplementedException();
	}

	protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
	{
		throw new NotImplementedException();
	}
}