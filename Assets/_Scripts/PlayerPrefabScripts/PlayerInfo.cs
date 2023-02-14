using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo 
{
    public int playerNumber = 0;
    public string name = "";
    public int playerUniqueID = 0;




    public PlayerInfo(int playerNumber, string name, int playerUniqueID)
    {
        this.playerNumber = playerNumber;
        this.name = name;
        this.playerUniqueID = playerUniqueID;
        
    }
}
