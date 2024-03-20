using UnityEngine;
using DesignPatterns;
using Cinemachine;
using Player;

namespace Managers
{
    public class CameraManager : PersistentSingleton<CameraManager>
    {
        [SerializeField] private CinemachineVirtualCamera _neutralVCam;
        [SerializeField] private CinemachineVirtualCamera _aimVCam;

        public void UpdateVCams(GameObject newTarget)
        {
            _neutralVCam.Follow = newTarget.transform;
            _neutralVCam.LookAt = newTarget.transform;
            _aimVCam.Follow = newTarget.transform;
            _aimVCam.enabled = false;
        }

    }
}