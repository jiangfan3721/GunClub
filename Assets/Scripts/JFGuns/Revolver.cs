using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JFGuns
{
    [RequireComponent(typeof(RevolverController))]
    public class Revolver : BaseGun
    {
        #region 成员变量

        private RevolverController revolverController;

        #endregion

        #region Unity函数
        protected sealed override void Awake()
        {
            base.Awake();

            revolverController = GetComponent<RevolverController>();
        }

        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
        }
        #endregion

        #region 核心功能函数

        protected sealed override void OnSingleFire(string id, ArrayList argv)
        {
            base.OnSingleFire(id, argv);

            // 开枪动画
            revolverController.SingleFire(isValidShoot);
        }

        protected sealed override void OnOpenMagazine(string id, ArrayList argv)
        {
            base.OnOpenMagazine(id, argv);

            // 开弹匣动画
            revolverController.OpenMagazine();
        }

        protected sealed override void OnCloseMagazine(string id, ArrayList argv)
        {
            base.OnCloseMagazine(id, argv);

            // 关弹匣动画
            revolverController.CloseMagazine();
        }


        #endregion

        #region 辅助函数
        protected sealed override void AddToDelegate()
        {
            base.AddToDelegate();
        }

        protected sealed override void RemoveFromDelegate()
        {
            base.RemoveFromDelegate();
        }
        #endregion
    }
}
