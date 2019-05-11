using System;

[Serializable]
public enum PlugType
{
    In,
    Out,
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