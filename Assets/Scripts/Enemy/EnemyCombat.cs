using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyCombat : MonoBehaviour
{
	[SerializeField] private int m_maxHealth = 2;
	[SerializeField] private float m_bulletSpeed = 200.0f;
	[SerializeField] private float m_attackCooldown = 2.5f;
	[SerializeField] private float m_attackCooldownRandomOffset = 0.75f; 
	[SerializeField] private float m_raycastDistance = 15.0f;
	[SerializeField] private LayerMask m_lineOfSightMask;

	[SerializeField] private Transform m_player;
	[SerializeField] private GameObject m_bulletPrefab;
	[SerializeField] private DamageFlash m_damageFlash;

	private int m_currentHealth;
	private bool m_alive;
	private bool m_coolingDown;
	private float m_currentCooldown;	//Includes random variation
	private IEnumerator m_attacking;

	private AIPath pathfinding;

	private void Awake()
	{
		m_currentHealth = m_maxHealth;
		m_currentCooldown = m_attackCooldown;

		pathfinding = GetComponent<AIPath>();
		pathfinding.canMove = false;

		if (!m_player)
			Debug.LogWarning("Enemy " + gameObject.name + " missing player reference");
	}

	private IEnumerator AttackLoop()
	{
		while (m_currentHealth > 0)
		{
			if (!m_coolingDown)
			{
				Vector2 directionToPlayer = (m_player.position - transform.position).normalized;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, m_raycastDistance, m_lineOfSightMask);
				if (hit)
				{
					if (hit.transform.CompareTag("Player"))
						Attack(directionToPlayer);
				}
			}
			yield return new WaitForSeconds(m_currentCooldown);
		}
	}

	private void Attack(Vector2 direction)
	{
		GameObject bullet = NewBullet();
		bullet.transform.position = transform.position;
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(m_bulletSpeed * direction);
		StartCoroutine(AttackCooldown());
	}

	private GameObject NewBullet()
	{
		return Instantiate(m_bulletPrefab);
	}

	private IEnumerator AttackCooldown()
	{
		m_coolingDown = true;
		m_currentCooldown = m_attackCooldown + Random.Range(-m_attackCooldownRandomOffset, m_attackCooldownRandomOffset);
		yield return new WaitForSeconds(m_currentCooldown);
		m_coolingDown = false;
	}

	public void Damage(int damageAmount)
	{
		m_currentHealth -= damageAmount;
		if (m_currentHealth < 1)
			SetAlive(false);
		else
			m_damageFlash.Flash();
	}

	public void SetAlive(bool alive)
	{
		m_alive = alive;

		if (alive)
		{
			StartCoroutine(AttackCooldown());

			m_attacking = AttackLoop();
			StartCoroutine(m_attacking);

			pathfinding.canMove = true;
		}
		else
		{
			StopCoroutine(m_attacking);
			gameObject.SetActive(false);
			//Destroy(gameObject);
			m_alive = false;
		}
	}
	public bool GetAlive()
	{
		return m_alive;
	}
}
