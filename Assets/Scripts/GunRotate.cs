using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour {

    private float angle;
    public GameObject player;

	void Update () {

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;

        if (player.transform.localScale.x == -1)
        {
            //Atan2 käyttää vektor komponentteja
            angle = Mathf.Atan2(dir.y * -1, dir.x * -1) * Mathf.Rad2Deg;

        }
        else
        {

            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        }
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
