using UnityEngine;

public static class BoidHelper {
    public const float numViewDirections = 200;
    public const float viewAngle = 300;
    public static readonly Vector2[] directions;

    static BoidHelper() {
        directions = new Vector2[(int) BoidHelper.numViewDirections + 1];

        float startAngle = BoidHelper.viewAngle / -2f;
        float angleIncrement = BoidHelper.viewAngle / BoidHelper.numViewDirections;

        for (int i = 0; i <= numViewDirections; i++) {
            float angle = startAngle + (angleIncrement * i);
            float radians = angle * (Mathf.PI / 180);
            
            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);
            directions[i] = new Vector2(x, y);
        }
    }
}
