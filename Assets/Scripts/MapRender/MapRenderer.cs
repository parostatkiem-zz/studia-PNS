using UnityEngine;
using System.Collections.Generic;



public class MapRenderer :MonoBehaviour
{
    private float offset = 0.1f;
    private float square_size = 0.35f; //TODO
    private Assets.Scripts.Map.Map map;
    private Transform prefab_grass; //TODO: change it to list or sth

    public MapRenderer(Assets.Scripts.Map.Map map, Transform prefab_grass){
        this.map = map;
        this.prefab_grass = prefab_grass;
       
    }

    public void RenderTheMap()
    {
        for (var x = 0; x < map.width; x++)
        {
            for (var y = 0; y < map.height; y++)
            {
                Instantiate(prefab_grass, new Vector3(offset+map.width*x*square_size, 0.0f, offset+map.height*y*square_size), Quaternion.identity);

            }
        }
    }
   
}
