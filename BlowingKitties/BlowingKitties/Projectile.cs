// Decompiled with JetBrains decompiler
// Type: ScreenSaver.Projectile
// Assembly: ScreenSaver, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B70AFD0-1708-4B27-A6A9-C85D58DB363D
// Assembly location: C:\Users\charles.IKON0\Desktop\ScreenSaver.exe

using System;
using System.Drawing;

namespace BlowingKitties
{
  internal class Projectile
  {
    private Point location;
    public static Point Size = new Point(15, 15);
    private PointF velocity;
    private double startTime;
    private bool shouldUpdate = true;
    private SolidBrush brush;
    public static Point screenSize;
    private double G = 0.00098;

    public Projectile(Point plocation, PointF pvelocity, double pstartTime, SolidBrush pbrush)
    {
      this.location = plocation;
      this.velocity = pvelocity;
      this.startTime = pstartTime;
      Random random = new Random();
      this.brush = pbrush;
    }

    public void Draw(Graphics pan, double newTime)
    {
      if (this.shouldUpdate)
      {
        double num = newTime - this.startTime;
        Point point = new Point(this.location.X + (int) ((double) this.velocity.X * num), (int) ((double) this.location.Y - (double) this.velocity.Y * num + 0.5 * this.G * num * num));
        if (point.Y + Projectile.Size.Y > Projectile.screenSize.Y)
        {
          point = new Point(point.X, Projectile.screenSize.Y - Projectile.Size.Y);
          this.location = point;
          this.shouldUpdate = false;
        }
        pan.FillEllipse((Brush) this.brush, point.X, point.Y, Projectile.Size.X, Projectile.Size.Y);
      }
      else
        pan.FillEllipse((Brush) this.brush, this.location.X, Projectile.screenSize.Y - Projectile.Size.Y, Projectile.Size.X, Projectile.Size.Y);
    }
  }
}
