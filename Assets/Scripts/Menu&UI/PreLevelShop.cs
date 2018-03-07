using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelShop : MonoBehaviour
{
    [SerializeField]
    public Text title;
    [SerializeField]
    public GameObject fade;

    public void LoadLevel()
    {
        SceneManager.LoadScene("Loading");
    }
}
