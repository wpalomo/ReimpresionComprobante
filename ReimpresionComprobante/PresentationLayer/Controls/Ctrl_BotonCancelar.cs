using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReimpresionComprobante.PresentationLayer.Controls
{
    public partial class Ctrl_BotonCancelar : UserControl
    {
        public Color FondoColor { get; set; }
        public delegate void ControlClickedHandler();
        public event ControlClickedHandler ControlClicked;

        public Ctrl_BotonCancelar()
        {
            InitializeComponent();
            FondoColor = BackColor;
            this.Click += new EventHandler(controlClick);
            label1.Click += new EventHandler(controlClick);
            pictureBox1.Click += new EventHandler(controlClick);
            this.MouseEnter += new EventHandler(controlMouseEnter);
            label1.MouseEnter += new EventHandler(controlMouseEnter);
            pictureBox1.MouseEnter += new EventHandler(controlMouseEnter);
            this.MouseLeave += new EventHandler(controlMouseLeave);
            label1.MouseLeave += new EventHandler(controlMouseLeave);
            pictureBox1.MouseLeave += new EventHandler(controlMouseLeave);
        }

        void controlMouseLeave(object sender, EventArgs e)
        {
            BackColor = FondoColor;
        }

        void controlMouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Orange;
        }

        void controlClick(object sender, EventArgs e)
        {
            OnControlClicked();
        }

        protected virtual void OnControlClicked()
        {
            if (ControlClicked != null)
            {
                ControlClicked();  
            }
        }
    }
}
