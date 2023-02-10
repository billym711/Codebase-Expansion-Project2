using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
	public static void Set(SpriteRenderer rend, Material mat, int matIndex = 0)
	{
		Material[] mats = rend.sharedMaterials;
		mats[matIndex] = mat;
		rend.sharedMaterials = mats;  //Set all mats, otherwise won't work
	}
}
