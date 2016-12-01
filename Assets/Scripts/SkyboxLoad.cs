using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxLoad : MonoBehaviour {

    public Material[] skyboxes;
    Material chosenSkybox;

	// Use this for initialization
	void Start () {
        chosenSkybox = skyboxes[Random.Range(0, skyboxes.Length)];

        RenderSettings.skybox = chosenSkybox;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
