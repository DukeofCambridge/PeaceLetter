using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DynamicObjectOrdering : MonoBehaviour
{
    private GameObject target;
    private SpriteRenderer targetSpriteRenderer;
    
    void Start()
    {
        target = gameObject.transform.parent.gameObject;
        targetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        targetSpriteRenderer.sortingOrder = (int) gameObject.transform.position.y * -1;
    }

    void Update()
    {
        targetSpriteRenderer.sortingOrder = (int) gameObject.transform.position.y * -1;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 center = gameObject.transform.position;
        Handles.color = Color.yellow;
        Handles.DrawSolidArc(center, new Vector3(0,0,1), new Vector3(0,1,0), 360, 0.25f);
        Handles.color = Color.red;
        Handles.DrawSolidArc(center, new Vector3(0,0,1), new Vector3(0,1,0), 360, 0.05f);
        Handles.color = Color.black;
        Handles.DrawWireArc(center, new Vector3(0,0,1), new Vector3(0,1,0), 360, 0.26f, 2);
    }
#endif
}
