using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateBonusScript : MonoBehaviour
{
    [SerializeField] ParticleSystem _bonusParticle;

    [SerializeField] Text _bonusText;

    public void PlayParticleBonus()
    {
        _bonusParticle.Play();

        _bonusText.gameObject.SetActive(true);

        _bonusText.text = "Вы получили пончик =)";

        #region _
        //_bonusParticle.Play();

        //_donutText.gameObject.SetActive(true);

        //await Task.Run(() =>
        //{
        //    while (_bonusParticle.isPlaying)
        //    {
        //        _donutText.text = "Вы получили пончик =)";
        //    }
        //});

        //_donutText.gameObject.SetActive(false);

        //gameObject.SetActive(false);
        #endregion
    }

    public void DeactivateBonus()
    {
        _bonusText.text = string.Empty;

        gameObject.SetActive(false);

        _bonusText.gameObject.SetActive(false);
    }
}
