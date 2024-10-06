using UnityEngine;
using System;

public class RadialCoordinateSampler
{
    private int innerRad;
    private int outerRad;
    private System.Random rand;

    public RadialCoordinateSampler(int InnerRadius, int OuterRadius)
    {
        innerRad = InnerRadius;
        outerRad = OuterRadius;
        rand = new System.Random();
    }

    public Vector2 SamplePoint(bool ExcludeInnerRadius) 
    {
        int angle = rand.Next(0, 359);
        float angle_rad = angle * Mathf.Deg2Rad;

        int dist = 0;

        if(!ExcludeInnerRadius) {
            dist = rand.Next(0, outerRad);
        } else {
            dist = rand.Next(innerRad, outerRad);
        }

        return new Vector2(Mathf.Cos(angle_rad) * (float)dist, Mathf.Sin(angle_rad) * (float)dist);
    }
}