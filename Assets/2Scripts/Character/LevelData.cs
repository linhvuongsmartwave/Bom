using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ListEnemy", menuName = "ListEnemy")]
public class LevelData : ScriptableObject
{

    public List<GameObject> enemies;

}
