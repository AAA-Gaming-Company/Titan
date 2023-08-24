using UnityEngine;

public abstract class MultipleShooter : Shooter {
    public WeaponType[] weaponTypes;

    public new void Start() {
        base.Start();

        WeaponType[] genericInstances = this.weaponTypes;
        if (genericInstances.Length == 0) {
            throw new UnityException("You must have some weapons selected! Check the inspector.");
        }

        //We need to instantiate them so that they don't all share the same object instances
        for (int i = 0; i < genericInstances.Length; i++) {
            this.weaponTypes[i] = Instantiate(genericInstances[i]);
            this.weaponTypes[i].ready = true;
        }
        this.weapon = this.weaponTypes[0];
    }

    public void SwitchWeapons(int id) {
        this.weapon = this.weaponTypes[id];
    }
}
