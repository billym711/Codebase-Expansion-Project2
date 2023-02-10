using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int damage = 1;
	public int heal = 0;

	public delegate void HealOnHitHandler(int healAmount);
	public event HealOnHitHandler HealOnHit;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Wall":

				Destroy(gameObject);

				break;

			case "Enemy":

				EnemyCombat enemy = collision.transform.GetComponent<EnemyCombat>();
				enemy.Damage(damage);
				HealOnHit?.Invoke(heal);
				Destroy(gameObject);

				break;

			case "Player":

				PlayerCombat player = collision.transform.GetComponent<PlayerCombat>();
				player.Damage(damage);
				Destroy(gameObject);

				break;

			default:
				Debug.LogWarning("Bullet: Unexpected collision tag");
				break;
		}
	}
}
