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

namespace TPL_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}


	private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
	{
		new Thread(async () =>
		{
			for (int i = 0; i < 100; ++i)
			{
				MyProgressBar.Value++;
				await Task.Delay(100);
			}
		}).Start();

		// Task.Run(async () =>
		// {
		// 	Dispatcher.InvokeAsync(async () =>
		// 	{
		// 		for (int i = 0; i < 100; ++i)
		// 		{
		// 			MyProgressBar.Value++;
		// 			await Task.Delay(100);
		// 		}
		// 	});
		// });
	}

	// async Task -> void
	// async Task<T> -> T
}