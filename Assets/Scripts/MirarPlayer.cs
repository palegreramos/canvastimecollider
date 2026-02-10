using UnityEngine;

public class MirarPlayer : MonoBehaviour
{

    private Transform camPlayer;

    void Start()
    {
        OVRCameraRig ovrRig = FindFirstObjectByType<OVRCameraRig>();
        if (ovrRig != null)
        {
            camPlayer = ovrRig.centerEyeAnchor;
        }
    }

    void Update()
    {
        if (camPlayer != null)
        {
            //transform.LookAt(camPlayer);
            //transform.Rotate(0, 180f, 0);
            transform.forward = transform.position - camPlayer.position;
        }
    }
}


