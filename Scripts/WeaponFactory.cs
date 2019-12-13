﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory 
{
    private DataBase weaponDB;

	public WeaponFactory(DataBase _weaponDB)
	{
		weaponDB = _weaponDB;
		
	}

	public GameObject CreateWeapon(string weaponName, Vector3 pos, Quaternion rot)
	{
		GameObject prefab = Resources.Load(weaponName) as GameObject;
		GameObject obj = GameObject.Instantiate(prefab, pos, rot);

		WeaponData wdata = obj.AddComponent<WeaponData>();
		wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;

		return obj;

	}

	public bool CreateWeapon(string weaponName, string side, WeaponManager wm)
	{
		WeaponController wc;
		if (side == "L")
		{
			wc = wm.wcL;
		}
		else if (side == "R")
		{
			wc = wm.wcR;
		}
		else
			return false;

		GameObject prefab = Resources.Load(weaponName) as GameObject;
		GameObject obj = GameObject.Instantiate(prefab);
		obj.transform.parent = wc.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;

		WeaponData wdata = obj.AddComponent<WeaponData>();
		wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].f;

		return true;
	}

}
