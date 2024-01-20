using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManipulate : MonoBehaviour
{
    PostProcessVolume masterVolume;
    ColorGrading cg;
    // Start is called before the first frame update
    void Start()
    {
        masterVolume = GetComponent<PostProcessVolume>();
        masterVolume.profile.TryGetSettings(out cg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverEffects()
    {
        cg.saturation.value = -100;
        cg.contrast.value = 52;
    }
}
