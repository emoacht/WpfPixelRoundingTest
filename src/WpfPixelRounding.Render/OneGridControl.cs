using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfPixelRounding.Render;

public class OneGridControl : Control
{
	public OneGridControl()
	{
		this.Width = 10;
		this.Height = 10;
	}

	protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
	{
		base.OnDpiChanged(oldDpi, newDpi);

		this.InvalidateVisual();
	}

	protected override void OnRender(DrawingContext drawingContext)
	{
		var line = new Pen(this.Foreground, 1D);
		var lineRadius = line.Thickness / 2;

		// Prepare rectangles.
		var baseRect = new Rect(0, 0, this.Width, this.Height);

		var actualRect = new Rect(
			baseRect.X + lineRadius,
			baseRect.Y + lineRadius,
			baseRect.Width - lineRadius * 2,
			baseRect.Height - lineRadius * 2);

		// Set guidelines.
		var guidelines = new GuidelineSet();
		guidelines.GuidelinesX.Add(baseRect.Left);
		guidelines.GuidelinesX.Add(baseRect.Right);
		guidelines.GuidelinesY.Add(baseRect.Top);
		guidelines.GuidelinesY.Add(baseRect.Bottom);

		drawingContext.PushGuidelineSet(guidelines);

		// Draw rectangle.
		drawingContext.DrawRectangle(null, line, actualRect);

		drawingContext.Pop();

		base.OnRender(drawingContext);
	}
}
