using System.Threading.Tasks;
using UnityEngine;

public class control : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public KeyCode keyCodeTab;
    public TMPro.TextMeshProUGUI text;
    void Start()
    {
        
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKey(keyCodeTab))
        {
            text.text = "Ini buat Objective";
            await Task.Delay(5000);
            text.text = "";
        }
    }
}
