using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviewScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene() 
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }
}
