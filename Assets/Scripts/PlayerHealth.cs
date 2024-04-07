using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public	int	maxHealth = 5; // max hp, set to public so that it can be changed at any point in the inspect tab of the engine

	public int health; //current hp


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

	public	void	TakeDamage(int dmg)
	{
		health -= dmg;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}

}
