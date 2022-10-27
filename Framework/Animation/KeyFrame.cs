using System;
namespace OrionFramework;

public class KeyFrame
{
    public int Frame;

    public Action? FrameEvent;

    public KeyFrame(int frame, Action? frameEvent)
    {
        Frame = frame;
        FrameEvent = frameEvent;
    }
}