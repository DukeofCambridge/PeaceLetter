using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Dialogue : ScriptableObject
{
    public DStartCondition startConditions;
    public DMessage messages;
    public DAction actions;
    public DChoiceStart choices;
    public DChoiceNode choiceNodes;
    public DBranchStart branches;
    public DBranchNode branchNodes;

    private bool processed;

    public void Instantiate(string jsonInfo)
    {
        var dialogueJToken = JObject.Parse(jsonInfo)["drawflow"]?["Home"]?["data"];
        if (dialogueJToken == null)
        {
            Debug.LogError("Wrong json format");
        }

        // parse json data
        FragmentCreator fragmentCreator = new FragmentCreator(this, dialogueJToken);
        startConditions = new DStartCondition();
        messages = new DMessage();
        actions = new DAction();
        choices = new DChoiceStart();
        choiceNodes = new DChoiceNode();
        branches = new DBranchStart();
        branchNodes = new DBranchNode();
        foreach (var fragment in dialogueJToken)
        {
            if (fragment.First()["name"].Value<string>().Equals("start"))
            {
                fragmentCreator.CreateFragment(fragment.First());
            }
        }

        processed = false;
    }

    public void ProcessData()
    {
        if (processed)
        {
            return;
        }
        
        Debug.Log("hi");
        foreach (var sc in startConditions)
        {
            
        }

        processed = true;
    }
}

public class FragmentCreator
{
    public Dialogue dialogue;
    public JToken dialogueJToken;
    public FragmentCreator(Dialogue dialogue, JToken dialogueJToken)
    {
        this.dialogue = dialogue;
        this.dialogueJToken = dialogueJToken;
    }

    public Fragment CreateFragment(int fragmentID)
    {
        JToken targetToken = dialogueJToken[fragmentID.ToString()];
        return CreateFragment(targetToken);
    }
    
    public Fragment CreateFragment(JToken fragmentInfo)
    {
        string name = fragmentInfo["name"].Value<string>();
        int id = fragmentInfo["id"].Value<int>();
        switch (name)
        {
            case "start":
                StartCondition sc = new StartCondition();
                sc.targetNameStr = fragmentInfo["data"]["target"].Value<string>();
                sc.startDateStr = fragmentInfo["data"]["start"].Value<string>();
                sc.endDateStr = fragmentInfo["data"]["end"].Value<string>();
                sc.channelStr = fragmentInfo["data"]["channel"].Value<string>();
                sc.contentStr = fragmentInfo["data"]["content"].Value<string>();
                sc.nextFragment = GetNextFragment(fragmentInfo);
                sc.id = id;
                if (sc.nextFragment != null)
                {
                    sc.nextId = new int[] {sc.nextFragment.id};
                }
                dialogue.startConditions.Add(id, sc);
                return sc;
            case "dialog":
                if (dialogue.messages.ContainsKey(id))
                {
                    return dialogue.messages[id];
                }
                Message m = new Message();
                m.nameStr = fragmentInfo["data"]["name"].Value<string>();
                m.textStr = fragmentInfo["data"]["text"].Value<string>();
                m.nextFragment = GetNextFragment(fragmentInfo);
                m.id = id;
                if (m.nextFragment != null)
                {
                    m.nextId = new int[] {m.nextFragment.id};
                }
                dialogue.messages.Add(id, m);
                return m;
            case "action":
                if (dialogue.actions.ContainsKey(id))
                {
                    return dialogue.actions[id];
                }
                Action a = new Action();
                a.channelStr = fragmentInfo["data"]["channel"].Value<string>();
                a.contentStr = fragmentInfo["data"]["content"].Value<string>();
                a.nextFragment = GetNextFragment(fragmentInfo);
                a.id = id;
                if (a.nextFragment != null)
                {
                    a.nextId = new int[] {a.nextFragment.id};
                }
                dialogue.actions.Add(id, a);
                return a;
            case "branch-node":
                if (dialogue.branchNodes.ContainsKey(id))
                {
                    return dialogue.branchNodes[id];
                }
                BranchNode b = new BranchNode();
                b.conditionStr = fragmentInfo["data"]["condition"].Value<string>();
                b.nextFragment = GetNextFragment(fragmentInfo);
                b.id = id;
                if (b.nextFragment != null)
                {
                    b.nextId = new int[] {b.nextFragment.id};
                }
                dialogue.branchNodes.Add(id, b);
                return b;
            case "choice-node":
                if (dialogue.choiceNodes.ContainsKey(id))
                {
                    return dialogue.choiceNodes[id];
                }
                ChoiceNode c = new ChoiceNode();
                c.choiceStr = fragmentInfo["data"]["choice"].Value<string>();
                c.nextFragment = GetNextFragment(fragmentInfo);
                c.id = id;
                if (c.nextFragment != null)
                {
                    c.nextId = new int[] {c.nextFragment.id};
                }
                dialogue.choiceNodes.Add(id, c);
                return c;
            default:
                Debug.LogError("Non-Existing class " + name);
                return null;
        }
    }

