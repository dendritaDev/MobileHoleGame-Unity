using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerSize : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Image fillImage;

    [Header(" Settings ")]
    private float scaleValue;
    [SerializeField] private float scaleIncreaseTreshold;
    [SerializeField] private float scaleStep;

    [Header(" Power ")]
    private float powerMultiplier;

    [Header(" Events ")]
    public static Action<float> onIncrease;

    private void Awake()
    {
        UpgradesManager.onDataLoaded += UpgradesDataLoadedCallback;

    }

    // Start is called before the first frame update
    void Start()
    {
        fillImage.fillAmount = 0;

        UpgradesManager.onSizePurchased += SizePurchasedCallback;
        UpgradesManager.onPowerPurchased += PowerPurchasedCallback;
    }

    private void OnDestroy()
    {
        UpgradesManager.onSizePurchased -= SizePurchasedCallback;
        UpgradesManager.onPowerPurchased -= PowerPurchasedCallback;
        UpgradesManager.onDataLoaded -= UpgradesDataLoadedCallback;
    }


    private void IncreaseScale()
    {
        float targetScale = transform.localScale.x + scaleStep;
        UpdateScale(targetScale);
    }

    private void UpdateScale(float targetScale)
    {
        LeanTween.scale(transform.gameObject, targetScale * Vector3.one, .5f * Time.deltaTime * 60).setEase(LeanTweenType.easeInOutBack);

        onIncrease?.Invoke(targetScale);
    }

    public void CollectibleColledcted(float objectSize)
    {
        scaleValue += objectSize * (1 + powerMultiplier);

        if(scaleValue > scaleIncreaseTreshold)
        {
            IncreaseScale();
            scaleValue = scaleValue % scaleIncreaseTreshold;
        }

        UpdateFillDisplay();
    }

    private void UpdateFillDisplay()
    {
        float targetFillAmount = scaleValue / scaleIncreaseTreshold;
        float fromFillAmount = fillImage.fillAmount;

        LeanTween.value(fromFillAmount, targetFillAmount, .2f * Time.deltaTime * 60)
            .setOnUpdate((value) => fillImage.fillAmount = value);
        
    }

    private void SizePurchasedCallback()
    {
        IncreaseScale();
    }

    private void PowerPurchasedCallback()
    {
        powerMultiplier++;
    }


    private void UpgradesDataLoadedCallback(int timerLevel, int sizeLevel, int powerLevel)
    {
        float targetScale = transform.localScale.x + scaleStep * sizeLevel;
        UpdateScale(targetScale);

        powerMultiplier = powerLevel;
    }


}
