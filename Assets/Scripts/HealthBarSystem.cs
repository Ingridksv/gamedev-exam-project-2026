using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{
    [SerializeField] bool shouldShowHealthNumbers = true;
    [SerializeField] float animationDuration = 0.1f;
    [SerializeField] bool hasAnimation = true;

    float finalValue;
    float animationSpeed;
    float leftoverAmount = 0f;

    // Caches
    Health health;
    Image image;
    Text text;

    private void Start()
    {
        health = GetComponentInParent<Health>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    
        Debug.Log("Health found: " + (health != null));
        Debug.Log("Image found: " + (image != null));
        Debug.Log("Image fill before: " + image.fillAmount);

       
        image.fillAmount = 1f;
        
        Debug.Log("Image fill after: " + image.fillAmount);
        
        health.onHurt.AddListener(RefreshBar);
        health.onDeath.AddListener(RefreshBar);
        health.onHeal.AddListener(RefreshBar);
        
        RefreshBar(); // initialize to full
    }

    void Update()
    {
        animationSpeed = animationDuration;

        if (!hasAnimation)
            image.fillAmount = health.GetHealthPercent();

        text.text = $"{health.GetHealth()}/{health.maxHealth}";
        text.enabled = shouldShowHealthNumbers;
        
    }

    private void RefreshBar()
    {
        Debug.Log("RefreshBar called, health%: " + health.GetHealthPercent());
        if (!hasAnimation) return;

        StopAllCoroutines();
        StartCoroutine(ChangeFillAmount(health.GetHealthPercent()));
    }

    private IEnumerator ChangeFillAmount(float targetPercent)
    {
        finalValue = targetPercent;

        float cacheLeftoverAmount = this.leftoverAmount;
        float startValue = image.fillAmount + cacheLeftoverAmount;
        float timeElapsed = 0;

        while (timeElapsed < animationSpeed)
        {
            float leftoverAmount = Mathf.Lerp(startValue, finalValue, timeElapsed / animationSpeed);
            this.leftoverAmount = leftoverAmount - finalValue;
            image.fillAmount = leftoverAmount;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        this.leftoverAmount = 0;
        image.fillAmount = finalValue;
    }
}