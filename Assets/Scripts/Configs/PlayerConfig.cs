using UnityEngine;

[CreateAssetMenu( fileName = "Player", menuName = "Config/Player")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player settings")]
    public int PlayerMaxLifes;
}
