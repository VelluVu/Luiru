using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDraw : MonoBehaviour {

    public Camera cam;
	
	void Start () {

        cam = GetComponent<Camera>();

	}
	
	void Update () {
		
        //Update jatkuu vain jos hiiren nappi on alhaalla...

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        //raycast hit saadaan dataa

        RaycastHit hit;

        // jos hiiren kohtaan lähtenyt säde ei osu niin ei jatketa updatea...

        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit)) {

            return;

        }
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Hiiri on kappaleen päällä ja säde osuu johonkin...
        // haetaan raycastin osumasta tietoa...

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        //null tsekataan osumat jo ei niin lopetetaan looppi

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null) 
        {
            return;
        }

        //kaikki ok.. päästään värittämään

        Texture2D tex = rend.material.mainTexture as Texture2D;

        //textuurin koordinaatti sen uvkartasta

        Vector2 pixelUV = hit.textureCoord;

        //kerrotaan jotta saadaan kuvan pixelikoordinaatti

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        //Väritettään pixeli ja asetetaan väritetty

        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.clear);
        tex.Apply();

    }

    public bool DrawRay(Vector3 testRay)
    {

        RaycastHit hit;

        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(testRay)), out hit))
        {

            return false;

        }
     
        //Testikappale on taustan päällä ja säde osuu johonkin kohtaan

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
     
        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }
 
        Texture2D tex = rend.material.mainTexture as Texture2D;
   
        Vector2 pixelUV = hit.textureCoord;
    
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {

            return true;

        }
        else
        {
            return false;
        }
    }

    public bool PaintRay(Vector3 testRay)
    {

        RaycastHit hit;

        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(testRay)), out hit))
        {

            return false;

        }

        //Testikappale on taustan päällä ja säde osuu johonkin kohtaan

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }

        Texture2D tex = rend.material.mainTexture as Texture2D;

        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            //ammu Osuu seinään
            //huom radius = kovakoodattu jos teet useampia aseita muokkaa radius parametrien mukaan
            Circle(tex, (int)pixelUV.x, (int)pixelUV.y, 10, Color.clear);
            tex.Apply();
            return true;

        }
        else
        {
            return false;
        }
    }

    public void Circle(Texture2D tex, int cx, int cy, int r, Color col)
    {

        int x, y, px, py, nx, ny, d;

        for (x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));

            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                tex.SetPixel(px, py, col);
                tex.SetPixel(nx, py, col);

                tex.SetPixel(px, ny, col);
                tex.SetPixel(nx, ny, col);
            }
        }
        

    }

    public bool Digging (Vector3 location, int radius)
    {

        RaycastHit hit;

        if (!Physics.Raycast(cam.ScreenPointToRay(Camera.main.WorldToScreenPoint(location)), out hit))
        {

            return false;

        }

        //Testikappale on taustan päällä ja säde osuu johonkin kohtaan

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            return false;
        }

        Texture2D tex = rend.material.mainTexture as Texture2D;

        Vector2 pixelUV = hit.textureCoord;

        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if (tex.GetPixel((int)pixelUV.x, (int)pixelUV.y).a == 1.0)
        {
            //ammu Osuu seinään
            //huom radius = kovakoodattu jos teet useampia aseita muokkaa radius parametrien mukaan
            Circle(tex, (int)pixelUV.x, (int)pixelUV.y, radius, Color.clear);
            tex.Apply();
            return true;

        }
        else
        {
            return false;
        }
    }

}
