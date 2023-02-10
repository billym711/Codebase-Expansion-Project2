using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
	[SerializeField] private int m_maxHealth = 3;
	[SerializeField] private float m_bulletSpeed = 1.0f;
	[SerializeField] private float m_retaliateCooldown = 1.0f;
	[SerializeField] private float m_retaliateShakeIntensity = 0.2f;
	[SerializeField] private float m_retaliateShakeDuration = 0.1f;
	[SerializeField] private float m_healthShakeIntensity = 0.1f;
	[SerializeField] private float m_healthShakeDuration = 0.15f;

	[SerializeField] private GameObject m_bulletPrefab;
	[SerializeField] private TextMeshProUGUI m_damageText;
	[SerializeField] private HealthBar m_healthBar;
	[SerializeField] private DamageFlash m_damageFlash;

	private int m_currentHealth;
	private int m_storedDamage;
	private bool m_coolingDown;
	private Vector2 m_input;

	private void Awake()
	{
		m_currentHealth = m_maxHealth;
		m_healthBar.maxHealth = m_maxHealth;
	}

	private void Update()
	{
		m_input.x = Input.GetAxisRaw("Horizontal2");
		m_input.y = Input.GetAxisRaw("Vertical2");

		if (m_input != Vector2.zero)
		{
			if (!m_coolingDown && m_storedDamage > 0)
				Retaliate();
		}
	}

	private void Retaliate()
	{
		GameObject bulletObject = Instantiate(m_bulletPrefab);

		Bullet bullet = bulletObject.GetComponent<Bullet>();

		bullet.damage = m_storedDamage;
		bullet.heal = m_storedDamage;
		bullet.HealOnHit += AddHealth;
		SetDamage(0);

		CameraShake.DirectionShake(m_retaliateShakeIntensity * bullet.damage, m_retaliateShakeDuration, -m_input.normalized);

		bulletObject.transform.position = transform.position;
		Rigidbody2D rb = bulletObject.GetComponent<Rigidbody2D>();
		rb.AddForce(m_bulletSpeed * m_input.normalized);

		StartCoroutine(RetaliateCooldown());
	}

	private IEnumerator RetaliateCooldown()
	{
		m_coolingDown = true;
		yield return new WaitForSeconds(m_retaliateCooldown);
		m_coolingDown = false;
	}

	public void Damage(int damageAmount)
	{
		AddHealth(-damageAmount);
		SetDamage(m_storedDamage + damageAmount);

		m_damageFlash.Flash();
		CameraShake.Shake(m_healthShakeIntensity * damageAmount, m_healthShakeDuration);
	}

	private void AddHealth(int amount)
	{
		SetHealth(m_currentHealth + amount);
	}
	private void SetHealth(int value)
	{
		m_currentHealth = value;

		if (m_currentHealth < 0)	//Time to die :)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //Reload the current scene

		Mathf.Clamp(m_currentHealth, 0, m_maxHealth);   //Used mathf, couldn't find int version

		m_healthBar.UpdateDisplay(m_currentHealth);
	}
	private void SetDamage(int value)
	{
		m_storedDamage = value;
		UpdateDamageText();
	}

	private void UpdateDamageText()
	{
		if (m_storedDamage < 1)
			m_damageText.text = "DAMAGE: --";
		else
			m_damageText.text = "DAMAGE: " + m_storedDamage + "X";
	}
}
