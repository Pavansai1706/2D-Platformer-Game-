using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("FootSteps")]
    public List<AudioClip> footstepFS;
    public List<AudioClip> stoneFS;

    enum FSMaterial
    {
        footstep,
        stone,
    }

    private AudioSource footStepSource;

    private void Start()
    {
        footStepSource = GetComponent<AudioSource>();
    }

    void PlayFootStep(FSMaterial material)
    {
        AudioClip clipToPlay = null;

        switch (material)
        {
            case FSMaterial.footstep:
                if (footstepFS.Count > 0)
                {
                    clipToPlay = footstepFS[UnityEngine.Random.Range(0, footstepFS.Count)];
                }
                break;
            case FSMaterial.stone:
                if (stoneFS.Count > 0)
                {
                    clipToPlay = stoneFS[UnityEngine.Random.Range(0, stoneFS.Count)];
                }
                break;
        }

        if (clipToPlay != null)
        {
            footStepSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning("No footstep sound available for the specified material.");
        }
    }

    // Example of calling PlayFootStep when needed
    void ExampleMethod()
    {
        // Call PlayFootStep with appropriate material
        PlayFootStep(FSMaterial.footstep);
        // or
        PlayFootStep(FSMaterial.stone);
    }
}

