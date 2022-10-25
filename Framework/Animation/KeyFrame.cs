using System;
namespace DewInterface;

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