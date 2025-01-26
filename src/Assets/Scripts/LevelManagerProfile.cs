using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Manager Profile", menuName = "Level Manager Profile")]
public class LevelManagerProfile : ScriptableObject
{
    public List<Level> levels;
}
