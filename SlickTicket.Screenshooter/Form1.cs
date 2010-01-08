using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace SlickTicket.Screenshooter
{
    public partial class Form1 : Form
    {
        #region private attributes
        private static Bitmap bmpScreenshot;
        private static Graphics gfxScreenshot;
        private bool isDrawActive = false;
        private bool mouseDown; 
        private readonly List<Point> list; 
        private readonly Bitmap bmp;
        private int PenWidth = 3;
        #endregion

        #region cTor
        private void Form1_Load(object sender, EventArgs e)
        {
            // None ;)
        }
        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.photo2;

            this.list = new List<Point>();
            this.bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            comboBox_Color.SelectedItem = "Red";
            comboBox_PenWidth.SelectedItem = "3 px";
        }
        #endregion

        #region Drawing stuff
        private void Draw(Graphics g)
        {
            if (list.Count > 0 && isDrawActive)
            {
                byte[] bs = new byte[list.Count];
                bs[0] = (byte)PathPointType.Start;
                for (int i = 1; i < list.Count; i++)
                    bs[i] = (byte)PathPointType.Line;

                using (Pen p = new Pen(Color.FromName(comboBox_Color.SelectedItem.ToString()), PenWidth))
                    g.DrawPath(p, new System.Drawing.Drawing2D.GraphicsPath(list.ToArray(), bs));
            }
        }
        #endregion

        #region Methods
        private void getPenWidth()
        {
            PenWidth = Convert.ToInt32(comboBox_PenWidth.SelectedItem.ToString().Replace(" px", ""));
        }
        #endregion

        #region Control Actions
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (isDrawActive)
            {
                mouseDown = true;
                list.Add(new Point(e.X, e.Y));
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseDown && isDrawActive)
            {
                mouseDown = false;
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Draw(g);
                    g.Flush();
                }

                list.Clear();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && isDrawActive)
            {
                list.Add(new Point(e.X, e.Y));
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (mouseDown && isDrawActive)
            {
                Draw(Graphics.FromImage(pictureBox1.Image));
            }
        }
        private void btnGetImageFromClipboard_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Clipboard.GetImage();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            sendScreenshot();
        }
        private void btnCreateScreenshot_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread.Sleep(300);
            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            pictureBox1.Image = bmpScreenshot;
            pictureBox1.Size = Screen.PrimaryScreen.Bounds.Size;
            this.Show();
        }
        private void btnEnableDraw_Click(object sender, EventArgs e)
        {
            if (isDrawActive)
            {
                isDrawActive = false;
                btnEnableDraw.BackColor = Color.Transparent;
                comboBox_Color.Enabled = false;
                comboBox_PenWidth.Enabled = false;
            }
            else
            {
                isDrawActive = true;
                btnEnableDraw.BackColor =  Color.GreenYellow;
                comboBox_Color.Enabled = true;
                comboBox_PenWidth.Enabled = true;
            }
        }
        private void comboBox_PenWidth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getPenWidth();
        }
        #endregion

        #region WebService
        private void sendScreenshot()
        {
            wsPutImage.Screenshooter ws = new Screenshooter.wsPutImage.Screenshooter();
            ws.Url = Properties.Settings.Default.Screenshooter_wsGetImage_Screenshooter;
            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            Byte[] img = stream.ToArray();
            ws.PutImage(img, null);

            MessageBox.Show("Screenshot transmitted...");
            pictureBox1.Image = bmp;
            isDrawActive = false;
            btnEnableDraw.BackColor = Color.Transparent;
            comboBox_Color.Enabled = false;
            comboBox_PenWidth.Enabled = false;
        }
        #endregion
    }
}
