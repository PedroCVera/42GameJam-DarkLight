using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections.Generic;

    // [RequireComponent(typeof (PlatformerCharacter2D))]
	
public class Platformer2DUserControl : MonoBehaviour
{
    private PlatformerCharacter2D m_Character;
	public	PlayerDamage	playerAttack; // reference to player attack class
    private bool m_Jump;
	private	bool m_attack;


    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
		if (!m_attack)
            m_attack = Input.GetKeyDown(KeyCode.Mouse0);
		if (m_attack)
		{
			// m_Character.attack();
			playerAttack.ATTACK();
		}
	}


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
		m_attack = false;
    }
}