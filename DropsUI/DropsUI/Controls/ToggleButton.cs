using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Control_Builder.Controls;

public class ToggleButton : CheckBox
{
    // Fields

    private Color _onBackColor = Color.FromArgb(0, 166, 118);
    private Color _onToggleColor = Color.WhiteSmoke;
    private Color _offBackColor = Color.FromArgb(0, 42, 50);
    private Color _offToggleColor = Color.Gainsboro;
    private Color _clearKey = Color.Transparent;
    private bool _solidStyle = true;

    // Properties
    [Category("BackColor")]
    public Color OnBackColor
    {
        get
        {
            return _onBackColor;
        }
        set
        {
            _onBackColor = value;
            Invalidate();
        }
    }

    [Category("ToggleColor")]
    public Color OnToggleColor
    {
        get
        {
            return _onToggleColor;
        }
        set
        {
            _onToggleColor = value;
            Invalidate();
        }
    }

    [Category("BackColor")]
    public Color OffBackColor
    {
        get
        {
            return _offBackColor;
        }
        set
        {
            _offBackColor = value;
            Invalidate();
        }
    }

    [Category("ToggleColor")]
    public Color OffToggleColor
    {
        get
        {
            return _offToggleColor;
        }
        set
        {
            _offToggleColor = value;
            Invalidate();
        }
    }

    [Category("ClearKey")]
    public Color ClearKey
    {
        get
        {
            return _clearKey;
        }
        set
        {
            _clearKey = value;
            Invalidate();
        }
    }

    public override string Text
    {
        get
        {
            return base.Text;
        }
    }

    [DefaultValue(true)]
    public bool SolidStyle
    {
        get
        {
            return _solidStyle;
        }
        set
        {
            _solidStyle = value;
            Invalidate();
        }
    }

    // Constructor
    public ToggleButton()
    {
        MinimumSize = new Size(45, 22);
    }

    // Methods
    private GraphicsPath GetFigurePath()
    {
        int arcSize = Height - 1;
        Rectangle leftArc = new(0, 0, arcSize, arcSize);
        Rectangle rightArc = new(Width - arcSize - 2, 0, arcSize, arcSize);

        GraphicsPath path = new();
        path.StartFigure();
        path.AddArc(leftArc, 90, 180);
        path.AddArc(rightArc, 270, 180);
        path.CloseFigure();

        return path;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        int toggleSize = Height - 5;
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        pevent.Graphics.Clear(Parent.BackColor);

        if (Checked) // ON
        {
            // Draw the control surface
            if (_solidStyle)
            {
                pevent.Graphics.FillPath(
                    new SolidBrush(_onBackColor), GetFigurePath());
            }
            else
            {
                pevent.Graphics.DrawPath(new Pen(OnBackColor, 2), GetFigurePath());
            }
            // Draw the toggle
            pevent.Graphics.FillEllipse(
                new SolidBrush(_onToggleColor),
                new Rectangle(Width - Height + 1, 2, toggleSize, toggleSize));
        }
        else // OFF
        {
            // Draw the control surface
            if (_solidStyle)
            {
                pevent.Graphics.FillPath(
                    new SolidBrush(_offBackColor), GetFigurePath());
            }
            else
            {
                pevent.Graphics.DrawPath(new Pen(OnBackColor, 2), GetFigurePath());
            }
            // Draw the toggle
            pevent.Graphics.FillEllipse(
                new SolidBrush(_onToggleColor),
                new Rectangle(2, 2, toggleSize, toggleSize));
        }
    }
}