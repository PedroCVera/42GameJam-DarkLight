using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
	public int damage;
	public PlayerHealth playerHealth;
	public	PlayerDamage playerDamage;
	public EnemiesHealth	enemyHealth;
	public	PlatformerCharacter2D playerMovement; // reference to player movement script

    // Start is called before the first frame update
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.name == "PlayerCharacter")
		{
			Debug.Log( "other collider: " + collision.otherCollider + "Enemy: " + collision.collider.gameObject.name + "Enemy Pos: " + collision.transform.position.x);

			Debug.Log("Enemy: Hit Player");
			playerMovement.KBCounter = playerMovement.KBTotalTime;
			if (collision.transform.position.x <= transform.position.x)
				playerMovement.KBFromRight = true;
			else
				playerMovement.KBFromRight = false;
			playerHealth.TakeDamage(damage);
		}
		else if (collision.collider.gameObject.name == "Sword")
			enemyHealth.TakeDamage(playerDamage.damage);
	}
}
