using Roguelike.Main;
using Roguelike.Utilities;
using UnityEngine;

public class UIService : MonoBehaviour,IService
{
    private void Awake()
    {
    }

    public void Initialize(params object[] dependencies)
    {
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
    }

    private void UnsubscribeToEvents()
    {
    }

    private void OnDestroy() => UnsubscribeToEvents();
}
