using UnityEngine.UI;
using UnityEngine;

public class CurrentGagsUI : MonoBehaviour
{
    public Text CounterText;
    // Update is called once per frame
    void Update()
    {
        CounterText.text = GameManager.Instance.PlayerResources.CurrentResources.ToString();
    }
}
