using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ScriptableObjectCreator : ScriptableObject
{
    public string prefabName;
    
    [Space(10)]public List<Vector3> playerTowerPositionsForRoundOne = new List<Vector3>();
    public List<Vector3> playerTowerRotationsForRoundOne = new List<Vector3>();
    
    [Space(10)]public List<Vector3> playerTowerPositionsForRoundTwo = new List<Vector3>();
    public List<Vector3> playerTowerRotationsForRoundTwo = new List<Vector3>();
    
    [Space(10)]public List<Vector3> playerTowerPositionsForRoundThree = new List<Vector3>();
    public List<Vector3> playerTowerRotationsForRoundThree = new List<Vector3>();
    
    [Space(10)]public List<Vector3> playerTowerPositionsForRoundFour = new List<Vector3>();
    public List<Vector3> playerTowerRotationsForRoundFour = new List<Vector3>();
    
    [Space(5)]public List<Vector3> usedPositions = new List<Vector3>();
    public List<Vector3> usedRotations = new List<Vector3>();
    
    [Space(20)] public List<Material> playerColors = new List<Material>();
    [Space(5)]public List<Material> usedColors = new List<Material>();
}
