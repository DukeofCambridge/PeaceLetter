using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public string description;
    public EffectType effect;
    public float value;
    public EffectValueType valueType;
    public float probability;

    public Effect(string description,EffectType effect,float value,EffectValueType valueType,float probability)
    {
        this.description = description;
        this.effect = effect;
        this.value = value;
        this.valueType = valueType;
        this.probability = probability;
    }
    public Effect(JsonData jsonData)
    {
        description = jsonData["description"].ToString();
        effect = (EffectType)int.Parse(jsonData["effect"].ToString());
        value = float.Parse(jsonData["value"].ToString());
        valueType = (EffectValueType)System.Enum.Parse(typeof(EffectValueType), jsonData["valueType"].ToString().ToUpper());
        probability = float.Parse(jsonData["probability"].ToString());
    }

    public double ApplyEffect(double value)
    {
        float rand = Random.Range(0f, 1f);
        if(rand <= probability)
        {
            switch (valueType)
            {
                case EffectValueType.ABSOLUTE:
                    return value += this.value;
                case EffectValueType.PERCENTAGE:
                    return value *= (1 + this.value);
                default:
                    Debug.Log("invalid valueType input");
                    return -1;
            }
        }//when rand is in (0,probability],execute effect
        else
        {
            return value;
        }
    }

    // this method is for the problems whitch add effect depending on some messenger attributes
    public double ApplyEffect(double value,int dependPoint)
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= probability)
        {
            switch (valueType)
            {
                case EffectValueType.ABSOLUTE:
                    return value += (this.value * dependPoint);
                case EffectValueType.PERCENTAGE:
                    return value *= (1 + (this.value * dependPoint));
                default:
                    Debug.Log("invalid valueType input");
                    return -1;
            }
        }//when rand is in (0,probability],execute effect
        else
        {
            return value;
        }
    }
}