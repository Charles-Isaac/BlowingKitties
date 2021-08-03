// Decompiled with JetBrains decompiler
// Type: ScreenSaver.ProjectileImage
// Assembly: ScreenSaver, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B70AFD0-1708-4B27-A6A9-C85D58DB363D
// Assembly location: C:\Users\charles.IKON0\Desktop\ScreenSaver.exe

using System;
using System.Drawing;

namespace BlowingKitties
{
  internal class ProjectileImage
  {
    public static Image Img;
    private Point location;
    public static Point Size = new Point(30, 30);
    private PointF velocity;
    private double startTime;
    private bool shouldUpdate = true;
    private SolidBrush brush;
    public static Point screenSize;
    private double G = 0.00098;

    public ProjectileImage(
      Point plocation,
      PointF pvelocity,
      double pstartTime,
      SolidBrush pbrush)
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
        if (point.Y + ProjectileImage.Size.Y > ProjectileImage.screenSize.Y)
        {
          point = new Point(point.X, ProjectileImage.screenSize.Y - ProjectileImage.Size.Y);
          this.location = point;
          this.shouldUpdate = false;
        }
        pan.DrawImage(ProjectileImage.Img, point);
      }
      else
        pan.DrawImage(ProjectileImage.Img, this.location);
    }
  }
}
