using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ExpToUpgradeScriptableObject", menuName = "ScriptableObjects/ExpToUpgradeScriptableObject")]
public class ExpToUpgradeScriptableObject : ScriptableObject
{
    public List<int> expToUpgradeList;
}
