using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    //public Animator animator;
    public float fadeTime = 1f;
    public float moveTransitTime = 1f;
    
    [HideInInspector] public enum DialogueType
    {
        Fade,
        Move
    };

    private Text nameText;
    private Text dialogueText;
    private GameObject dialogueBox;
    
    private Queue<SentenceInfo> sentences;
    private DialogueType type;
    private Sequence fadeSequence;
    private Coroutine coroutine;

    private void Start()
    {
        sentences = new Queue<SentenceInfo>();
        
        GameObject dialogueCanvas = GameObject.Find("Dialogue");
        nameText = dialogueCanvas.transform.GetChild(0).Find("Name").GetComponent<Text>();
        dialogueText = dialogueCanvas.transform.GetChild(0).Find("Dialogue").GetComponent<Text>();
        dialogueBox = dialogueCanvas.transform.GetChild(0).gameObject;
    }

    public void StartDialogue(Dialogue dialogue, DialogueType type = DialogueType.Fade, bool isAuto = false)
    {
        sentences.Clear();
        foreach (SentenceInfo sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        this.type = type;
        nameText.text = dialogue.name;
        switch (this.type)
        {
            case DialogueType.Fade:
                dialogueText.DOFade(0, 0);
                dialogueBox.GetComponent<RectTransform>().DOAnchorPosY(0, 0);
                break;
            case DialogueType.Move:
                dialogueBox.GetComponent<RectTransform>().DOAnchorPosY(0, moveTransitTime);
                break;
        }
        //animator.SetBool("isOpen",true);
        if (isAuto)
            StartCoroutine(DisplayNextSentenceAuto(dialogue.sentences.Length));
        else
            DisplayNextSentence();
    }

    IEnumerator DisplayNextSentenceAuto(int counts)
    {
        for (int i = 0; i <= counts; i++)
        {
            DisplayNextSentence();
            if(fadeSequence.IsActive())
                yield return fadeSequence.WaitForCompletion();
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        SentenceInfo sentence = sentences.Dequeue();
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(SentenceInfo sentence)
    {
        fadeSequence = DOTween.Sequence();
        switch (type)
        {
            case DialogueType.Fade:
                fadeSequence.Append(dialogueText.DOFade(0, fadeTime))
                    .AppendCallback(() => { dialogueText.text = sentence.text; })
                    .Append(dialogueText.DOFade(1, fadeTime))
                    .AppendInterval(sentence.stayTime);
                break;
            case DialogueType.Move:
                fadeSequence.Append(dialogueText.DOText(sentence.text,1))
                    .AppendInterval(sentence.stayTime);
/*                dialogueText.text = "";
                foreach (char letter in text.ToCharArray())
                {
                    dialogueText.text += letter;
                    yield return null;
                }
                break;*/
                yield return null;
                break;
        }
    }

    private void EndDialogue()
    {
        switch (type)
        {
            case DialogueType.Fade:
                dialogueText.DOFade(0, fadeTime);
                break;
            case DialogueType.Move:
                dialogueBox.GetComponent<RectTransform>().DOAnchorPosY(-500, moveTransitTime);
                break;
        }
        //animator.SetBool("isOpen",false);
    }
}