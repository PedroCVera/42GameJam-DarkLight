using System;
using UnityEngine;
using System.Collections;

// namespace UnityStandardAssets._2D
// {
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private int   m_MaxJumps = 2;                     // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

		public	float	AttackDelay = 1000000f;
		public	bool	attackBlocked;
	


		public	int	Hp;

		public	float	KBForce; // KnockBack force
		public	float	KBCounter; // time left on the effect
		public	float	KBTotalTime; // How long the effect will last all together
		public	bool	KBFromRight; // Direction of knockback

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private int jumps_left;

		public	float	maxVerticalSpeed;
		// public	CircleCollider2D circleCollider2D;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            jumps_left = m_MaxJumps;
        }



        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to trhe groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
			int groundLayerMask = LayerMask.GetMask("Default");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {

                if (colliders[i].gameObject != gameObject)
				{
					jumps_left = m_MaxJumps - 1;
                    m_Grounded = true;
					m_Anim.SetBool("Ground", true);
				}
            }
			if (Mathf.Abs(m_Rigidbody2D.velocity.y) > maxVerticalSpeed){
				float newYVelocity = Mathf.Clamp(m_Rigidbody2D.velocity.y, -maxVerticalSpeed, maxVerticalSpeed);
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, newYVelocity);
			}

			
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);


        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character\
				if (KBCounter <= 0)
                	m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
				else
				{
					if (KBFromRight)
						m_Rigidbody2D.velocity = new Vector2(-KBForce, m_Rigidbody2D.velocity.y);
					if (!KBFromRight)
						m_Rigidbody2D.velocity = new Vector2(KBForce, m_Rigidbody2D.velocity.y);
					KBCounter -= Time.deltaTime;
				}

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // Reset jump count if the player is on the ground
            if (jump && (m_Anim.GetBool("Ground") || jumps_left > 0))
            {   
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				jumps_left--;
            }
        }


		public void	attack()
		{
			if (attackBlocked)
				return ;

			m_Anim.SetBool("Attack", true);
			attackBlocked = true;
			StartCoroutine(DelayAttack());
		}

		private	IEnumerator DelayAttack()
		{
			yield return new WaitForSeconds(1);
			m_Anim.SetBool("Attack", false);
			yield return new WaitForSeconds(1);
			attackBlocked = false;
		}
        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
// }
