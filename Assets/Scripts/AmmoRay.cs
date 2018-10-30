using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour {

    private bool hitToWall;
    public Camera cam;
    public int ammoRad;

	void Start () {

        cam = Camera.main;

	}
	
	
	void Update () {

        hitToWall = cam.GetComponent<CameraDraw>().PaintRay(transform.position, ammoRad);

        if (hitToWall)
        {
            Destroy(gameObject);
        }

	}
}
