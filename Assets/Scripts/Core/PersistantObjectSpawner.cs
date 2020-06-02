using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Core
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab;

        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned)
            {
                return;
            }
            SpawnPersistantObjects();
            hasSpawned = true;
        }

        private void SpawnPersistantObjects()
        {
            GameObject persistantObject = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(persistantObject);
        }
    }
}
