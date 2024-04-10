using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed3rdPersonCamera : MonoBehaviour
{
    [SerializeField] float minDist;
    [SerializeField] float cameraTilt;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // This script follows the player towards the center of their attention, then stops rotating with them when they get too close.
        if (player.getDistance() >= minDist)
        {
            gameObject.transform.position = player.getLook();
            gameObject.transform.rotation = Quaternion.Euler(cameraTilt, player.getLookAt().eulerAngles.y, 0);
        }
    }
}
