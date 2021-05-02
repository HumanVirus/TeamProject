using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public Color BackColor;
    public GameObject UIStartSet;
    public GameObject UIAchiveSet; 
    public GameObject UIOptionSet;

    private void Awake()
    {
        UIStartSet.transform.DOScale(Vector3.zero, 0f);
        UIAchiveSet.transform.DOScale(Vector3.zero, 0f);
        UIOptionSet.transform.DOScale(Vector3.zero, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeBackground", 3.0f);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitBtnState()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void ChangeBackground()
    {
        Camera.main.backgroundColor = BackColor;
    }
    public void StartBtnClick()
    {   
            UIStartSet.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public void NewBtnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadBtnClick()
    {
        SceneManager.LoadScene(2);
    }
    public void AchiveBtnClick()
    {
       UIAchiveSet.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public void OptionBtnClick()
    {
        UIOptionSet.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public void ExitBtnClick()
    {
        if (UIStartSet.transform.localScale == Vector3.one)
            UIStartSet.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        else if (UIAchiveSet.transform.localScale == Vector3.one)
            UIAchiveSet.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        else if (UIOptionSet.transform.localScale == Vector3.one)
            UIOptionSet.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);

        else
            Application.Quit();
    }
}
