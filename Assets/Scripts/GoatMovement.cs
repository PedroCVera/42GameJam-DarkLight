using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GoatMovement : MonoBehaviour
{
    // Start is called before the first frame update
   public	Transform playerTransform; //Keeps track of player's position through the game
	public	bool	isChasing;

	public	float	moveSpeed;
	public SpriteRenderer backgroundGoat;
	public SpriteRenderer actualGoat;
	
	public	float	rushSpeed;
	public	float	chaseDistance;
	public Rigidbody2D e_Rigidbody2D;
	private	bool	rushBlocked; //rush cooldown
	private	float	Speed; // current Speed (to swap between move speed and rush speed)
	private bool	dodgeRush; // see if char switched sides, if so, cancel rush



	private void FixedUpdate()
	{
		if (rushBlocked == false)
			Speed = rushSpeed;
		else
			Speed = moveSpeed;
	}



	

	void	Update()
	{

		if (isChasing)
		{
			if (transform.position.x > playerTransform.position.x)
			{
				transform.localScale = new Vector3(-0.1658545f,0.1376653f,1); // if monster is on the left of the player, make his scale positive so that he faces left
				transform.position += Vector3.left * Speed * Time.deltaTime;
				
			}
			if (transform.position.x < playerTransform.position.x)
			{
				transform.localScale = new Vector3(0.1658545f,0.1376653f,1); // if monster is on the right of the player, make his scale positive so that he faces right
				transform.position += Vector3.right * Speed * Time.deltaTime;
				
			}
			if (playerTransform.position.y > -4.117823f)
				isChasing = false;
		}
		else
		{
			if (playerTransform.position.y < -4.117823f)
			{
				backgroundGoat.enabled = false;
				actualGoat.enabled = true;

				isChasing = true;
				if (!e_Rigidbody2D)
					Debug.Log("No e_Rigidbody2D");
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.gameObject.name == "PlayerCharacter" || collision.collider.gameObject.name == "Sword")
		{
			StartCoroutine(DelayRush());
		}
	}
	private	IEnumerator DelayRush()
	{
			rushBlocked = true;
		yield return new WaitForSeconds(15);
		rushBlocked = false;
	}

}
