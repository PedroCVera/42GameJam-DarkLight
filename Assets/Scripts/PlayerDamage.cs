using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // Start is called before the first frame update
	public int damage = 2;
	public EnemiesHealth enemiesHealth;
	// public	Transform playerTransform;
	public PlatformerCharacter2D platformPlayer;
	public Rigidbody2D PlayerBody;
	public	GameObject	Sword;
	public	MonsterMovement enemyMovement; // reference to enemy movement script

    // Start is called before the first frame update


	public	void	ATTACK()
	{
		if (platformPlayer.attackBlocked == true)
			return ;
		platformPlayer.attack();
		Sword.GetComponent<BoxCollider2D>().enabled = true;
		StartCoroutine(DelayAttack());

	}

	private	IEnumerator DelayAttack()
	{
		yield return new WaitForSeconds(0.1f);
		Sword.GetComponent<BoxCollider2D>().enabled = false;
	}

	// public void OnCollisionEnter2D(Collision2D collision){
	// 	if (collision.gameObject.tag == "Enemy")
	// 	{
	// 		enemiesHealth.TakeDamage(damage);
	// 	}
	// }
}
