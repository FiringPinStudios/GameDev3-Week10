﻿using UnityEngine;

namespace GameDevWithMarco.Managers
{
    public class scp_Shredder : MonoBehaviour
    {
        /// <summary>
        /// Just destroys anything that enters in the trigger
        /// </summary>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
