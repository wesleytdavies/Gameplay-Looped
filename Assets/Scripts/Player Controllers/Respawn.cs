using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
