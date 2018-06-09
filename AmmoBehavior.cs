using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (00)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			ShootRay (ray);
		}
		if (Input.touchCount > 0) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			ShootRay (ray);
		}
	}

	void ShootRay(Ray ray)
	{
		RaycastHit rhit;
		GameObject objectHit = null;

		if(Physics.Raycast(ray, out rhit, 10000.0f))
		{
			//objectHit = true;
			print ("ammo box hit");
			//gameObject.SendMessage ("addAmmo", 30);
			GameObject.Find("AmmoText").GetComponent<AmmoTracker>().addAmmo(Random.Range(10,30));
			Destroy (gameObject);
		}

	}
	public void touched()
	{
		print ("touched ammmo");
	}
}
