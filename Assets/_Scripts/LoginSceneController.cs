using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using TMPro;

public class LoginSceneController : MonoBehaviour
{
    public Launcher launcher;
    
    public TMP_InputField nicknameField;
    public TMP_InputField roomNameField;

    //TODO: Dropdown aç içindeki listeden oda seçtir.
    
    public Toggle hostToggle;
    public Toggle clientToggle;
    public Button startButton;
    
    public void CheckPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length < 3)
        {
            Debug.LogError("Player Name is null or empty!");
            startButton.interactable = false;
            return;
        }

        Debug.Log("Player name letter count is okay!");
        startButton.interactable = true;
    }

    public void CheckJoinMode(bool value)
    {
        // if (value)
        // {
        //     roomNameField.interactable = true;
        // }
        // else
        // {
        //     roomNameField.interactable = false;
        // }
    }

    public void StartGame()
    {
        if (hostToggle.isOn && !clientToggle.isOn)
        {
            launcher.CreateAsHost(roomNameField.text);
        }
        else if (!hostToggle.isOn && clientToggle.isOn)
        {
            launcher.CreateAsClient(roomNameField.text);
        }
    }
}