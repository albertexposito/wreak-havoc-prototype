using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstantValues
{
    // GLOBAL SCENES
    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public const string QUALITY_SETTINGS_SCENE_NAME = "SettingsPanel";

    // LOCAL SCENE NAMES
    public const string LOCAL_CHARACTER_SELECTION_SCENE_NAME = "LocalCharacterSelection";
    public const string LOCAL_GAME_SETUP_SCENE_NAME = "LocalGameSetup";
    public const string LOCAL_GAME_RESULT_SCENE_NAME = "LocalResultScreen";

    // PHOTON ONLINE SCENE NAMES
    public const string PHOTON_LOBBY_SCENE_NAME = "OnlineLobby";
    public const string PHOTON_CHARACTER_SELECTION_SCENE_NAME = "OnlineCharacterSelection";
    public const string PHOTON_GAME_SCENE_NAME = "OnlineGameSetup";
    public const string PHOTON_GAME_RESULT_SCENE_NAME = "OnlineGameSetup";

    public const string LOBBY_DEFAULT_NAME = "DEFAULT_LOBBY";


    // Control Schemes
    public const string KEYBOARD_CONTROL_SCHEME = "Keyboard&Mouse";
    public const string GAMEPAD_CONTROL_SCHEME = "Gamepad";



    public const float SPAWN_TIME = 2;
    public const float RESPAWN_TIME = 1.5f; // Time to being respawned after dying

    public const int MAX_PLAYERS_PER_GAME = 4;
    public const int DEFAULT_LIVES = 3;

}
