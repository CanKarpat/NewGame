using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Private Constants

    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";
    public TMP_InputField inputField;
    #endregion

    #region MonoBehaviour CallBacks

    /// &lt;summary&gt;
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// &lt;/summary&gt;
    void Start () {

        string defaultName = string.Empty;
        
        if (inputField!=null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName =  defaultName;
    }

    #endregion

    #region Public Methods

    /// &lt;summary&gt;
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// &lt;/summary&gt;
    /// &lt;param name="value"&gt;The name of the Player&lt;/param&gt;
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey,value);
    }

    #endregion
}


