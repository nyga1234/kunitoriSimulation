using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int turnCount;
    public GameManager.Phase phase;
    public GameManager.Step step;
}
