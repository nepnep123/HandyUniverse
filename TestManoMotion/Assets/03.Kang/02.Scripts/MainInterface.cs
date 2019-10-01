// 커맨드
using UnityEngine;

public class World : MonoBehaviour
{
    public string worldName;
}

public class WorldInfo
// 모노해비어에 상속받지아니하고, 어디에서나 쓰일 수 있어 
{
    private World world;

    public WorldInfo(World _world)
    {
        this.world = _world;
    }
    // _world : 


    public void CreatePortal(Vector3 pos)
    {
        GameObject.Instantiate(world, pos, Quaternion.identity);
    }

    public string GetWorldName() { return world.worldName; }
}