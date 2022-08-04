using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class TaskArea : MonoBehaviour
{
    private GameObject Office;
    private bool canOpen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
    private void Update()
    {
        if (canOpen & Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("///Main Canvas").GetComponent<TaskUI>().AlterTask();
        }
    }
}