    public Fragment GetNextFragment(JToken currentFragment)
    {
        List<JToken> nextIds = currentFragment["outputs"]["output_1"]["connections"].Children().ToList();
        int id = -currentFragment["id"].Value<int>();
        
        if (dialogue.choices.ContainsKey(id))
        {
            return dialogue.choices[id];
        }
        
        if (dialogue.branches.ContainsKey(id))
        {
            return dialogue.branches[id];
        }
        
        if (nextIds.Count == 0)
        {
            return null;
        }

        if (nextIds.Count == 1)
        {
            int i = nextIds[0]["node"].Value<int>();
            return CreateFragment(i);
        }
        
        string nextType = dialogueJToken[nextIds[0]["node"].Value<string>()]["name"].Value<string>();
        if (nextType.Equals("choice-node"))
        {
            ChoiceStart choiceStart = new ChoiceStart();
            choiceStart.id = id;
            List<int> nextIdNums = new List<int>();
            foreach (JToken token in nextIds)
            {
                int i = token["node"].Value<int>();
                choiceStart.followingNodes.Add((ChoiceNode) CreateFragment(i));
                nextIdNums.Add(i);
            }
            choiceStart.nextId = nextIdNums.ToArray();
            dialogue.choices.Add(id, choiceStart);
            return choiceStart;
        }

        if (nextType.Equals("branch-node"))
        {
            BranchStart branchStart = new BranchStart();
            branchStart.id = id;
            List<int> nextIdNums = new List<int>();
            foreach (JToken token in nextIds)
            {
                int i = token["node"].Value<int>();
                branchStart.followingNodes.Add((BranchNode) CreateFragment(i));
                nextIdNums.Add(i);
            }
            branchStart.nextId = nextIdNums.ToArray();
            dialogue.branches.Add(id, branchStart);
            return branchStart;
        }

        Debug.LogError("Problem with json file: it should only branch under a condition");
        return null;
    }
}

[Serializable]
public class Fragment
{
    public Fragment nextFragment;
    public int id;
    public int[] nextId;
}

[Serializable]
public class StartCondition : Fragment
{
    // json raw data
    public string targetNameStr;
    public string startDateStr;
    public string endDateStr;
    public string channelStr;
    public string contentStr;

    public Date startDate;
    public Date endDate;
    public Condition condition;
    public StartCondition()
    {
        
    }
}

[Serializable]
public class Message : Fragment
{
    public string nameStr;
    public string textStr;

    public Message()
    {
        
    }
}

[Serializable]
public class Action : Fragment
{
    public string channelStr;
    public string contentStr;
}

[Serializable]
public class ChoiceStart : Fragment
{
    public List<ChoiceNode> followingNodes;

    public ChoiceStart()
    {
        followingNodes = new List<ChoiceNode>();
    }
}

[Serializable]
public class ChoiceNode : Fragment
{
    public string choiceStr;
}

[Serializable]
public class BranchStart : Fragment
{
    public List<BranchNode> followingNodes;

    public BranchStart()
    {
        followingNodes = new List<BranchNode>();
    }
}

[Serializable]
public class BranchNode : Fragment
{
    public string conditionStr;
}

[Serializable]
public class Condition
{
    
}

[Serializable] public class DStartCondition : SerializableDictionary<int, StartCondition> {}
[Serializable] public class DMessage : SerializableDictionary<int, Message> {}
[Serializable] public class DAction : SerializableDictionary<int, Action> {}
[Serializable] public class DChoiceStart : SerializableDictionary<int, ChoiceStart> {}
[Serializable] public class DChoiceNode : SerializableDictionary<int, ChoiceNode> {}
[Serializable] public class DBranchStart : SerializableDictionary<int, BranchStart> {}
[Serializable] public class DBranchNode : SerializableDictionary<int, BranchNode> {}