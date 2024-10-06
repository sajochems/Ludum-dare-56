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

        float dist = 0.1f * ((float) rand.Next(0,10));

        if(!ExcludeInnerRadius) {
            dist += (float)rand.Next(0, outerRad);
        } else {
            dist += (float)rand.Next(innerRad, outerRad);
        }

        return new Vector2(Mathf.Cos(angle_rad) * dist, Mathf.Sin(angle_rad) * dist);
    }
}