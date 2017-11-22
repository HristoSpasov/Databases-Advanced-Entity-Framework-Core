﻿public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double height)
    {
        this.length = length;
        this.width = width;
        this.height = height;
    }

    public double GetSurfaceArea()
    {
        return 2.0 * this.length * this.width + 2.0 * this.length * this.height + 2.0 * this.width * this.height;
    }

    public double GetLateralArea()
    {
        return 2.0 * this.length * this.height + 2.0 * this.width * this.height;
    }

    public double GetVolume()
    {
        return this.length * this.height * this.width;
    }
}