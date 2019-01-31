using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class militaryBehavior : MonoBehaviour {
    
    // Use this for initialization
    private Color playerColor;
    public Vector2 mapPosition;
    public Vector3 realPosition;
    public Assets.Scripts.Map.MapObject listInstanceRef;
    private Global globalScript;
    private ObjectInfoUI objectInfoUI;


    void Start () {
        globalScript = (Global)GameObject.Find("GLOBAL").GetComponent(typeof(Global));
        objectInfoUI = (ObjectInfoUI)GameObject.Find("ObjectInfo").GetComponent(typeof(ObjectInfoUI));
        playerColor = GetComponent<Renderer>().material.GetColor("_Color");
        listInstanceRef = globalScript.listOfMapObjects.FindLast(obj => obj.x == mapPosition.x && obj.y == mapPosition.y);
        if (listInstanceRef != null)
        {
            listInstanceRef.instance = gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position != realPosition )
        {
            var zeroSpeed = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, realPosition, Time.deltaTime * 3);
        }
    }


    private void OnMouseDown()
    {
        globalScript.HandleFigureHighlight(listInstanceRef);
    }

    private void OnMouseEnter()
    {
        this.objectInfoUI.UpdateFromMapObject(listInstanceRef);
        this.objectInfoUI.ShowInfoUI();
    }

    private void OnMouseExit()
    {
        this.objectInfoUI.HideInfoUI();
    }


}
