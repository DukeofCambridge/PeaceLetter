using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneObjectSetup))]
public class SceneObjectSetupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SceneObjectSetup setup = (SceneObjectSetup)target;
        
        if (GUILayout.Button("Set Up Scene Object"))
        {
            SetUp(setup.sceneObject);
        }
    }
    
    
    public void SetUp(GameObject sceneObject)
    {
        GameObject groundObjectGroup = GetChildWithName(sceneObject, "地面");
        int groundLayer = SortingLayer.NameToID("Ground");
        SetSpriteLayers(groundObjectGroup, groundLayer);
        
        GameObject defaultObjectGroup = GetChildWithName(sceneObject, "正常");
        int defaultLayer = SortingLayer.NameToID("Default");
        SetSpriteLayers(defaultObjectGroup, defaultLayer);
        
        GameObject aboveObjectGroup = GetChildWithName(sceneObject, "屋顶");
        int aboveLayer = SortingLayer.NameToID("Above");
        SetSpriteLayers(aboveObjectGroup, aboveLayer);
        
        List<GameObject> children = GetAllChild(sceneObject);
        foreach (GameObject child in children)
        {
            switch (GetTypeFromLayer(child))
            {
                case PSDLayerType.BOX_COLLIDER:
                    child.AddComponent<Rigidbody2D>();
                    child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    child.AddComponent<BoxCollider2D>();
                    child.layer = LayerMask.NameToLayer("Editor");
                    EditorUtility.SetDirty(child);
                    break;
                case PSDLayerType.CIRCLE_COLLIDER:
                    child.AddComponent<Rigidbody2D>();
                    child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    child.AddComponent<CircleCollider2D>();
                    child.layer = LayerMask.NameToLayer("Editor");
                    EditorUtility.SetDirty(child);
                    break;
                case PSDLayerType.POLYGON_COLLIDER:
                    child.AddComponent<Rigidbody2D>();
                    child.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    child.AddComponent<PolygonCollider2D>();
                    child.layer = LayerMask.NameToLayer("Editor");
                    EditorUtility.SetDirty(child);
                    break;
                case PSDLayerType.ANCHOR:
                    child.AddComponent<StaticObjectOrdering>();
                    child.layer = LayerMask.NameToLayer("Editor");
                    
                    GameObject imageObject = FindImageNeighbor(child);
                    child.GetComponent<StaticObjectOrdering>().target = imageObject;
                    EditorUtility.SetDirty(child);
                    break;
                default:
                    break;
            }
        }
    }

    private GameObject FindImageNeighbor(GameObject anchor)
    {
        Transform parent = anchor.transform.parent;
        int n = parent.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            if (GetTypeFromLayer(child) == PSDLayerType.IMAGE)
            {
                return child;
            }
        }
        
        Debug.LogError($"Error: did not find neighbor of type Image for {anchor.name} under {parent.name}");
        return null;
    }

    private PSDLayerType GetTypeFromLayer(GameObject layerObject)
    {
        string name = layerObject.name;
        if (name.Length >= 3 && name.Substring(0, 3).ToUpper() == "BOX")
        {
            return PSDLayerType.BOX_COLLIDER;
        }
        
        if (name.Length >= 6 && name.Substring(0, 6).ToUpper() == "CIRCLE")
        {
            return PSDLayerType.CIRCLE_COLLIDER;
        }
        
        if (name.Length >= 7 && name.Substring(0, 7).ToUpper() == "POLYGON")
        {
            return PSDLayerType.POLYGON_COLLIDER;
        }
        
        if (name.Length >= 6 && name.Substring(0, 6).ToUpper() == "ANCHOR")
        {
            return PSDLayerType.ANCHOR;
        }

        if (layerObject.GetComponent<SpriteRenderer>() != null)
        {
            return PSDLayerType.IMAGE;
        }

        return PSDLayerType.FOLDER;
    }
    
    private void SetSpriteLayers(GameObject parent, int targetLayer)
    {
        if (parent.GetComponent<SpriteRenderer>() != null)
        {
            parent.GetComponent<SpriteRenderer>().sortingLayerID = targetLayer;
        }
        
        int n = parent.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            SetSpriteLayers(child, targetLayer);
        }
    }
    
    private GameObject GetChildWithName(GameObject parent, string name)
    {
        int n = parent.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            if (child.name.Equals(name))
            {
                return child.gameObject;
            }

            GameObject grandChildren = GetChildWithName(child.gameObject, name);
            if (grandChildren != null)
            {
                return grandChildren;
            }
        }

        return null;
    }
    
    private List<GameObject> GetAllChild(GameObject parent)
    {
        List<GameObject> currentObjects = new List<GameObject>();
        int n = parent.transform.childCount;
        for (int i = 0; i < n; i++)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            currentObjects.Add(child);
            List<GameObject> grandChildren = GetAllChild(child);
            currentObjects = currentObjects.Concat(grandChildren).ToList();
        }

        return currentObjects;
    }
}
