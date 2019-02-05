using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WinNotificationUI : MonoBehaviour
{
    public Image panel;
    public Button button;
    public Text text;

    public static string PlayerHeader = "Player ";
    public static string InfoHeader = " won!\n Now the app will be closed after clicking button.";

    public static string[] Players = { "Red", "Blue" };

    public void Start()
    {
        panel.enabled = false;
        button.gameObject.SetActive(false);
        text.enabled = false;
    }

    public void ShowWinAdnotacion(int winnerID)
    {
        panel.enabled = true;
        button.gameObject.SetActive(true);

        text.text = PlayerHeader + Players[winnerID] + InfoHeader;
        text.enabled = true;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}