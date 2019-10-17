using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraControls : MonoBehaviour
{
    public GameObject player;
    private Camera myCam;
    private VehicleControls playerControls;

    float minPlayerSpeed = 0;
    float maxPlayerSpeed = 100;

    float minFOV = 60;
    float maxFOV = 120;

    // below is a post-processing hint
    // you do not have to use post-processing or lens distortion if you don't want to!
    PostProcessVolume volume;
    LensDistortion lensDistortionLayer;
    //float minLensDistortion = ???;
    //float maxLensDistortion = ???;

    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();
        playerControls = player.GetComponent<VehicleControls>();

        // more post-processing hints
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out lensDistortionLayer);
        lensDistortionLayer.enabled.Override(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newCameraPosition = player.transform.position - (player.transform.forward * 5);
        transform.position = newCameraPosition;
        transform.LookAt(player.transform.position);

        AddFeel();
    }

    void AddFeel()
    {
        float temporaryFOV = Remap(playerControls.currentSpeed, minPlayerSpeed, maxPlayerSpeed, minFOV, maxFOV);
        float newFOV = Mathf.Clamp(temporaryFOV, minFOV, maxFOV);
        myCam.fieldOfView = newFOV;

        // how might you adjust the lens distortion based on player speed?
        // e.g. lensDistortionLayer.intensity.Override(???);
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
