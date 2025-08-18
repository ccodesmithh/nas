using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class monolouge : MonoBehaviour
{

    public NPCConversation conversation;

    private void startConversation()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }

    public void Start()
    {
        startConversation();
    }
}
