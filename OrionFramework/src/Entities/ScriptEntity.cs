using OrionFramework.Entities;

public class ScriptEntity : Entity
{
    public override void Draw()
    {
    }

    public override void Update()
    {
       ScriptUpdate();
    }

    protected virtual void ScriptUpdate()
    {

    }
}