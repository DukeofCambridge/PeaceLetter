using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public enum PSDLayerType
{
    BOX_COLLIDER,
    CIRCLE_COLLIDER,
    POLYGON_COLLIDER,
    ANCHOR,
    IMAGE,
    FOLDER
};

public class SceneObjectSetup : MonoBehaviour
{
    public GameObject sceneObject;
}
