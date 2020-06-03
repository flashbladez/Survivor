using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Saving;

namespace Survivor.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "SavedFile";
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        void Load()
        {
            //call to savingsystem load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }              
    }
}