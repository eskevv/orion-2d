using System;

namespace OrionFramework.Animation;

public class KeyFrame
{
    public int Frame;

    public Action Call;

    public KeyFrame(int frame, Action call)
    {
        Frame = frame;
        Call = call;
    }
}