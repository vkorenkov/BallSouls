using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DonutCountScript : MonoBehaviour
{
    [SerializeField] List<Collider> donutList;
    public int donutCount;
    bool isBonusListNull;

    private void Awake()
    {
        if (donutList.Count == 0)
        {
            isBonusListNull = true;
            Time.timeScale = 0;
            Debug.LogError("список бонусов пуст");
        }
        else
            donutCount = donutList.Count;
    }

    private void Update()
    {
        if (!isBonusListNull)
        {
            if (donutList.Count == 0)
            {
                SceneControl.LoadNextScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "bonus")
        {
            donutCount--;
            donutList.Remove(other);
            Debug.Log(donutList.Count);
        }
    }
}
