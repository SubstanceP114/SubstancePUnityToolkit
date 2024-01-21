using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Lines : MonoBehaviour, ILineProcecer
{
    [SerializeField] private bool playOnce;
    [SerializeField] private float interval = 0.02f;
    [SerializeField] private GameObject textPosition;
    [SerializeField] private List<string> lines = new List<string>();

    private bool texting;
    private Queue<string> lineCopy = new Queue<string>();
    private Text text;
    private TextMesh textMesh;
    private int index = 0;
    private string currentLine;
    private void Start()
    {
        text = textPosition.GetComponent<Text>();
        textMesh = textPosition.GetComponent<TextMesh>();
        Restart();
        if (playOnce) foreach (var line in lines) lineCopy.Enqueue(line);
    }
    public void LineStart()
    {
        if (texting) return;
        StartCoroutine(ShowDialog());
    }
    public void LineStop() => Restart();
    private void Restart()
    {
        textPosition.SetActive(false);
        texting = false;
        MatchText(" ");
        if (playOnce) return;
        lineCopy.Clear();
        foreach (var line in lines) lineCopy.Enqueue(line);
    }
    private void MatchText(string targetText)
    {
        if (textMesh) textMesh.text = targetText;
        else if (text) text.text = targetText;
        else Debug.Log("There is no matching text component, chech Text Position please.");
    }
    private IEnumerator ShowDialog()
    {
        texting = true;
        textPosition.SetActive(true);
        while (lineCopy.Count > 0)
        {
            currentLine = lineCopy.Dequeue();
            index = 0;
            while (index <= currentLine.Length)
            {
                if (!texting) yield break;

                yield return new WaitForSeconds(interval);
                MatchText(currentLine.Substring(0, index));
                index++;
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0) || !texting);

            if (!texting) yield break;
        }
        Restart();
    }
}
