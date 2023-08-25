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

    public int GetWeapon()
    {
        for (int i = 0; i < this.weaponTypes.Length; i++)
        {
            //If we find the index return the current index
            if (this.weaponTypes[i] == this.weapon)
            {
                return i;
            }
        }
        //Nothing found. Return a negative number
        return -1;
    }

    public void SwitchWeapons(int id) {
        this.weapon = this.weaponTypes[id];
    }
}
