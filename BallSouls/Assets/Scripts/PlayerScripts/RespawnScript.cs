using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] List<Transform> roomPoints;

    [SerializeField] List<Animation> roomAnimations;

    Rigidbody player;

    bool teleport;

    float teleportTimer = 0;

    private void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    public void RespawnPosition(Vector3 startPosition)
    {
        // Снижение скорости игрока
        player.velocity = new Vector3();
        // Снижение вращения игрока
        player.transform.rotation = new Quaternion();
        // помещение игрока в позицию возраждения
        player.transform.position = startPosition;
    }

    private void Update()
    {
        if (teleport)
        {
            teleportTimer -= Time.deltaTime;

            if (teleportTimer < 0)
            {
                teleport = false;
                player.isKinematic = false;
            }
        }
    }

    public void ChangeRoom(int roomNumber, bool isAnimation)
    {
        teleport = true;

        if (isAnimation)
        {
            player.isKinematic = true;

            foreach (var a in roomAnimations)
            {
                a.gameObject.SetActive(true);
            }

            if (!roomAnimations.Last().isPlaying)
            {
                try
                {
                    teleportTimer = roomAnimations[roomNumber].clip.length;
                    roomAnimations[roomNumber].Play();
                }
                catch
                {
                    roomAnimations.Last().Play();
                    teleportTimer = roomAnimations.Last().clip.length;
                }
            }
        }

        player.position = roomPoints[roomNumber].position;
    }
}
