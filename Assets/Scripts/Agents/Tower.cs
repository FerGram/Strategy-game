using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    enum TowerType { TowerSmall, TowerKing }

    [SerializeField] TowerType _towerType;

    private void OnDestroy()
    {
        switch (_towerType)
        {
            case TowerType.TowerSmall:

                //Give point
                break;

            case TowerType.TowerKing:

                //GameOver
                break;
        }
            
    }
}


