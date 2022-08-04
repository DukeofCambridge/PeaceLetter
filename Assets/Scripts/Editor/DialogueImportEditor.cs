using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DialogueImporter))]
public class DialogueImportEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DialogueImporter dialogue = (DialogueImporter)target;
        
        if (GUILayout.Button("Import Dialogues"))
        {
            Import();
        }
        
        if (GUILayout.Button("Process Dialogues"))
        {
            ProcessDialogue();
        }
    }

    public void Import()
    {
        String jsonPath = Util.GetPathToFolder("Json Data");
        String dataPath = Util.GetPathToFolder("Dialogue Data");
        HashSet<string> dataNames = new HashSet<string>();
        DirectoryInfo dataInfo = new DirectoryInfo(dataPath);
        foreach (FileInfo dataFile in dataInfo.GetFiles("*.asset", SearchOption.AllDirectories))
        {
            dataNames.Add(Util.getName(dataFile));
        }
        
        DirectoryInfo jsonInfo = new DirectoryInfo(jsonPath);
        foreach (FileInfo jsonFile in jsonInfo.GetFiles("*.json", SearchOption.AllDirectories))
        {
            string filePath = jsonFile.FullName;
            int length = filePath.Length - jsonInfo.FullName.Length + jsonPath.Length;
            filePath = filePath.Substring(jsonFile.FullName.Length - length, length);

            string info = File.ReadAllText(filePath);
            string name = Util.getName(jsonFile);
            if (!dataNames.Contains(name))
            {
                Debug.Log("creating asset " + name);
                Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();
                string newPath = dataPath + "/" + name + ".asset";
                AssetDatabase.CreateAsset(dialogue, newPath);
                dialogue.Instantiate(info);
                EditorUtility.SetDirty(dialogue);
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void ProcessDialogue()
    {
        String dataPath = Util.GetPathToFolder("Dialogue Data");
        HashSet<string> dataNames = new HashSet<string>();
        DirectoryInfo dataInfo = new DirectoryInfo(dataPath);
        foreach (FileInfo dataFile in dataInfo.GetFiles("*.asset", SearchOption.AllDirectories))
        {
            string filePath = dataFile.FullName;
            int length = filePath.Length - dataInfo.FullName.Length + dataPath.Length;
            filePath = filePath.Substring(dataFile.FullName.Length - length, length);
            
            Dialogue dialogue = AssetDatabase.LoadAssetAtPath<Dialogue>(filePath);
            dialogue.ProcessData();
            EditorUtility.SetDirty(dialogue);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
