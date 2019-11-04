using UnityEngine;

[CreateAssetMenu(menuName = "MyAssets/GestureSoundPack")]
public class GestrueSound_Scriptable : ScriptableObject
{
    public AudioClip grab;
    public AudioClip pick;
    public AudioClip release;
    public AudioClip click;
    public AudioClip drop;
}
