using TMPro;
using UnityEngine;

public class InputFieldSubmit : MonoBehaviour
{
    public CheatScript cheatScript;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_InputField>().onSubmit.AddListener(ProcessSubmit);
    }


    public void ProcessSubmit(string input)
    {
        cheatScript.SubmitCheat();
    }
}
