using Homewor11.InputControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    [SerializeField] GameObject reception;
    [SerializeField] GameObject point;
    Collider player;
    bool isTeleported;
    float teleportTimer = 1;
    PlayerInputs playerControl;

    private void Update()
    {
        if (isTeleported)
        {
            teleportTimer -= Time.deltaTime;

            if (teleportTimer <= 0)
            {
                player.transform.position = reception.transform.position;
                isTeleported = false;
                teleportTimer = 1;
                player.attachedRigidbody.useGravity = true;
                player = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            if (playerControl == null)
                playerControl = other.gameObject.GetComponent<PlayerInputs>();

            player = other;
            isTeleported = true;
            other.attachedRigidbody.velocity = new Vector3();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.ToLower() == "player")
        {
            if (player.attachedRigidbody.useGravity == true)
                player.attachedRigidbody.useGravity = false;

            playerControl.isControlActive = false;

            player.transform.position = Vector3.MoveTowards(player.transform.position, point.transform.position, 0.08f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.ToLower() == "player")
            playerControl.isControlActive = true;
    }
}
