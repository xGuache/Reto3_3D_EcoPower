﻿using System.Collections;
using Etra.StarterAssets.Abilities;
using Etra.StarterAssets.Combat;
using UnityEngine;

namespace Etra.StarterAssets.Interactables.Enemies
{
    public class FPSTurretFist : MonoBehaviour
    {
        public FPSTurret mainScript;
        bool cooldown;
        private void OnTriggerEnter(Collider other)
        {
            //If the player is hit, launch them back
            if (other.gameObject.CompareTag("Player"))
            {
                mainScript.launchPlayer(other.gameObject);
                if (other.GetComponentInChildren<ABILITY_Health>())
                {
                    other.GetComponentInChildren<ABILITY_Health>().Damage(mainScript.damage);
                }
            }

            //If a damageable thing is hit (like another puncher), deal damage to it
            if (!cooldown)
            {
                if (other.gameObject.TryGetComponent<HealthSystem>(out var isDamageableCheck))
                {
                    isDamageableCheck.Damage(1);
                    //Wait one second before applying damage again with that fist
                    StartCoroutine(friendlyFireCooldown());
                }
            }

        }


        IEnumerator friendlyFireCooldown()
        {
            cooldown = true;
            yield return new WaitForSeconds(1);
            cooldown = false;
        }

    }
}
