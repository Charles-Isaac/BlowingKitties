using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace BlowingKitties
{
    public partial class MainForm : Form
    {
        private Queue<double> FPSCount = new Queue<double>();
        private Random rng;
        private System.Timers.Timer explosionTimer;
        private Queue<ProjectileImage> projectiles;
        private int QueueMaxCount = 250;
        private int ExplosionDelay = 1000;


        private int minX = Screen.AllScreens.Aggregate((curMin, x) => curMin.Bounds.Left < x.Bounds.Left ? curMin : x).Bounds.Left;
        private int minY = Screen.AllScreens.Aggregate((curMin, x) => curMin.Bounds.Top < x.Bounds.Top ? curMin : x).Bounds.Top;
        private int maxX = Screen.AllScreens.Aggregate((curMin, x) => curMin.Bounds.Right > x.Bounds.Right ? curMin : x).Bounds.Right;
        private int maxY = Screen.AllScreens.Aggregate((curMin, x) => curMin.Bounds.Bottom > x.Bounds.Bottom ? curMin : x).Bounds.Bottom;

        bool previewMode = false;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        public MainForm(IntPtr PreviewWndHandle)
        {
            InitializeComponent();

            // Set the preview window as the parent of this window
            SetParent(this.Handle, PreviewWndHandle);

            // Make this a child window so it will close when the parent dialog closes
            // GWL_STYLE = -16, WS_CHILD = 0x40000000
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            // Place our window inside the parent
            Rectangle ParentRect;
            GetClientRect(PreviewWndHandle, out ParentRect);
            Size = ParentRect.Size;
            Location = new Point(0, 0);

            InitialyzeCatQueue();

            BackgroundImageLayout = ImageLayout.Stretch;

            previewMode = true;
        }

        private void InitialyzeCatQueue()
        {
            this.projectiles = new Queue<ProjectileImage>();
            Size clientSize1 = this.ClientSize;
            int width1 = clientSize1.Width;
            clientSize1 = this.ClientSize;
            int height1 = clientSize1.Height;
            Projectile.screenSize = new Point(width1, height1);
            Size clientSize2 = this.ClientSize;
            int width2 = clientSize2.Width;
            clientSize2 = this.ClientSize;
            int height2 = clientSize2.Height;
            ProjectileImage.screenSize = new Point(width2, height2);

            var assembly = typeof(BlowingKitties.Program).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("BlowingKitties.Resources.cat.png");

            Bitmap bitmap1 = new Bitmap(resource);
            Bitmap bitmap2 = new Bitmap(bitmap1.Width, bitmap1.Height, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage((Image)bitmap2))
                graphics.DrawImage((Image)bitmap1, new Rectangle(0, 0, ProjectileImage.Size.X, ProjectileImage.Size.Y));
            ProjectileImage.Img = (Image)bitmap2;
            this.explosionTimer = new System.Timers.Timer(ExplosionDelay);
            this.explosionTimer.Elapsed += new ElapsedEventHandler(this.Explode);
            this.explosionTimer.AutoReset = true;
            this.explosionTimer.Start();
        }


        public MainForm()
        {
            this.InitializeComponent();
            this.rng = new Random();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\BlowingKitties_ScreenSaver");

            if (int.TryParse((string)key?.GetValue("MaxCatPartsCount"), out int MaxCatPartsCount))
            {
                QueueMaxCount = MaxCatPartsCount;
            }
            if (int.TryParse((string)key?.GetValue("ExplosionDelay"), out int OutExplosionDelay))
            {
                ExplosionDelay = OutExplosionDelay;
            }

            InitialyzeCatQueue();

            var assembly = typeof(BlowingKitties.Program).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream(new string[] { "BlowingKitties.Resources.xp.jpg", "BlowingKitties.Resources.win10-icon.png", "BlowingKitties.Resources.cat-wallpaper.png", "BlowingKitties.Resources.404-wallpaper.png", }[rng.Next(4)]);
            BackgroundImage = new Bitmap(new Bitmap(resource), new Size(1920, 1080));

            this.Bounds = new Rectangle(0, 0, maxX - minX, maxY - minY);
            this.Location = new Point(minX, minY);
            this.Scale(new SizeF(0.1f, 0.1f));
        }

        private Color HsvToRgb(double h, double S, double V)
        {
            double num1 = h;
            while (num1 < 0.0)
                num1 += 360.0;
            while (num1 >= 360.0)
                num1 -= 360.0;
            double num2;
            double num3;
            double num4;
            if (V <= 0.0)
            {
                double num5;
                num2 = num5 = 0.0;
                num3 = num5;
                num4 = num5;
            }
            else if (S <= 0.0)
            {
                double num5;
                num2 = num5 = V;
                num3 = num5;
                num4 = num5;
            }
            else
            {
                double d = num1 / 60.0;
                int num5 = (int)Math.Floor(d);
                double num6 = d - (double)num5;
                double num7 = V * (1.0 - S);
                double num8 = V * (1.0 - S * num6);
                double num9 = V * (1.0 - S * (1.0 - num6));
                switch (num5)
                {
                    case -1:
                        num4 = V;
                        num3 = num7;
                        num2 = num8;
                        break;
                    case 0:
                        num4 = V;
                        num3 = num9;
                        num2 = num7;
                        break;
                    case 1:
                        num4 = num8;
                        num3 = V;
                        num2 = num7;
                        break;
                    case 2:
                        num4 = num7;
                        num3 = V;
                        num2 = num9;
                        break;
                    case 3:
                        num4 = num7;
                        num3 = num8;
                        num2 = V;
                        break;
                    case 4:
                        num4 = num9;
                        num3 = num7;
                        num2 = V;
                        break;
                    case 5:
                        num4 = V;
                        num3 = num7;
                        num2 = num8;
                        break;
                    case 6:
                        num4 = V;
                        num3 = num9;
                        num2 = num7;
                        break;
                    default:
                        double num10;
                        num2 = num10 = V;
                        num3 = num10;
                        num4 = num10;
                        break;
                }
            }
            return Color.FromArgb((int)byte.MaxValue, this.Clamp((int)(num4 * (double)byte.MaxValue)), this.Clamp((int)(num3 * (double)byte.MaxValue)), this.Clamp((int)(num2 * (double)byte.MaxValue)));
        }

        private int Clamp(int i)
        {
            if (i < 0)
                return 0;
            return i > (int)byte.MaxValue ? (int)byte.MaxValue : i;
        }

        private void Explode(object sender, ElapsedEventArgs e)
        {
            double pstartTime = (double)(DateTime.Now.Ticks / 10000L);
            Point plocation = new Point(this.rng.Next(this.ClientSize.Width), this.ClientSize.Height - Projectile.Size.Y - 10);
            SolidBrush pbrush = new SolidBrush(this.HsvToRgb((double)this.rng.Next(360), 0.9, 0.9));
            for (int index = 0; index < 50; ++index)
            {
                double num1 = this.rng.NextDouble() * 1.5 + 0.5;
                double num2 = this.rng.NextDouble() * 0.8 + 0.1;
                this.AddProjectile(new ProjectileImage(plocation, new PointF((float)(Math.Cos(Math.PI * num2) * num1), (float)(Math.Sin(Math.PI * num2) * num1)), pstartTime, pbrush));
            }
        }

        private void AddProjectile(ProjectileImage p)
        {
            lock (this.projectiles)
            {
                if (this.projectiles.Count > this.QueueMaxCount)
                    this.projectiles.Dequeue();
                this.projectiles.Enqueue(p);
            }
        }

        private void Form1_Click(object sender, EventArgs e) => this.Close();

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            double newTime = (double)(DateTime.Now.Ticks / 10000L);
            while (this.FPSCount.Count > 1 && newTime - this.FPSCount.First<double>() > 1000.0)
                this.FPSCount.Dequeue();
            this.FPSCount.Enqueue(newTime);
            this.FPS.Text = this.FPSCount.Count.ToString();
            lock (this.projectiles)
            {
                foreach (ProjectileImage projectile in this.projectiles)
                    projectile.Draw(e.Graphics, newTime);
            }
            Application.DoEvents();
            this.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) => this.Close();

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Size clientSize1 = this.ClientSize;
            int width1 = clientSize1.Width;
            //clientSize1 = this.ClientSize;
            int height1 = clientSize1.Height;
            Projectile.screenSize = new Point(width1, height1);
            Size clientSize2 = this.ClientSize;
            int width2 = clientSize2.Width;
            //clientSize2 = this.ClientSize;
            int height2 = clientSize2.Height;
            ProjectileImage.screenSize = new Point(width2, height2);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size clientSize1 = this.ClientSize;
            int width1 = clientSize1.Width;
            //clientSize1 = this.ClientSize;
            int height1 = clientSize1.Height;
            Projectile.screenSize = new Point(width1, height1);
            Size clientSize2 = this.ClientSize;
            int width2 = clientSize2.Width;
            //clientSize2 = this.ClientSize;
            int height2 = clientSize2.Height;
            ProjectileImage.screenSize = new Point(width2, height2);
            if (!previewMode)
            {
                this.Bounds = new Rectangle(0, 0, maxX - minX, maxY - minY);
                this.Location = new Point(minX, minY);
            }
        }
    }
}
