using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float typingSpeed = 0.05f;
    public float initialDelay = 2f;
    public float lineDelay = 1f;

    [TextArea(3, 10)]
    public string[] dialogueLines;

    private Coroutine typingCoroutine;
    private int currentLineIndex;

    private void Start()
    {
        currentLineIndex = 0;
        StartCoroutine(StartDialogueWithDelay());
    }

    IEnumerator StartDialogueWithDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        StartDialogue();
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeDialogue());
    }

    public void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
            if (currentLineIndex < dialogueLines.Length)
                typingCoroutine = StartCoroutine(TypeDialogue());
        }
    }

    IEnumerator TypeDialogue()
    {
        string currentLine = dialogueLines[currentLineIndex];
        for (int i = 0; i <= currentLine.Length; i++)
        {
            dialogueText.text = currentLine.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        // Wait for a brief moment before going to the next line
        yield return new WaitForSeconds(lineDelay);

        // Move to the next line
        currentLineIndex++;

        // Check if there are more lines
        if (currentLineIndex < dialogueLines.Length)
        {
            // Start typing the next line
            typingCoroutine = StartCoroutine(TypeDialogue());
        }
    }
}
