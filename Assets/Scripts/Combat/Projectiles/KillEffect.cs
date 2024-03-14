using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEffect : MonoBehaviour
{
    public static KillEffect Instance;
    public ParticleSystem splatSystem;

    private void Start() {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void DoSplat(Vector3 pos, int amount)
    {
        transform.position = pos;
        splatSystem.Emit(amount);
    }
}
