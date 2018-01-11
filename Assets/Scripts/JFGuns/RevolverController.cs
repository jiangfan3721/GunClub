using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JFGuns
{
    public class RevolverController : MonoBehaviour
    {
        public Animator hammerAC;
        public Animator triggerAC;
        public Animator magazineHolderAC;
        public Animator revolverAC;

        public void SingleFire(bool isValidShoot)
        {
            if (isValidShoot)
            {
                revolverAC.SetTrigger("SingleFire");
            }
            hammerAC.SetTrigger("SingleFire");
            triggerAC.SetTrigger("SingleFire");
        }

        public void OpenMagazine()
        {
            magazineHolderAC.SetTrigger("OpenMagazine");
        }

        public void CloseMagazine()
        {
            magazineHolderAC.SetTrigger("CloseMagazine");
        }
    }
}
