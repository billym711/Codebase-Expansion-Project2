using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private float m_lowHealthShakeIntensity = 5.0f;

	[SerializeField] private Transform m_healthBarParent;
	[SerializeField] private Image m_healthFill;

	[HideInInspector] public int maxHealth;

	private IEnumerator m_shake;

	public void UpdateDisplay(int health)
	{
		m_healthFill.fillAmount = (float)health / (float)maxHealth;

		if (health < 1)
			ShakeUI(m_lowHealthShakeIntensity);
	}

	private void ShakeUI(float intensity)
	{
		if (m_shake != null)
			StopCoroutine(m_shake);

		m_shake = ShakeRoutine(intensity);
		StartCoroutine(m_shake);
	}
	private IEnumerator ShakeRoutine(float intensity)
	{
		Vector3 originalPos = m_healthBarParent.localPosition;

		while (m_healthFill.fillAmount < 0.3f)
		{
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;

			m_healthBarParent.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

			yield return null;
		}

		m_healthBarParent.localPosition = originalPos;

		m_shake = null;
	}
}
