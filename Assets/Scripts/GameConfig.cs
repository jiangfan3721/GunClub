using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputConfig
{
    // Input Signal
    public const string INPUT_FIRE_SINGLE = "INPUT_FIRE_SINGLE"; //单发射击
    public const string INPUT_MAGAZINE_OPEN = "INPUT_MAGAZINE_OPEN"; // 开弹匣
    public const string INPUT_MAGAZINE_CLOSE = "INPUT_MAGAZINE_CLOSE"; // 关弹匣
    public const string INPUT_FILLED_BULLETS = "INPUT_FILLED_BULLETS"; // 填充子弹
}

public class GameEventConfig
{
    public const string EVENT_FIRE_SINGLE = "EVENT_FIRE_SINGLE";
}
