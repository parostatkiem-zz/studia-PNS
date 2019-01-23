using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class militaryBehavior : MonoBehaviour {

    // Use this for initialization
    private Color playerColor;
    public Vector2 mapPosition;
	void Start () {
        playerColor = GetComponent<Renderer>().material.GetColor("_Color");
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnMouseDown()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Bumped Diffuse");
        Global other = (Global)GameObject.Find("GLOBAL").GetComponent(typeof(Global));
        other.HandleFigureHighlight(mapPosition);
      //  Debug.Log(a);
    }
    void OnMouseOver()
        {

        //GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Bumped Diffuse");
     //   GetComponent<Renderer>().material.SetColor("_Color", Color.green);

    }
    void OnMouseExit()
            {
           // GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
//GetComponent<Renderer>().material.SetColor("_Color",playerColor);
        }
}
