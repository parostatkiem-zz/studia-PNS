using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    // Main Map object with all informations about current map states
    Assets.Scripts.Map.Map GameMap;


    // Use this for initialization
    void Start () {
        this.GameMap = Assets.Scripts.Map.MapLoader.LoadMapFromJson(Assets.Scripts.Map.GlobalMapConfig.JsonMapPath);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
