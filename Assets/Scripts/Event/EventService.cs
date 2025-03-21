using UnityEngine;
using Roguelike.Utilities;

namespace Roguelike.Event
{
    public class EventService : IService
    {
        public void Initialize(params object[] dependencies) 
        {
            Debug.Log("Event Service Running");
        }
    }
}
