﻿using UnityEngine;

public class menuController : MonoBehaviour
{
    //--local refs
    private stateManager.editorModes _editorMode = stateManager.editorMode;
    private bool _playerIsLocomoting = stateManager.playerIsLocomoting;

    private void Start()
    {
        stateManager.editorModeEvent += updateEditorMode;
        stateManager.playerIsLocomotingEvent += updatePlayerIsLocomoting;
    }

    private void OnApplicationQuit()
    {
        stateManager.editorModeEvent -= updateEditorMode;
        stateManager.playerIsLocomotingEvent -= updatePlayerIsLocomoting;
    }

    void updateEditorMode(stateManager.editorModes value)
    {
        _editorMode = value;

        if (_editorMode == stateManager.editorModes.openMenuMode)
        {
            toggleMenuComponents(true);
        }
        else
        {
            toggleMenuComponents(false);
        }
    }

    void updatePlayerIsLocomoting(bool value)
    {
        _playerIsLocomoting = value;

        if (_playerIsLocomoting)
        {
            toggleMenuComponents(false);
        }
        else
        {
            toggleMenuComponents(true);
        }       
    }

    public void toggleMenuComponents(bool on)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(on);
        }

        if (on)
        {
            setMenuToFacePlayer();
        }
    }

    void setMenuToFacePlayer()
    {
        //--make menu face the HMD
        GameObject menu = gameObject;
        GameObject vrCam = init.vrCamera;

        Vector3 forwardAmount = vrCam.transform.position + (vrCam.transform.forward * 2.15f);
        Quaternion rotationAmount = vrCam.transform.rotation;

        menu.transform.position = new Vector3(forwardAmount.x, vrCam.transform.position.y - 1, forwardAmount.z);

        menu.transform.rotation = rotationAmount;
        menu.transform.localEulerAngles = new Vector3(0, menu.transform.localEulerAngles.y + 180, 0);
    }
}
