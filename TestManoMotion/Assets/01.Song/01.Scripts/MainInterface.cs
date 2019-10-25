// 커맨드
using UnityEngine;

public class World : MonoBehaviour
{
    public virtual void InitWorld()
	{

	}
}

public class WorldInfo
// 모노해비어에 상속받지아니하고, 어디에서나 쓰일 수 있어 
{
    public World world;
    public bool isPortalOpened = false;

    public WorldInfo(World _world)
    {
        this.world = _world;
    }
    public string GetWorldName() { return world.name; }
}