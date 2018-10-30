using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour {

    private bool hitToWall;
    public Camera cam;

	void Start () {

        cam = Camera.main;

	}
	
	
	void Update () {

        hitToWall = cam.GetComponent<CameraDraw>().PaintRay(transform.position);

        if (hitToWall)
        {
            Destroy(gameObject);
        }

	}
}
