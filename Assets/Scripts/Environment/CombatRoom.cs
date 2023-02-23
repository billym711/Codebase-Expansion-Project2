using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class OnRoomClearEvent : UnityEvent { }

public class CombatRoom : MonoBehaviour
{
	public OnRoomClearEvent OnRoomClear;

	[SerializeField] private GameObject[] m_doors;
	[SerializeField] private EnemyCombat[] m_enemies;
	[SerializeField] private GameObject default_char;
	[SerializeField] private GameObject dual_char;
	[SerializeField] private GameObject dual_char2;
	private bool m_active;
	private bool m_complete;

	private void Update()
	{
		if (m_active)
		{
			bool roomClear = true;
			foreach (EnemyCombat enemy in m_enemies)
			{
				if (enemy.GetAlive())
				{
					roomClear = false;
					break;
				}
			}

			if (roomClear)
				Deactivate();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!m_complete && !m_active)
		{
			if (collision.CompareTag("Player") && CharacterSelect.defaultChar)
			{
				Activate();
			}
			if (collision.CompareTag("Player") && collision.name == "Player2" && !CharacterSelect.defaultChar)
            {
				dual_char2.transform.position = dual_char.transform.position + new Vector3(.4f, 0, 0);
				Activate();
			}
		}
	}

	private void Activate()
	{
		foreach (GameObject door in m_doors)
			door.SetActive(true);

		foreach (EnemyCombat enemy in m_enemies)
			enemy.SetAlive(true);

		//Turn off the trigger box for optimisation
		gameObject.GetComponent<Collider2D>().enabled = false;

		m_active = true;
	}
	private void Deactivate()
	{
		m_active = false;
		m_complete = true;

		foreach (GameObject door in m_doors)
			door.SetActive(false);

		OnRoomClear?.Invoke();
	}
}
