using System;
using UnityEngine;

[Serializable]
public enum GameStates
{
    Play,
    GameOver,
}

[Serializable]
public enum PlugType
{
    In,
    Out,
}

[Serializable]
public enum ScreenStates
{
    Display,
    Mire,
}

[Serializable]
public enum PlugRole
{
    Screen,
    Changer
}

[Serializable]
public enum HeadType
{
    FirstHead,
    SecondHead
}

[Serializable]
public enum MessageChangerType
{
    Shape,
    Color
}

[Serializable]
public enum MessageColors
{
    Red,
    Green,
    Blue
}

[Serializable]
public enum MessageShapes
{
    Cube,
    Circle,
    Triangle
}

public class Utilities : MonoBehaviour
{
    public const string MenuName = "Menu";
    public const string CreditsName = "Credits";
}