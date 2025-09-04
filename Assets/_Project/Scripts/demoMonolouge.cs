using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class demoMonolouge : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await Task.Delay(2000);
        dialogueText.text = "Hah?";
        await Task.Delay(3000);
        dialogueText.text = "Siapa di sana?";
        await Task.Delay(7000);
        dialogueText.text = "Mimpi?";
        await Task.Delay(5000);
        dialogueText.text = "Lemari!";
        await Task.Delay(10000);
        dialogueText.text = "";
    }

    public TextMeshProUGUI dialogueText;


    // Update is called once per frame
    void Update()
    {

    }
}
