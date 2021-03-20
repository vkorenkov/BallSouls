using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatedPlateSpawn : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Plates;

    [SerializeField]
    GameObject AnimatedPlate;

    List<int> RetiredNum;

    // Start is called before the first frame update
    void Start()
    {
        RetiredNum = new List<int>();

        AnimatePlateSpawn();
    }

    private void AnimatePlateSpawn()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < 20; i++)
        {
            int rNum = random.Next(0, Plates.Count);

            if (!RetiredNum.Contains(rNum) && Plates[rNum] != Plates.First() && Plates[rNum] != Plates.Last())
            {
                var tempPosition = Plates[rNum].transform.position;

                Destroy(Plates[rNum]);

                Instantiate(AnimatedPlate, tempPosition, Quaternion.identity);

                RetiredNum.Add(rNum);
            }
        }
    }
}
