using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Global : MonoBehaviour
{

    // Main Map object with all informations about current map states
     Assets.Scripts.Map.Map gameMap;
    public Transform prefab_grass,prefab_water,prefab_sand;
   

    public Assets.Scripts.Map.Map GameMap{
        get{
            return this.gameMap;
        }
    }

    // Use this for initialization
    void Start () {
       var filter= gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        this.gameMap = Assets.Scripts.Map.MapLoader.LoadMapFromJson(Assets.Scripts.Map.GlobalMapConfig.JsonMapPath);

        //  Instantiate(prefab_grass, new Vector3(0, 0.5f, 0), Quaternion.identity);
        var mapRenderer = new MapRenderer(GameMap, prefab_grass, prefab_water, prefab_sand);
        mapRenderer.RenderTheMap();
        // filter.mesh = MapRenderer.RenderTheMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
