using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Map;

public class ObjectInfoUI : MonoBehaviour {

    public Text OwnerInfo;
    public Text HealthInfo;
    public Text AttackInfo;

    private static string redPlayer = "Owner: Red";
    private static string bluePlayer = "Owner: Blue";

    private static string healthHeader = "Health: ";
    private static string attackHeader = "Attack: ";

    public void Start()
    {
        this.HideInfoUI();
    }

    public void UpdateFromMapObject(MapObject mapObject)
    {
        if (mapObject.ownerID == 1)
            OwnerInfo.text = bluePlayer;
        else
            OwnerInfo.text = redPlayer;

        HealthInfo.text = healthHeader + mapObject.health.ToString();
        AttackInfo.text = attackHeader + mapObject.attack.ToString();
    }

    public void HideInfoUI()
    {
        OwnerInfo.enabled = false;
        HealthInfo.enabled = false;
        AttackInfo.enabled = false;
    }

    public void ShowInfoUI()
    {
        OwnerInfo.enabled = true;
        HealthInfo.enabled = true;
        AttackInfo.enabled = true;
    }
}
