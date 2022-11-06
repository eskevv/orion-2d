using System;

namespace OrionFramework;

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