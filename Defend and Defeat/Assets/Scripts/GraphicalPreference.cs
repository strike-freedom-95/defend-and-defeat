using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GraphicalPreference : MonoBehaviour
{
    [SerializeField] PostProcessVolume volume;
    [SerializeField] PostProcessProfile[] profiles;

    private void Start()
    {
        UpdateProfile();
    }

    public void UpdateProfile()
    {
        volume.profile = profiles[PlayerPrefs.GetInt("PP_Profile", 0)];
    }
}
