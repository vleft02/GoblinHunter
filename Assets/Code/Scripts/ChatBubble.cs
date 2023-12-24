using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TextMeshPro textMeshPro;
    [SerializeField] private string myText;
    [SerializeField] private bool changeText = false;

    private List<string> textLines;

    private Vector3 initialPosition;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = backgroundSpriteRenderer.transform.Find("Text").GetComponent<TextMeshPro>();
        textLines = new List<string>();
    }

    private void Start()
    {
        Setup("yo yo");
        //UpdatePosition(textMeshPro.GetRenderedValues(false).x);
        initialPosition = backgroundSpriteRenderer.transform.localPosition;
    }

    private void Update()
    {
        if (changeText)
        {
            Setup(myText);
            //UpdatePosition(textMeshPro.GetRenderedValues(false).x);
            changeText = false;
        }
    }

    private void Setup(string text)
    {
        //handleString(text, 8);

        //Debug.Log("text0: " + textLines[0]);

        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(2f, 0.5f);
        backgroundSpriteRenderer.size = textSize + padding;

        backgroundSpriteRenderer.transform.localPosition = new Vector3
             (
                 initialPosition.x - textSize.x * 0.01f,
                 0f
             );

    }

    private void UpdatePosition(float offset)
    {
        backgroundSpriteRenderer.transform.localPosition = new Vector3
             (
                 initialPosition.x - offset * 0.08f,
                 0f
             );
    }

    private void handleString(string text, int sentenseLength)
    {
        textLines.Clear();

        string currentText = text;

        for (int i = 0; currentText.Length > i+sentenseLength; i+=sentenseLength)
        {
            int index = i + sentenseLength;
            textLines.Add(currentText.Substring(i, index));
            
        }

        if (textLines.Count > 0)
        { 
            textLines.Add(currentText.Substring((textLines.Count * sentenseLength)+1, currentText.Length-1));
        }
        else
        {
            textLines.Add(currentText.Substring(0, currentText.Length - 1));
        }

    }



}
