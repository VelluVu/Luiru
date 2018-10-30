using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    public bool hit;
    public GameObject player;
    public Rigidbody2D rb2d;
    public CameraDraw camDraw;
    public SpringJoint2D hookConnection;
    private LineRenderer hookRobe;
    Vector3[] robePos = new Vector3[2];


    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        camDraw = Camera.main.GetComponent<CameraDraw>();
        hookRobe = gameObject.GetComponent<LineRenderer>();

	}
	
	void Update () {

        robePos[0] = player.transform.position;
        robePos[1] = transform.position;
        hookRobe.SetPositions(robePos);

        hit = camDraw.DrawRay(transform.position);

        //Grappling hook on osunut näkyvään pixeliin

        if (hit)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0;

            if(!player.GetComponent<SpringJoint2D>())
            {

                hookConnection = player.AddComponent<SpringJoint2D>();
                hookConnection.connectedAnchor = transform.position;
                hookConnection.autoConfigureDistance = false;
                hookConnection.dampingRatio = 1;
                hookConnection.frequency = 0.8f;

            }
            hookConnection.distance -= Input.GetAxis("Vertical") * 0.5f;
        }
        else
        {
            if (rb2d.gravityScale == 0)
            {
                
                Destroy(gameObject);
            }
            Destroy(player.GetComponent<SpringJoint2D>());


        }

	}
}
