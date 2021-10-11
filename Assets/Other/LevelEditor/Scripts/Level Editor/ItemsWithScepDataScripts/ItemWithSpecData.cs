using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ItemWithSpecData : MonoBehaviour
    {
        public string[] specData;
        public string[] GetItemSpecData()
        {
            return specData;
        }
    }

}