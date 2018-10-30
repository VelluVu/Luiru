using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CameraDraw cameraDraw;

    public bool grounded;
    public bool headHit;
    public bool firstHeadHit;
    public bool frontHit;
    public bool backHit;
    public bool slopeHit;
    public bool instantiate;
    public bool digging;

    public float nextShot;

    public float defaultGravity;
    public float jumpForce;
    public float moveSpeed;
    public float reducedSpeed;
    public float diggingSpeed;

    public Rigidbody2D rb2d;
    public GameObject testRayGround;
    public GameObject testRayTop;
    public GameObject testRayFront;
    public GameObject testRayBack;
    public GameObject testRaySlope;
    public GameObject ammo;
    public GameObject ammoSpawn;
    public GameObject hook;
    public GameObject hookInstance;
	
	void Update () {

        PlayerMove();
        GroundCheck();
        HeadHitCheck();
        FrontCheck();
        BackCheck();
        SlopeCheck();
        Jump();
        Shoot();

	}

    public void Shoot ()
    {
        if (Input.GetButton("Fire1") && !instantiate && Time.time > nextShot)
        {
            nextShot = Time.time + 1;
            GameObject ammoInstance = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            ammoInstance.GetComponent<Rigidbody2D>().AddForce(transform.localScale.x * ammoSpawn.transform.right * 10, ForceMode2D.Impulse);
            instantiate = true;
        }
        else
        {
            instantiate = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if(!GetComponent<SpringJoint2D>())
            {
                hookInstance = Instantiate(hook, ammoSpawn.transform.position, Quaternion.identity);
                hookInstance.GetComponent<Rigidbody2D>().AddForce(transform.localScale.x * ammoSpawn.transform.right * 10, ForceMode2D.Impulse);
            }
            else
            {
                Destroy(hookInstance);
                Destroy(GetComponent<SpringJoint2D>());
            }
        }
    }

    public void SlopeCheck()
    {
        slopeHit = cameraDraw.DrawRay(testRaySlope.transform.position);
    }
    

    public void FrontCheck()
    {
        frontHit = cameraDraw.DrawRay(testRayFront.transform.position);

          
    }

    public void BackCheck()
    {
        backHit = cameraDraw.DrawRay(testRayBack.transform.position);

       
    }

    public void GroundCheck()
    {
        grounded = cameraDraw.DrawRay(testRayGround.transform.position);

        if (grounded)
        {
            rb2d.gravityScale = 0;
            rb2d.velocity = new Vector2(0, 0);
        }
        else
        {
            rb2d.gravityScale = defaultGravity;
        }
    }

    public void HeadHitCheck()
    {
        headHit = cameraDraw.DrawRay(testRayTop.transform.position);

        if (headHit && !firstHeadHit)
        {
            firstHeadHit = true;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
        if (firstHeadHit && !headHit)
        {
            firstHeadHit = false;
        }
    }

    public void PlayerMove()
    {
        if (grounded)
        {          

            if(Input.GetAxisRaw("Horizontal") != 0)

            {
                if (!frontHit)
                {

                    if (slopeHit)
                    {
                        transform.Translate(Input.GetAxis("Horizontal") * reducedSpeed * Time.deltaTime, reducedSpeed * Time.deltaTime, 0);
                    }

                    else

                    {
                        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
                    }

                    

                } else
                {

                    transform.Translate(Input.GetAxis("Horizontal") * diggingSpeed * Time.deltaTime, Input.GetAxis("Vertical") * diggingSpeed * Time.deltaTime, 0);
                    digging = cameraDraw.Digging(transform.position, 15);

                              
                }
                
           
                transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);

            }
        }
    }
   
    public void Jump()
    {
        if (grounded && !digging)
        {
            if(Input.GetButtonDown("Jump"))
            {
                rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * jumpForce * Time.deltaTime, jumpForce);              
            }
        }
    }
}
