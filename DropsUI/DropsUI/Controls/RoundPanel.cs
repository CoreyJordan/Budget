using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DropsUI.Controls;

public class RoundPanel : Panel
{
    // Fields

    private int _borderSize = 0;
    private int _borderRadius = 40;
    private Color _borderColor = Color.FromArgb(0, 42, 50);
    private Color _backColor2 = Color.FromArgb(0, 100, 50);

    // Properties

    [Category("Custom Panel")]
    public int BorderSize
    {
        get
        {
            return _borderSize;
        }
        set
        {
            _borderSize = value;
            Invalidate();
        }
    }

    [Category("Custom Panel")]
    public int BorderRadius
    {
        get
        {
            return _borderRadius;
        }
        set
        {
            if (value < Height)
            {
                _borderRadius = value;
            }
            else
            {
                _borderRadius = Height;
            }
            Invalidate();
        }
    }

    [Category("Custom Panel")]
    public Color BackColor2
    {
        get
        {
            return _backColor2;
        }
        set
        {
            _backColor2 = value;
            Invalidate();
        }
    }

    [Category("Custom Panel")]
    public Color BorderColor
    {
        get
        {
            return _borderColor;
        }
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    [Category("Custom Panel")]
    public Color TextColor
    {
        get { return ForeColor; }
        set { ForeColor = value; }
    }

    // Constructor
    public RoundPanel()
    {
        Size = new Size(150, 40);
        BackColor = Color.FromArgb(53, 167, 255);
        ForeColor = Color.White;
        Resize += new EventHandler(Button_Resize);
    }

    // Methods
    private GraphicsPath GetFigurePath(RectangleF rect, float radius)
    {
        GraphicsPath path = new();
        path.StartFigure();
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
        path.CloseFigure();

        return path;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        RectangleF rectSurface = new(0, 0, Width, Height);
        RectangleF rectBorder = new(1, 1, Width - 0.8F, Height - 1);

        if (_borderRadius > 2) // Rounded buton
        {
            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, _borderRadius))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder, _borderRadius - 1F))
            using (Pen penSurface = new(Parent.BackColor, 2))
            using (Pen penBorder = new(_borderColor, _borderSize))
            using (LinearGradientBrush brush = new(rectSurface, BackColor, BackColor2, LinearGradientMode.ForwardDiagonal))
            {
                penBorder.Alignment = PenAlignment.Inset;
                Region = new(pathSurface);
                // Draw surface border for HD result
                pevent.Graphics.DrawPath(penSurface, pathSurface);
                pevent.Graphics.FillRectangle(brush, rectSurface);
                // Draw border.
                if (_borderSize > 0)
                {
                    pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
        }
        else // Normal button
        {
            Region = new(rectSurface);
            if (_borderSize >= 1)
            {
                using (Pen penBorder = new(_borderColor, _borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                }
            }
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
    }

    private void Container_BackColorChanged(object? sender, EventArgs e)
    {
        if (DesignMode)
        {
            Invalidate();
        }
    }

    private void Button_Resize(object? sender, EventArgs e)
    {
        if (_borderRadius > Height)
        {
            _borderRadius = Height;
        }
    }
}