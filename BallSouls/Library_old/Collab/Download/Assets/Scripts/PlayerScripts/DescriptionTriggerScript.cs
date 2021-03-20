using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionTriggerScript : MonoBehaviour
{
    /// <summary>
    /// Поле текста описания
    /// </summary>
    [SerializeField] Text _descrioptionText;

    private void OnTriggerStay(Collider other)
    {
        // Условие "включения" описания
        if (other.tag.ToLower() == "player" && _descrioptionText != null)
            _descrioptionText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        // Условие "выключения" описания
        if (_descrioptionText != null)
            _descrioptionText.gameObject.SetActive(false);
    }
}
