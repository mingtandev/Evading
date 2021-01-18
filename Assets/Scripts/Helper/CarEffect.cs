using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem smokeEffect;
    public ParticleSystem driftSmoke;
    public bool isDust;
    public bool isDrift;
    
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isDust) CreateDustSmoke();
        else StopDustSmoke();

        if (isDrift) CreateDriftSmoke();
        else StopDriftSmoke();
    }


    public void CreateDustSmoke()
    {
        if (!smokeEffect.isPlaying)
            smokeEffect.Play();
    }

    public void StopDustSmoke()
    {
        if (smokeEffect.isPlaying)
            smokeEffect.Stop();
    }


    public void CreateDriftSmoke()
    {
        if (!driftSmoke.isPlaying)
        {
            driftSmoke.Play();
        }

    }

    public void StopDriftSmoke()
    {
        if (driftSmoke.isPlaying)
        {
            //if (controller.rb.velocity.magnitude > 3)
                driftSmoke.Stop();
        }
    }

}
