using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOrbitRotation : MonoBehaviour
{

    public GameObject Player;
    public float Speed;

    void Update()
    {
        orbitRotation();
    }

    private void orbitRotation()
    {
        transform.RotateAround(Player.transform.position, Vector3.forward, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
