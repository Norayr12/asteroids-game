using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader: MonoBehaviour
{
    private void Awake()
    {
        GameData.Config = Resources.Load<Config>("Main");
        GameData.ControlConfig = Resources.Load<ControlConfig>("Controller");
        GameData.UIConfig = Resources.Load<UIConfig>("UI");
        GameData.PlayerConfig = Resources.Load<PlayerConfig>("Player");
    }
}
