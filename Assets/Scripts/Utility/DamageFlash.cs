using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
	[SerializeField] private int m_numberOfFlashes = 1;
	[SerializeField] private float m_flashDuration = 0.1f;

	[SerializeField] private Material m_flashMat;

	private SpriteRenderer m_rend;
	private Material m_originalMat;
	private IEnumerator m_flashRoutine;

	private void Awake()
	{
		m_rend = GetComponent<SpriteRenderer>();
		m_originalMat = m_rend.material;
	}

	public void Flash()
	{
		if (m_flashRoutine != null)
			StopCoroutine(m_flashRoutine);

		m_flashRoutine = FlashRoutine();
		StartCoroutine(m_flashRoutine);
	}
	private IEnumerator FlashRoutine()
	{
		for (int i = 0; i < m_numberOfFlashes; ++i)
		{
			MaterialSetter.Set(m_rend, m_flashMat);
			yield return new WaitForSeconds(m_flashDuration);
			MaterialSetter.Set(m_rend, m_originalMat);
			yield return new WaitForSeconds(m_flashDuration);
		}
		m_flashRoutine = null;
	}
}
