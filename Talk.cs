using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;

}

public class Talk : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sprite_dialogue;
    [SerializeField] private Text text_dialogue;
    public Button SkipBTN;
    private bool isDialogue = false;
    private int count = 0;

    [SerializeField] private Dialogue[] dialogues;


    public void Show()
    {
        sprite_dialogue.gameObject.SetActive(true);
        text_dialogue.gameObject.SetActive(true);

        count = 0;
        isDialogue = true;
    }

    private void Next()
    {
        text_dialogue.text = dialogues[count].dialogue;
        count++;
    }
    private void Hide()
    {
        sprite_dialogue.gameObject.SetActive(false);
        text_dialogue.gameObject.SetActive(false);
        isDialogue = false;
    }
    void Update()
    {
        if(isDialogue)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if (count < dialogues.Length)
                    Next();
                else
                    Hide();
            }
        }
        
    }

    public void Skip()
    {
        SceneManager.LoadScene(3);
    }
  
}
