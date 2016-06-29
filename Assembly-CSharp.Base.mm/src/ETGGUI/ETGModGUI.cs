﻿#pragma warning disable 0626
#pragma warning disable 0649

using UnityEngine;
using System.Collections.Generic;

public class ETGModGUI: MonoBehaviour {

    public enum MenuOpened {
        None,
        Loader,
        Logger,
        Console,
        Inspector
    };

    public static MenuOpened CurrentMenu;

    public static GameObject menuObj;
    private readonly static ETGModNullMenu nullMenu = new ETGModNullMenu();
    private static ETGModLoaderMenu loaderMenu;
    private static ETGModConsole consoleMenu;
    private static ETGModDebugLogMenu loggerMenu;
    private static ETGModInspector inspectorMenu;
    
    public static bool UseDamageIndicators = false;

    private static IETGModMenu currentMenuScript {
        get {
            switch (CurrentMenu) {
                case MenuOpened.Loader:
                    return loaderMenu;
                case MenuOpened.Console:
                    return consoleMenu;
                case MenuOpened.Logger:
                    return loggerMenu;
                case MenuOpened.Inspector:
                    return inspectorMenu;

            }
            return nullMenu;
        }
    }

    /// <summary>
    /// Creates a new object with this script on it.
    /// </summary>
    public static void Create() {
        if (menuObj!=null) {
            return;
        }
        menuObj=new GameObject();
        menuObj.name="ModLoaderMenu";
        menuObj.AddComponent<ETGModGUI>();
        DontDestroyOnLoad(menuObj);
    }

    public void Start() {
        loaderMenu=new ETGModLoaderMenu();
        loaderMenu.Start();

        consoleMenu=new ETGModConsole();
        consoleMenu.Start();

        loggerMenu=new ETGModDebugLogMenu();
        loggerMenu.Start();

        inspectorMenu=new ETGModInspector();
        inspectorMenu.Start();

        ETGDamageIndicatorGUI.Create();

    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            if (CurrentMenu==MenuOpened.Loader)
                CurrentMenu=MenuOpened.None;
            else
                CurrentMenu=MenuOpened.Loader;

            UpdatePlayerState();
        }

        if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.Slash) || Input.GetKeyDown(KeyCode.BackQuote)) {
            if (CurrentMenu==MenuOpened.Console)
                CurrentMenu=MenuOpened.None;
            else
                CurrentMenu=MenuOpened.Console;

            UpdatePlayerState();
        }

        if (Input.GetKeyDown(KeyCode.F3)) {
            if (CurrentMenu==MenuOpened.Logger)
                CurrentMenu=MenuOpened.None;
            else
                CurrentMenu=MenuOpened.Logger;

            UpdatePlayerState();
        }

        if (Input.GetKeyDown(KeyCode.F4)) {
            if (CurrentMenu==MenuOpened.Inspector)
                CurrentMenu=MenuOpened.None;
            else
                CurrentMenu=MenuOpened.Inspector;

            UpdatePlayerState();
        }


        currentMenuScript.Update();
    }

    public static void UpdatePlayerState() {
        if (GameManager.GameManager_0!=null&&GameManager.GameManager_0.PlayerController_1!=null) {
            bool set = CurrentMenu==MenuOpened.None;
            GameManager.GameManager_0.PlayerController_1.enabled=set;
            Camera.main.GetComponent<CameraController>().enabled=set;
        }
    }

    public void OnGUI() {

        //For seeing about UI stuff, ignore.
        //foreach (Transform t in mainControl.GetComponentsInChildren<Transform>())
        //GUILayout.Label(t.name);

        currentMenuScript.OnGUI();
    }

}

