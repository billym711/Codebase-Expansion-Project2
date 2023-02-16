using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float m_moveSpeed = 1.0f;
	[SerializeField] private Animator m_animator;
	[SerializeField] private SpriteRenderer m_rend;

	private Vector2 m_input;

	private Rigidbody2D m_rb;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		//Store inputs for fixed update
		//Using raw for instantaneous movement rather than smooth
		m_input.x = Input.GetAxisRaw("Horizontal1");
		m_input.y = Input.GetAxisRaw("Vertical1");
	}
	private void FixedUpdate()
	{
		m_input.Normalize();
		if (this.name == "Player2_other" && Input.GetKey("space"))
		{

		}
		else
		{
			m_rb.MovePosition(m_rb.position + (m_input * m_moveSpeed * Time.fixedDeltaTime));
			m_animator.SetFloat("Horizontal", m_input.x);
			m_animator.SetFloat("Vertical", m_input.y);
			m_animator.SetFloat("Speed", m_input.sqrMagnitude);
			m_rend.flipX = m_input.x < 0;
		}

	}
}
