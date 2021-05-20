using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PrologueManager : MonoBehaviour
{
    public Transform[] ImgToon;
    public int page;
    public Button LeftBTN;
    public Button RightBTN;
    public Button IngameBTN;
    public Image fade;
    // Start is called before the first frame update

    public void RightBtnclick()
    {
        page++;
        ImgToon[page].DOMoveY(1000, 0.5f).SetEase(Ease.OutBack);

        if (page==3)
        {
            RightBTN.gameObject.SetActive(false);
            IngameBTN.gameObject.SetActive(true);
        }
    }
    public void INgameBtn()
    {
        fade.gameObject.SetActive(false);
        fade.DOFade(1, 1).SetEase(Ease.InQuad);
        Invoke("Next", 3);
    }
    public void Next()
    {
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
