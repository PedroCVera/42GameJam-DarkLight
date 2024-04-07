using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMovement : MonoBehaviour
{

	public	Transform playerTransform; //Keeps track of player's position through the game
	public	bool	isChasing;

	public SpriteRenderer backgroundSwan;
	public SpriteRenderer actualSwan;

	public	float	moveSpeed;
	public	float	chaseDistance;
	public Rigidbody2D e_Rigidbody2D;

	public float glideRate = 0.1f;
	private	float	maxVerticalFallSpeed = 1f;

	// e_Rigidbody2D.gravityScale = 0.5f; // Adjust gravity scale as needed
	// e_Rigidbody2D.drag = 0.5f; // Adjust drag as needed



	void FixedUpdate()
	{
		e_Rigidbody2D.gravityScale = 0; // Adjust gravity scale as needed
		// e_Rigidbody2D.drag = 0.5f; // Adjust drag as needed

		// Check if the enemy is falling too fast
		if (Mathf.Abs(e_Rigidbody2D.velocity.y) > maxVerticalFallSpeed)
		{
			// Calculate the new vertical velocity with reduced speed
			float newYVelocity = Mathf.MoveTowards(e_Rigidbody2D.velocity.y, maxVerticalFallSpeed, glideRate * Time.deltaTime);
			
			// Apply the new vertical velocity
			e_Rigidbody2D.velocity = new Vector2(e_Rigidbody2D.velocity.x, newYVelocity);
		}

	}
	void	Update()
	{

		if (isChasing)
		{
			if (transform.position.x > playerTransform.position.x)
			{
				transform.localScale = new Vector3(0.12f,0.12f,1); // if monster is on the right of the player, make his scale positive so that he faces right
				// transform.position += Vector3.left * moveSpeed * Time.deltaTime;
				Vector2 direction = (playerTransform.position - transform.position).normalized;
				e_Rigidbody2D.velocity = direction * moveSpeed;
			}
			if (transform.position.x < playerTransform.position.x)
			{
				transform.localScale = new Vector3(-0.12f,0.12f,1); // if monster is on the left of the player, make his scale positive so that he faces left
				// transform.position += Vector3.right * moveSpeed * Time.deltaTime;
				Vector2 direction = (playerTransform.position - transform.position).normalized;
				e_Rigidbody2D.velocity = direction * moveSpeed;
			}
		}
		else
		{
			if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance) //Vector2.Distance calculates the distance between two objects
			{
				backgroundSwan.enabled = false;
				actualSwan.enabled = true;
				isChasing = true;
				if (!e_Rigidbody2D)
					Debug.Log("No e_Rigidbody2D");
				e_Rigidbody2D.AddForce(new Vector2(0f, 100));
			}
		}
	}

}
