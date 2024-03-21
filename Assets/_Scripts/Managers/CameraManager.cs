using UnityEngine;
using DesignPatterns;
using Cinemachine;
using Player;
using Unity.VisualScripting;

namespace Managers
{
    public class CameraManager : PersistentSingleton<CameraManager>
    {
        [Header("Cinemachine Player Properties")]
        [SerializeField] private CinemachineVirtualCamera _neutralVCam;
        [SerializeField] private CinemachineVirtualCamera _aimVCam;

        [Header("Cinemachine Monster Properties")]
        [SerializeField] private CinemachineVirtualCamera[] _neutralVCams;

        private Hero _hero;

        private void Start()
        {
            _hero = Hero.Instance;
            //UpdateVCams(_hero.gameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Period))
            {
                Start();
            }
        }

        public void UpdateVCams(GameObject newTarget)
        {
            _neutralVCam.Follow = newTarget.transform.Find("FollowTarget");
            _neutralVCam.LookAt = newTarget.transform.Find("LookAtTarget");
            _aimVCam.Follow = newTarget.transform;
            _aimVCam.gameObject.SetActive(false);
            _neutralVCam.gameObject.SetActive(true);
        }
    }
}