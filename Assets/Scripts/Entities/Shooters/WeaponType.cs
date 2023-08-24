using UnityEngine;

[CreateAssetMenu(fileName = "Assets", menuName = "Scriptable Objects/Weapon Type")]
public class WeaponType : ScriptableObject {
    public Projectile prefab;
    public float shootDelay;
    public float projectileSpeed = 2f;
    public float shootRange;
    public int damage;

    public bool ready; //This could cause issues if 2 things use the same type at once. To be tested!
}
