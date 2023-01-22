using System.Drawing.Drawing2D;

namespace DropsUI.Controls;

internal class RoundedPictureBox : PictureBox
{
    // Fields

    private int _borderSize = 2;
    private Color _borderColor = Color.RoyalBlue;
    private Color _borderColor2 = Color.HotPink;
    private DashStyle _borderLineStyle = DashStyle.Solid;
    private DashCap _borderCapStyle = DashCap.Flat;
    private float _gradientAngle = 50F;

    // Constructor
    public RoundedPictureBox()
    {
        Size = new Size(100, 100);
        SizeMode = PictureBoxSizeMode.StretchImage;
    }

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

    public Color BorderColor2
    {
        get
        {
            return _borderColor2;
        }
        set
        {
            _borderColor2 = value;
            Invalidate();
        }
    }

    public DashStyle BorderLineStyle
    {
        get
        {
            return _borderLineStyle;
        }
        set
        {
            _borderLineStyle = value;
            Invalidate();
        }
    }

    public DashCap BorderCapStyle
    {
        get
        {
            return _borderCapStyle;
        }
        set
        {
            _borderCapStyle = value;
            Invalidate();
        }
    }

    public float GradientAngle
    {
        get
        {
            return _gradientAngle;
        }
        set
        {
            _gradientAngle = value;
            Invalidate();
        }
    }

    // Overridden Methods

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Size = new Size(Width, Height);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);
        // Fields

        var graph = pe.Graphics;
        var rectContourSmooth = Rectangle.Inflate(ClientRectangle, -1, -1);
        var rectBorder = Rectangle.Inflate(rectContourSmooth, -_borderSize, -_borderSize);
        var smoothSize = _borderSize > 0 ? _borderSize * 3 : 1;

        using (var borderGColor = new LinearGradientBrush(rectBorder, _borderColor, _borderColor2, _gradientAngle))
        using (var pathRegion = new GraphicsPath())
        using (var penSmooth = new Pen(Parent.BackColor, smoothSize))
        using (var penBorder = new Pen(borderGColor, _borderSize))
        {
            penBorder.DashStyle = _borderLineStyle;
            penBorder.DashCap = _borderCapStyle;
            pathRegion.AddEllipse(rectContourSmooth);
            Region = new Region(pathRegion);
            graph.SmoothingMode = SmoothingMode.AntiAlias;

            // Drawing
            graph.DrawEllipse(penSmooth, rectContourSmooth);
            if (_borderSize > 0)
            {
                graph.DrawEllipse(penBorder, rectBorder);
            }
        }
    }
}