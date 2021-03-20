using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] List<Transform> roomList;

    [SerializeField] List<Animation> roomAnimations;

    public void RespawnPosition(Rigidbody rb, Vector3 startPosition)
    {
        // Снижение скорости игрока
        rb.velocity = new Vector3();
        // Снижение вращения игрока
        rb.transform.rotation = new Quaternion();
        // помещение игрока в позицию возраждения
        rb.transform.position = startPosition;
    }

    public void ChangeRoom(int roomNumber, Transform player)
    {
        foreach(var a in roomAnimations)
        {
            a.gameObject.SetActive(true);
            a.Stop();
        }

        if (!roomAnimations.Last().isPlaying)
        {
            player.position = roomList[roomNumber].position;
        }
    }
}
