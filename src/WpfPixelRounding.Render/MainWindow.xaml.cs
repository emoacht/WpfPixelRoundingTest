using System;
using System.Windows;
using System.Windows.Media;

namespace WpfPixelRounding.Render;

public partial class MainWindow : Window
{
	DpiScale InitialSystemDpi { get; }

	public DpiScale CurrentDpi
	{
		get { return (DpiScale)GetValue(CurrentDpiProperty); }
		set { SetValue(CurrentDpiProperty, value); }
	}
	public static readonly DependencyProperty CurrentDpiProperty =
		DependencyProperty.Register("CurrentDpi", typeof(DpiScale), typeof(MainWindow), new PropertyMetadata(default(DpiScale)));

	public double CurrentFactor
	{
		get { return (double)GetValue(CurrentFactorProperty); }
		set { SetValue(CurrentFactorProperty, value); }
	}
	public static readonly DependencyProperty CurrentFactorProperty =
		DependencyProperty.Register("CurrentFactor", typeof(double), typeof(MainWindow), new PropertyMetadata(0D));

	public MainWindow()
	{
		InitializeComponent();

		InitialSystemDpi = DpiHelper.GetSystemDpi();
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);

		CurrentDpi = VisualTreeHelper.GetDpi(this);
		CurrentFactor = CurrentDpi.PixelsPerDip / InitialSystemDpi.PixelsPerDip;
	}

	protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
	{
		CurrentDpi = newDpi;
		CurrentFactor = newDpi.PixelsPerDip / InitialSystemDpi.PixelsPerDip;

		base.OnDpiChanged(oldDpi, newDpi);
	}
}
