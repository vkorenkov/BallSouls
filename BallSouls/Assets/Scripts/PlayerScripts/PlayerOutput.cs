using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOutput : MonoBehaviour
{
    /// <summary>
    /// Поле изображения здоровья игрока
    /// </summary>
    [SerializeField] Image HealthImage;

    [SerializeField] Text LivesText;

    [SerializeField] Text healthText;

    PlayerHeathScript playerHeath;

    DonutCountScript donutCount;

    [SerializeField] GameObject textPanel;
    [SerializeField] Text _descriptionText;
    [SerializeField] Text _donutCountText;

    PlaySounds playSounds;

    float _textLiveTimer = 5;

    private void Start()
    {
        playerHeath = GetComponent<PlayerHeathScript>();

        playSounds = GetComponent<PlaySounds>();

        donutCount = GetComponent<DonutCountScript>();

        playSounds.PlayRandomSound();
    }

    private void Update()
    {
        if (!playSounds.IsPlayedInCollection())
            playSounds.PlayRandomSound();

        LivesText.text = $"x {playerHeath.lives}";

        _donutCountText.text = $"x {donutCount.donutCount}";

        if (playerHeath.health > 0)
            healthText.text = $"x {playerHeath.health}";
        else
            healthText.text = "0";

        CheckDescription();

        // вызов метода изменения изображения здоровья игрока
        ChangeHealthSprite();

        // условие получил ли игрок урон
        if (playerHeath.isHit)
        {
            // сброс таймера неуязвимости
            if (playerHeath.postHitTime <= 0)
            {
                playerHeath.isHit = false;
            }
        }
    }

    void CheckDescription()
    {
        if (textPanel.activeSelf)
        {
            _textLiveTimer -= Time.deltaTime;

            if (_textLiveTimer <= 0)
            {
                textPanel.SetActive(false);

                _textLiveTimer = 5;
            }
        }
    }

    /// <summary>
    /// Метод изменения изображения здоровья игрока
    /// </summary>
    void ChangeHealthSprite()
    {
        if (playerHeath.health == 100)
            HealthImage.fillAmount = 1;

        HealthImage.fillAmount = playerHeath.health * 0.01f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() == "bonus")
        {
            textPanel.SetActive(true);

            _descriptionText.text = "Вы получили пончик =)";
        }
    }
}
