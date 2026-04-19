using UnityEngine;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    public Image easyImage;
    public Image normalImage;
    public Image hardImage;

    public Sprite normalSprite;
    public Sprite selectedSprite;

    public void SelectEasy()
    {
        ResetAll();
        easyImage.sprite = selectedSprite;
    }

    public void SelectNormal()
    {
        ResetAll();
        normalImage.sprite = selectedSprite;
    }

    public void SelectHard()
    {
        ResetAll();
        hardImage.sprite = selectedSprite;
    }

    void ResetAll()
    {
        easyImage.sprite = normalSprite;
        normalImage.sprite = normalSprite;
        hardImage.sprite = normalSprite;
    }
}