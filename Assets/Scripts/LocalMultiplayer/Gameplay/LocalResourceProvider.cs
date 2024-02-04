using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Local Multiplayer Resource Provider", menuName = "Resource Providers/Local Multiplayer")]
public class LocalResourceProvider : BaseResourceProvider
{
    public override BaseTimer GetTimer()
    {
        return new LocalTimer();
    }

}
