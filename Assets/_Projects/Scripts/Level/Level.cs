using System;
using UnityEngine;

public class Level : PoolElement
{

    public void OnInit()
    {
    }

    public void Clear()
    {
        DespawnSelf();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
    }
#endif
}
