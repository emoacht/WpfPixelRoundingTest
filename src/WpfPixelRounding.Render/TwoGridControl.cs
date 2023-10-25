using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfPixelRounding.Render;

public class TwoGridControl : Control
{
	public TwoGridControl()
	{
		this.Width = 11;
		this.Height = 11;
	}

	protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
	{
		base.OnDpiChanged(oldDpi, newDpi);

		this.InvalidateVisual();
	}

	protected override void OnRender(DrawingContext drawingContext)
	{
		var line = new Pen(this.Foreground, 1D); // 1 is base path thickness.
		var lineRadius = line.Thickness / 2;

		// Prepare rectangles.
		var baseRect = new Rect(0, 0, this.Width, this.Height);

		var actualRect = new Rect(
			baseRect.X + lineRadius,
			baseRect.Y + lineRadius,
			baseRect.Width - lineRadius * 2,
			baseRect.Height - lineRadius * 2);

		// Prepare lines.
		var baseCenterX = Math.Round((baseRect.Left + baseRect.Right) / 2D, MidpointRounding.AwayFromZero);
		var baseCenterY = Math.Round((baseRect.Top + baseRect.Bottom) / 2D, MidpointRounding.AwayFromZero);
		var actualCenterX = baseCenterX - lineRadius;
		var actualCenterY = baseCenterY - lineRadius;

		var crossHorizontal1 = new Point(actualRect.Left, actualCenterY);
		var crossHorizontal2 = new Point(actualRect.Right, actualCenterY);
		var crossVertical1 = new Point(actualCenterX, actualRect.Top);
		var crossVertical2 = new Point(actualCenterX, actualRect.Bottom);

		// Set guidelines for rectangle.
		var guidelines1 = new GuidelineSet();
		guidelines1.GuidelinesX.Add(baseRect.Left);
		guidelines1.GuidelinesX.Add(baseRect.Right);
		guidelines1.GuidelinesY.Add(baseRect.Top);
		guidelines1.GuidelinesY.Add(baseRect.Bottom);

		drawingContext.PushGuidelineSet(guidelines1);

		// Draw rectangle.
		drawingContext.DrawRectangle(null, line, actualRect);

		drawingContext.Pop();

		// Set guidelines for lines.
		var guidelines2 = new GuidelineSet();
		guidelines2.GuidelinesX.Add(baseCenterX);
		guidelines2.GuidelinesY.Add(baseCenterY);

		drawingContext.PushGuidelineSet(guidelines2);

		// Draw lines.
		drawingContext.DrawLine(line, crossHorizontal1, crossHorizontal2);
		drawingContext.DrawLine(line, crossVertical1, crossVertical2);

		drawingContext.Pop();

		base.OnRender(drawingContext);
	}
}
