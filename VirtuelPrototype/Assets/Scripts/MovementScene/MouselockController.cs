using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouselockController : Controller
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

}
