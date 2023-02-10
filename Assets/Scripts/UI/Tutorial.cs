using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private GameObject m_tutorial;

	private void Update()
	{
		if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
			m_tutorial.SetActive(false);
	}
}
