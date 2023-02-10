using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Animator m_animator;
	[SerializeField] private SpriteRenderer m_rend;

	private AIPath m_pathfinding;

	private void Awake()
	{
		m_pathfinding = GetComponent<AIPath>();
	}

	private void Update()
	{
		Vector2 direction = m_pathfinding.desiredVelocity.normalized;
		m_animator.SetFloat("Horizontal", direction.x);
		m_animator.SetFloat("Vertical", direction.y);
		m_animator.SetFloat("Speed", direction.sqrMagnitude);
		m_rend.flipX = direction.x < 0;
	}
}
