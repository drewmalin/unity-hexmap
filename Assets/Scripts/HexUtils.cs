using UnityEngine;

public class HexUtils {

    
    /* From the origin, assume 0th corner is at the "top," proceed clockwise:
     * 
     *      0/6
     *    /     \
     *  5         1
     *  |         |
     *  4         2
     *    \     /
     *       3
     */ 
    public static Vector3[] corners = {
        new Vector3(0f, 0f, MAX_R),
        new Vector3(MIN_R, 0f, 0.5f * MAX_R),
        new Vector3(MIN_R, 0f, -0.5f * MAX_R),
        new Vector3(0f, 0f, -MAX_R),
        new Vector3(-MIN_R, 0f, -0.5f * MAX_R),
        new Vector3(-MIN_R, 0f, 0.5f * MAX_R),
        new Vector3(0f, 0f, MAX_R)
    };

    public const float MAX_R = 10f;
    public const float MIN_R = MAX_R * minToMaxRatio;

    private const float minToMaxRatio = 0.86602540378f;
}
