using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis
{
    public const string MOUSE_X = "Mouse X";
    public const string MOUSE_Y = "Mouse Y";
}

public class AnimationTags
{
    public const string ZOOM_IN_ANIM = "ZoomIn";
    public const string ZOOM_OUT_ANIM = "ZoomOut";

    public const string SHOOT_TRIGGER = "Shoot";
    public const string AIM_PARAMETER = "Aim";

    public const string WALK_PARAMETER = "Walk";
    public const string RUN_PARAMETER = "Run";
    public const string ATTACK_TRIGGER = "Attack";
    public const string DEAD_TRIGGER = "Dead";
}

public class Tags
{
    public const string LOOK_ROOT = "Look Root";
    public const string ZOOM_CAMERA = "FP Camera";
    public const string CROSSHAIR = "Crosshair";
    public const string ARROW_TAG = "Arrow";

    public const string AXE_TAG = "Axe";

    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";

    public const string ENEMY_CANNIBAL_TAG = "Cannibal(Clone)";
    public const string REVOLVER_TAG = "2 - Revolver";
}

public class UITags
{
    public const string PLAYER_HEALTH_UI = "Player Health Foreground";
    public const string PLAYER_STAMINA_UI = "Player Stamina Foreground";
    public const string CANNIBAL_HEALTH_BACKGROUND = "Cannibal Health Background";
    public const string BOAR_HEALTH_BACKGROUND = "Boar Health Background";
    public const string CANNIBAL_HEALTH_ICON = "Cannibal Health Icon";
    public const string BOAR_HEALTH_ICON = "Boar Health Icon";
    public const string CANNIBAL_HEALTH_FOREGROUND = "Cannibal Health Foreground";
    public const string BOAR_HEALTH_FOREGROUND = "Boar Health Foreground";
}
