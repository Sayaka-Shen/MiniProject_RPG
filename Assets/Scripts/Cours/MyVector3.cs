using UnityEngine;


// Struct
public struct MyVector3
{
    public MyVector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    // Magnitude au carré 
    public float Magnitude => Mathf.Sqrt(SqrtMagnitude);

    // Distance entre le point d'origine et le point du Vector
    public float SqrtMagnitude => X * X + Y * Y + Z * Z;

    // Normalize Vector (passe la Magnitude à 1)
    // Divise X, Y ou Z par sa direction au carré 
    // Réduit la direction à 1 peu importe la vraie distance, on garde juste la direction
    public void Normalized()
    {
        var mag = Magnitude;

        X /= mag;
        Y /= mag;
        Z /= mag;
    }

    // Surchage d'operateur + 
    public static MyVector3 operator +(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
    }
    public static MyVector3 operator *(MyVector3 vector1, MyVector3 vector2)
    {
        return new MyVector3(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);
    }
}
