using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrigger : MonoBehaviour
{
    [SerializeField] private GameObject lineSource;
    private ILineProcesser line;
    private void Start()
    {
        line = lineSource.GetComponent<ILineProcesser>();
    }
    private void OnTriggerEnter(Collider other)
    {
        line.LineStart();
    }
    private void OnTriggerExit(Collider other)
    {
        line.LineStop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        line.LineStart();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        line.LineStop();
    }
}
