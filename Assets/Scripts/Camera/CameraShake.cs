using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private Camera m_camera;

	private IEnumerator m_shake;

	private static CameraShake m_instance;

	private void Awake()
	{
		if (m_instance == null)
			m_instance = this;
	}

	public static void Shake(float intensity, float duration)
	{
		if (m_instance.m_shake != null)
			m_instance.StopCoroutine(m_instance.m_shake);

		m_instance.m_shake = m_instance.ShakeRoutine(intensity, duration);
		m_instance.StartCoroutine(m_instance.m_shake);
	}
	private IEnumerator ShakeRoutine(float intensity, float duration)
	{
		Vector3 originalPos = m_camera.transform.localPosition;

		float t = 0.0f;

		while (t < duration)
		{
			float x = Random.Range(-1f, 1f) * intensity;
			float y = Random.Range(-1f, 1f) * intensity;
			float z = Random.Range(-1f, 1f) * intensity;

			//transform.localPosition = new Vector3 (x, y, z);
			m_camera.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

			t += Time.deltaTime;

			yield return null;
		}

		m_camera.transform.localPosition = originalPos;

		m_shake = null;
	}

	public static void DirectionShake(float intensity, float duration, Vector2 direction)
	{
		if (m_instance.m_shake != null)
			m_instance.StopCoroutine(m_instance.m_shake);

		m_instance.m_shake = m_instance.DirectionShakeRoutine(intensity, duration, direction);
		m_instance.StartCoroutine(m_instance.m_shake);
	}
	private IEnumerator DirectionShakeRoutine(float intensity, float duration, Vector2 direction)
	{
		Vector3 originalPos = m_camera.transform.localPosition;

		direction.Normalize();

		float t = 0.0f;

		while (t < duration)
		{
			float x = Random.Range(direction.x - 1.0f, direction.x + 1.0f) * intensity;
			float y = Random.Range(direction.y - 1.0f, direction.y + 1.0f) * intensity;
			float z = Random.Range(-1.0f, 1.0f) * intensity * 0.5f;

			//transform.localPosition = new Vector3 (x, y, z);
			m_camera.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z + z);

			t += Time.deltaTime;

			yield return null;
		}

		m_camera.transform.localPosition = originalPos;

		m_shake = null;
	}
}
