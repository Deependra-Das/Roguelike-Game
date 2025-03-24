using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI
{ 
    [CreateAssetMenu(fileName = "LevelButtonScriptableObject", menuName = "ScriptableObjects/LevelButtonScriptableObject")]
    public class LevelButtonScriptableObject : ScriptableObject
    {
        public int ID;
        public Image levelImage;
        public string levelName;
        public string levelDescription;
    }
  
}
