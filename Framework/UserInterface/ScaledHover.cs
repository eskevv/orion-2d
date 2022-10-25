namespace DewInterface;

public class ScaledHover : HoverEffect
{
    // -- Properties / Fileds
    private float _scale;

    // -- Initialization

    public ScaledHover(float scale)
    {
        _scale = scale;
    }

    // -- Public Interface

    public void OnHover(ItemCell cell)
    {
        if (cell.Image is not null)
            cell.GrowToCenter(_scale);
    }

    public void OnReset(ItemCell cell)
    {
        if (cell.Image is not null)
            cell.ShrinkToCenter(_scale);
    }
}
