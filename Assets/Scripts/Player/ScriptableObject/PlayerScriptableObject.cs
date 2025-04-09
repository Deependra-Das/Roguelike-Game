using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public List<PlayerData> playerDataList;
    }
}
