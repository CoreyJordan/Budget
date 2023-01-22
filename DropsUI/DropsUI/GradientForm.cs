using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropsUI;

public class GradientForm : Form
{
    private Color _BackColor2 = Color.White;
    private float _ColorAngle = 45f;

    public Color BackColor2
    {
        get { return _BackColor2; }
        set
        {
            _BackColor2 = value;
            //this.Invalidate();
        }
    }

    public float ColorAngle
    {
        get { return _ColorAngle; }
        set
        {
            _ColorAngle = value;
            this.Invalidate();
        }
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        // Creating the rectangle for the gradient
        Rectangle rBackground = new Rectangle(0, 0,
                                  this.Width, this.Height);

        // Creating the lineargradient
        using (LinearGradientBrush bBackground = new LinearGradientBrush(rBackground,
                                              BackColor, _BackColor2, _ColorAngle))
        {
            // Draw the gradient onto the form
            pevent.Graphics.FillRectangle(bBackground, rBackground);
        }
    }
}