using UnityEngine;
using UnityEngine.U2D;
using Cinemachine;
using System.Reflection;


// Made by erineccleston
/// https://github.com/Unity-Technologies/2d-pixel-perfect/issues/2
/// https://gist.github.com/erineccleston/97a91f8fbefe90e45e0e42c1bc97b421

/// <summary>
/// Add this component to a camera that has PixelPerfectCamera and CinemachineBrain
/// components to prevent the active CinemachineVirtualCamera from overwriting the
/// correct orthographic size calculated by the PixelPerfectCamera.
/// </summary>
[RequireComponent(typeof(PixelPerfectCamera), typeof(CinemachineBrain))]
class CineMachinePixelPerfectFix : MonoBehaviour
{
    CinemachineBrain CB;
    object Internal; // PixelPerfectCameraInternal
    FieldInfo OrthoInfo;

    void Start()
    {
        CB = GetComponent<CinemachineBrain>();
        Internal = typeof(PixelPerfectCamera).GetField("m_Internal", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(GetComponent<PixelPerfectCamera>());
        OrthoInfo = Internal.GetType().GetField("orthoSize", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    void LateUpdate()
    {
        if (CB.ActiveVirtualCamera != null)
            (CB.ActiveVirtualCamera as CinemachineVirtualCamera).m_Lens.OrthographicSize = (float)OrthoInfo.GetValue(Internal);
    }
}