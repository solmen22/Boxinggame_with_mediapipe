using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }
    public void QuitGame()
    {
        
        // ÀÛ‚ÌƒQ[ƒ€I—¹ˆ—
        Application.Quit();

    }
}
