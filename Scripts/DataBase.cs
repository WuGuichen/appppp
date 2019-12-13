using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase
{
    public string weaponDatabaseFileName = "weaponData";
	public readonly JSONObject weaponDataBase;

	public DataBase()
	{
		TextAsset weaponCountent = Resources.Load(weaponDatabaseFileName) as TextAsset;
		weaponDataBase = new JSONObject(weaponCountent.text);
	}
}
