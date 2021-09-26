using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        GameData.Config = Resources.Load<Config>("Main");
        GameData.ControlConfig = Resources.Load<ControlConfig>("Controller");
    }
}
