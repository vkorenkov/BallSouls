using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveScript : MonoBehaviour
{
    /// <summary>
    /// Поле коллекция объектов активации/деактивации
    /// </summary>
    [SerializeField] List<GameObject> _activateObject;

    private void OnTriggerEnter(Collider other)
    {
        // Условие активации списка объектов/деактивации
        if (other.tag.ToLower() == "player")
        {
            // Цикл активации/деактивации объектов
            foreach (var a in _activateObject)
            {
                if (!a.activeSelf)
                    a.SetActive(true);
                else
                    a.SetActive(false);
            }
        }
    }
}
