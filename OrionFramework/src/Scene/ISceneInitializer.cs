namespace OrionFramework.Scene;

public interface ISceneInitializer
{
    void Initialize(MapManager mapManager, EntityManager entityManager);
}