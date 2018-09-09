using System;
using UnityEngine;

/*
 * See "Axial Coordinates": https://www.redblobgames.com/grids/hexagons/
 */
[System.Serializable]
public struct HexCoordinates {

	public int X { get; private set; }
    public int Y
    {
        get
        {
            return -X - Z;
        }
    }
    public int Z { get; private set; }

    public HexCoordinates(int x, int z) {
        this.X = x;
        this.Z = z;
	}

    public static HexCoordinates FromOffset(int x, int z)
    {
        // Undo the half-shift from the original offset values
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromWorldPosition(Vector3 position)
    {
        float offset = position.z / (HexUtils.MAX_R * 3f);
        float x = position.x / (HexUtils.MAX_R * 2f) - offset;
        float y = -x - offset;

        int xi = Mathf.RoundToInt(x);
        int yi = Mathf.RoundToInt(y);
        int zi = Mathf.RoundToInt(-x - y);

        if (xi + yi + zi != 0)
        {
            float dX = Mathf.Abs(x - xi);
            float dY = Mathf.Abs(y - yi);
            float dZ = Mathf.Abs(-x - y - zi);

            if (dX > dY && dX > dZ)
            {
                xi = -yi - zi;
            }
            else if (dZ > dY)
            {
                zi = -xi - yi;
            }
        }

        return new HexCoordinates(xi, zi);
    }

    public override string ToString()
    {
        return "(" + this.X + ", " + this.Y + ", " + this.Z + ")";
    }
}
