#define TEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JFGuns
{
    public enum GunState
    {
        NO_CONTROLL,
        READY,
        NO_BULLETS,
        MAGAZINE_OPEN,
        CONTINUOUS_FIRE
    }

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class BaseGun : MonoBehaviour
    {
#region 配置参数
        [Header("======Base Audio=====")]

        [Tooltip("单发射击音频")]
        public AudioClip singleFireAudioClip;
        [Tooltip("开弹匣音频")]
        public AudioClip openMagazineAudioClip;
        [Tooltip("关弹匣音频")]
        public AudioClip closeMagazineAudioClip;
        [Tooltip("空枪音频")]
        public AudioClip emptyFireAudioClip;
        [Tooltip("上弹音频")]
        public AudioClip filledBulletsAudioClip;

        [Header("======Base Shooting Params=====")]

        [Range(0.0f, 6.0f)]
        [Tooltip("单发射击时间间隔")]
        public float singleFireInterval;

        [Header("======Base Bullet Params=====")]
        [Tooltip("枪内最多容纳子弹数量")]
        public int maxBullets;
        [Tooltip("每次填充子弹数量")]
        public int fillBulletsEveryTime;

        [Header("======Base Effects=====")]
        [Tooltip("枪口火焰")]
        public GameObject fireFlashEffect;

        #endregion

        #region 成员变量
        protected AudioSource audioSource;
        protected Animator animator;

        protected GunState gunState;

        protected int remainBullets;
        protected bool isValidShoot = true;
#endregion

#region Unity函数
        protected virtual void Awake()
        {
            AddToDelegate();

            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();

            gunState = GunState.NO_CONTROLL;

#if TEST
            gunState = GunState.NO_BULLETS;
#endif
        }

        protected virtual void OnEnable()
        {
            fireFlashEffect.SetActive(false);
        }

        protected virtual void OnDestroy()
        {
            RemoveFromDelegate();
        }
        #endregion

#region 核心功能函数

        // 单发射击
        protected virtual void OnSingleFire(string id, ArrayList argv)
        {
            if (gunState == GunState.READY)
            {
                // 播放射击音效
                PlayAudio(singleFireAudioClip);

                // 扣除子弹数量
                remainBullets--;
                if (remainBullets <= 0)
                {
                    gunState = GunState.NO_BULLETS;
                }

                // 枪口火焰
                fireFlashEffect.SetActive(true);
                StartCoroutine(HideFireFlushEffect());

                isValidShoot = true;
            }
            else if (gunState == GunState.NO_BULLETS)
            {
                EmptyFire();
            } 
        }

        // 开弹匣
        protected virtual void OnOpenMagazine(string id, ArrayList argv)
        {
            if (gunState == GunState.READY || gunState == GunState.NO_BULLETS)
            {
                // 播放开弹匣音效
                PlayAudio(openMagazineAudioClip);

                gunState = GunState.MAGAZINE_OPEN;
            }
        }

        // 关弹匣
        protected virtual void OnCloseMagazine(string id, ArrayList argv)
        {
            if (gunState == GunState.MAGAZINE_OPEN)
            {
                // 播放关弹匣音效
                PlayAudio(closeMagazineAudioClip);

                if (remainBullets > 0)
                {
                    gunState = GunState.READY;
                }
                else
                {
                    gunState = GunState.NO_BULLETS;
                }
            }

        }

        // 填充子弹
        protected virtual void OnFillBullets(string id, ArrayList argv)
        {
            if (gunState == GunState.MAGAZINE_OPEN)
            {
                if (remainBullets >= maxBullets) return;

                // 播放上弹音效
                PlayAudio(filledBulletsAudioClip);

                remainBullets = maxBullets;
            }
        }

        // 空枪处理
        protected virtual void EmptyFire()
        {
            if (gunState == GunState.NO_BULLETS)
            {
                PlayAudio(emptyFireAudioClip);
                isValidShoot = false;
            }
        }


        #endregion

        #region 辅助函数
        protected virtual void AddToDelegate()
        {
            if (DispatcherMachineComponent.Instance == null)
                return;

            DispatcherMachineComponent.Instance.DispatcherMachine.AddHandler(GameInputConfig.INPUT_FIRE_SINGLE, OnSingleFire);
            DispatcherMachineComponent.Instance.DispatcherMachine.AddHandler(GameInputConfig.INPUT_MAGAZINE_OPEN, OnOpenMagazine);
            DispatcherMachineComponent.Instance.DispatcherMachine.AddHandler(GameInputConfig.INPUT_MAGAZINE_CLOSE, OnCloseMagazine);
            DispatcherMachineComponent.Instance.DispatcherMachine.AddHandler(GameInputConfig.INPUT_FILLED_BULLETS, OnFillBullets);
        }

        protected virtual void RemoveFromDelegate()
        {
            if (DispatcherMachineComponent.Instance == null)
                return;

            DispatcherMachineComponent.Instance.DispatcherMachine.RemoveHandler(GameInputConfig.INPUT_FIRE_SINGLE, OnSingleFire);
            DispatcherMachineComponent.Instance.DispatcherMachine.RemoveHandler(GameInputConfig.INPUT_MAGAZINE_OPEN, OnOpenMagazine);
            DispatcherMachineComponent.Instance.DispatcherMachine.RemoveHandler(GameInputConfig.INPUT_MAGAZINE_CLOSE, OnCloseMagazine);
            DispatcherMachineComponent.Instance.DispatcherMachine.RemoveHandler(GameInputConfig.INPUT_FILLED_BULLETS, OnFillBullets);
        }

        protected void PlayAudio(AudioClip audio)
        {
            audioSource.Stop();
            if (audio != null)
            {
                audioSource.clip = audio;
                audioSource.Play();
            }
        }

        protected IEnumerator HideFireFlushEffect()
        {
            yield return new WaitForSeconds(0.05f);
            fireFlashEffect.SetActive(false);
        }
#endregion

    }
}
