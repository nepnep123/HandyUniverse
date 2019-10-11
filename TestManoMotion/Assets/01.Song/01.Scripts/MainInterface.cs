// 커맨드
using UnityEngine;

public class World : MonoBehaviour
{
    
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


    public void InToThePortalWorld(Vector3 pos)
    {
		if (!isPortalOpened)
		{
			var a = GameObject.Instantiate(world, pos, Quaternion.identity);
			isPortalOpened = true;

		}
    }

    public string GetWorldName() { return world.name; }
}