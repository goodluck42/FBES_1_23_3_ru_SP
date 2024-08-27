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

namespace GUIThreads;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private SynchronizationContext _synchronizationContext;
	
	public MainWindow()
	{
		InitializeComponent();
		
		_synchronizationContext = SynchronizationContext.Current!;
	}

	private bool _isOn = false;
	private int _interval;
	private DateTime _startDatetime;
	
	private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
	{
	}

	private void StartButton_OnClick(object sender, RoutedEventArgs e)
	{
		var stopwatchThread = new Thread(() =>
		{
			while (_isOn)
			{
				Thread.Sleep(_interval);
				
				_synchronizationContext.Send(_ =>
				{
					Stopwatch.Text = (DateTime.Now - _startDatetime).TotalMilliseconds.ToString();
				}, null);
			}
		});
		
		_interval = Convert.ToInt32(StopwatchInterval.Text);
		_startDatetime = DateTime.Now;

		_isOn = true;
		stopwatchThread.Start();
	}
}