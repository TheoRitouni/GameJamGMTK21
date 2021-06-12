using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace utils
{
    public class TagList
    {
        private static string _groundTag = "Ground";
        private static string _enemyTag = "Enemy";

        public static string groundTag => _groundTag;
        public static string enemyTag => _enemyTag;
    }
}

