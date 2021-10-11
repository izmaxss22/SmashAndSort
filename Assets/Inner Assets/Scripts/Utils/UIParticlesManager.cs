using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticlesManager : MonoBehaviour
{
    public ParticleSystem victoryParticle_1;
    public ParticleSystem victoryParticle_2;

    public static UIParticlesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayVictoryParticle()
    {
        victoryParticle_1.Play();
        victoryParticle_2.Play();
    }
}
