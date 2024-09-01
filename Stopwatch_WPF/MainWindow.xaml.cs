using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stopwatch_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private IStopwatch? _stopwatch;

	public MainWindow()
	{
		InitializeComponent();
	}

	private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
	{
		_stopwatch = new Stopwatch
		{
			Callback = timespan =>
			{
				Dispatcher.Invoke(() => { StopwatchTextBlock.Text = timespan.ToString(); });
			},
			Interval = 33
		};

		_stopwatch.OnStop += () =>
		{
			StopwatchTextBlock.Text = TimeSpan.Zero.ToString();
		};
	}

	private void StartButton_OnClick(object sender, RoutedEventArgs e)
	{
		_stopwatch?.Start();
	}

	private void PauseButton_OnClick(object sender, RoutedEventArgs e)
	{
		_stopwatch?.Pause();
	}

	private void StopButton_OnClick(object sender, RoutedEventArgs e)
	{
		_stopwatch?.Stop();
	}
}