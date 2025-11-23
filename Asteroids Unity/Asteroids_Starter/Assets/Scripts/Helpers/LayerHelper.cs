using UnityEngine;

public static class LayerHelper
{
    public static bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }
}
