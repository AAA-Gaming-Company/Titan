using UnityEngine;

public static class BoidHelper {
    public const int numViewDirections = 200;
    public const float viewAngle = 300;
    public static readonly Vector2[] directions;

    static BoidHelper() {
        directions = new Vector2[BoidHelper.numViewDirections];

        float angleIncrement = BoidHelper.viewAngle / BoidHelper.numViewDirections;
        int halfDir = numViewDirections / 2;

        directions[0] = new Vector2(1, 0); //Right
        directions[100] = new Vector2(1, 0); //Hmm

        for (int i = 1; i < halfDir; i++) {
            float angle = angleIncrement * i;
            float radians = angle * (Mathf.PI / 180);
            
            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);
            directions[i] = new Vector2(x, y);

            radians = -radians;
            x = Mathf.Cos(radians);
            y = Mathf.Sin(radians);
            directions[i + halfDir] = new Vector2(x, y);
        }
    }
}
