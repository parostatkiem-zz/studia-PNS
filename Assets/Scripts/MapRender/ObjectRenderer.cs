using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    private Assets.Scripts.Map.Map map;
    private List<Assets.Scripts.Map.MapObject> listOfMapObjects;

    private List<Transform> prefabs;

    private float separator;
    private float square_xz;

    private Vector2 mapStart;

    private float scale;

    public ObjectRenderer() { }

    public ObjectRenderer(Assets.Scripts.Map.Map map, List<Assets.Scripts.Map.MapObject>  listOfMapObjects, float scale, float square_xz, float separator)
    {
        setParameters(map, listOfMapObjects, scale, square_xz, separator, new Vector2(0,0));
    }

    public void setPrefabs(Transform prefab_archer, Transform prefab_swordsman, Transform prefab_mutant, Transform prefab_horseman, Transform prefab_castle)
    {
        this.prefabs = new List<Transform>{ prefab_castle, prefab_swordsman,  prefab_archer, prefab_horseman };
    }

    public void setParameters(Assets.Scripts.Map.Map map, List<Assets.Scripts.Map.MapObject> listOfMapObjects, float scale, float square_xz, float separator, Vector2 mapStart)
    {
        this.map = map;
        this.listOfMapObjects = listOfMapObjects;
        this.separator = separator;
        this.square_xz = square_xz;
        this.scale = scale;
        this.mapStart = mapStart;
    }

    public void RenderObjectAtPos(float xPos, float yPos, float zPos, int objTypeID, int ownerID,Vector2 mapPosition)
    {
        var elementInstance = Instantiate(
                   prefabs[objTypeID-2],
                   new Vector3(
                       x: xPos,
                       y: 0.35f,
                       z: zPos),
                   prefabs[objTypeID - 2].rotation);

      
        Color highlightColor=Color.yellow;
        if(ownerID==0)
        {
            highlightColor = Color.red;
        }

        if (ownerID == 1)
        {
            highlightColor = Color.cyan;
        }
        var coloredMaterial = elementInstance.GetComponent<Renderer>().materials.FirstOrDefault(m => m.name == "Podstawka (Instance)");
        if (coloredMaterial != null) { coloredMaterial.SetColor("_Color", highlightColor); }
        elementInstance.GetComponent<militaryBehavior>().mapPosition = mapPosition;
        elementInstance.GetComponent<militaryBehavior>().realPosition = elementInstance.transform.position;
        var children = elementInstance.GetComponentsInChildren<Transform>();
        foreach(var child in children)
        {
            if(child.GetComponent<Light>())
            {
                var light = child.GetComponent<Light>();
                light.color = highlightColor;
                light.enabled = false;
                light.intensity = 6;
                light.range = 2f;
                light.spotAngle = 40;
            }
        }
       
    }

    public void UpdateObjects()
    {
        militaryBehavior instanceBehavior;
        Light light;
        Transform[] children;
        foreach (var listObj in listOfMapObjects)
        {
            if (!listObj.instance) { continue; }

            children = listObj.instance.GetComponentsInChildren<Transform>(true);
           
            foreach (var child in children)
            {
                if (child.GetComponent<Light>())
                {
                    light = child.GetComponent<Light>();
                    light.enabled = listObj.isHighlighted;
                }
            }

            if (listObj.isHighlighted)
            {
                listObj.instance.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Bumped Diffuse");
            }
            else
            {
                listObj.instance.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
            }

            instanceBehavior = listObj.instance.GetComponent<militaryBehavior>();

            if (instanceBehavior.mapPosition.x!=listObj.x || instanceBehavior.mapPosition.y != listObj.y)
            {
                // need to move figure
                instanceBehavior.mapPosition = new Vector2(listObj.x, listObj.y);
                MoveFigureOnMap(listObj.instance, instanceBehavior.mapPosition);
            }
        }
    }

    private void MoveFigureOnMap(GameObject figure, Vector2 position)
    {
        var newPos = new Vector3(
                mapStart.x + (square_xz + separator) * scale * position.x,
                figure.transform.position.y,
                 mapStart.y + (square_xz + separator) * scale * position.y
            );

        var instanceBehavior = figure.GetComponent<militaryBehavior>();
        instanceBehavior.realPosition = newPos;
    }
}

