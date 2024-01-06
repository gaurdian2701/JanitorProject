using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LauncherExtension
{
    public static Transform GetShootPos(this GameObject obj)
    {
        return obj.transform.GetChild((int)LauncherChildren.ShootPosition);
    }

    public static Transform GetSuckPos(this GameObject obj)
    {
        return (obj.transform.GetChild((int)LauncherChildren.SuckPosition));
    }
}
