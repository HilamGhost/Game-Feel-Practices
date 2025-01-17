using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public class GameManager : Singleton <GameManager>
    {
        private void OnEnable()
        {
            HideCursor();
        }

        private void Update()
        {
            CheckCursorInput();
        }

        #region Cursor

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void CheckCursorInput()
        {
            if(Input.GetKeyDown(KeyCode.Escape)) ShowCursor(); 
            if(Input.GetMouseButtonDown(0)) HideCursor();
        }
        #endregion
    }
}
