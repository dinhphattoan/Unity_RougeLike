using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform transformPlayer;
    [Header("Camera attributes")]
    public Vector3 offset = new Vector3(0, 10, -100);
    public float moveSpeed = 5f;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
            this.transform.position = Vector3.SmoothDamp(this.transform.position, transformPlayer.position + offset, ref velocity, Time.deltaTime);
        

    }
    private void OnDrawGizmos()
    {
    }
}
