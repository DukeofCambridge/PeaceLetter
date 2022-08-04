using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightControl : MonoBehaviour
{
    private Light2D currentLight;
    private LightDetails currentLightDetails;

    private void Awake()
    {
        currentLight = GetComponent<Light2D>();
        currentLightDetails = new LightDetails();
        currentLightDetails.lightColor = new Color(1, 1, 1, 1);
        currentLightDetails.lightAmount = 1f;
        currentLightDetails.lightShift = LightShift.Morning;
    }
    /*private void OnEnable()
    {
        EventHandler.LightShiftChangeEvent += ChangeLightShift;
    }
    private void OnDisable()
    {
        EventHandler.LightShiftChangeEvent -= ChangeLightShift;
    }*/
    //实际切换灯光
    public void ChangeLightShift(LightShift lightShift, float timeDifference)
    {
        /*if (currentLightDetails.lightShift == lightShift)
        {
            return;
        }
        currentLightDetails.lightShift = lightShift;*/
        if (lightShift == LightShift.Morning)
        {
            currentLightDetails.lightColor = new Color(1,1,1,1);
            currentLightDetails.lightAmount = 1f;
        }
        else if(lightShift==LightShift.Evening)
        {
            currentLightDetails.lightColor = new Color(1f, 171f/255f, 116f/255f,1f);
            currentLightDetails.lightAmount = 0.8f;
        }
        else
        {
            currentLightDetails.lightColor = new Color(67f/255f, 85f/255f, 174f/255f,1f);
            currentLightDetails.lightAmount = 0.6f;
        }

        if (timeDifference < Common.lightChangeDuration)
        {
            var colorOffst = (currentLightDetails.lightColor - currentLight.color) / Common.lightChangeDuration * timeDifference;
            currentLight.color += colorOffst;
            DOTween.To(() => currentLight.color, c => currentLight.color = c, currentLightDetails.lightColor, Common.lightChangeDuration - timeDifference);
            DOTween.To(() => currentLight.intensity, i => currentLight.intensity = i, currentLightDetails.lightAmount, Common.lightChangeDuration - timeDifference);
        }
        if (timeDifference >= Common.lightChangeDuration)
        {
            currentLight.color = currentLightDetails.lightColor;
            currentLight.intensity = currentLightDetails.lightAmount;
        }
    }
}

[System.Serializable]
public class LightDetails
{
    public LightShift lightShift;
    public Color lightColor;
    public float lightAmount;
}
