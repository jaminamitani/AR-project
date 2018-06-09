using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTracker : MonoBehaviour {

	public int magAmmo = 30;
    private int maxAmmoMag = 30;
	public int ammoReserve = 90;
	public Text uiAmmoText;

	void Start (){
		uiAmmoText = gameObject.GetComponent<Text> ();
		print("ammo: " + transform.GetSiblingIndex ());
	}
		
		

	// return true if reload can occur i.e. enough ammo reserve or the mag ammo is not at max
	// return false if reload cannot occur
    public bool reload()
	{
		if (magAmmo == maxAmmoMag || ammoReserve == 0)
			return false;

        int sub;
        int missingAmmo = maxAmmoMag - magAmmo; //amount of used/missing ammo
        ammoReserve = ammoReserve - missingAmmo;//subtracting used ammo from reserved
        if (missingAmmo > ammoReserve)
        { //if not enough reserve ammo to fully fill mag
            magAmmo = magAmmo + missingAmmo;//add rest of reserve to mag
            ammoReserve = 0;
        }
        else
            magAmmo = maxAmmoMag;

        uiAmmoText.text = (magAmmo.ToString() + "/" + ammoReserve.ToString());
		return true;
    }

	// return true if enough ammo to shoot i.e. mag ammo is greater than 0
	// return false if not enough ammo to shoot
    public bool shootUpdate()
	{
		if (magAmmo > 0) 
		{
			magAmmo--;
			uiAmmoText.text = magAmmo.ToString() + "/" + ammoReserve.ToString();
			return true;
		}
		return false;
    }

	// add amount to ammo reserve and update ammo text
	public void addAmmo(int amount)
	{
		ammoReserve += amount;
		uiAmmoText.text = magAmmo.ToString() + "/" + ammoReserve.ToString();
	}
}
