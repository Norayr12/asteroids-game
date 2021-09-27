using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "UI", menuName = "Config/UI")]
public class UIConfig : ScriptableObject
{
    public Sprite LifeIcon;

    public KeyCode PauseButton;
}
