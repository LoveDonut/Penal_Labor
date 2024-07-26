using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region PrivateVariables
    [SerializeField] int _power;
    [SerializeField] ParticleSystem _damagedFX;
    [SerializeField] GameObject _hitPoint;
    [SerializeField] CinemachineBlenderSettings _cinemachineSettings;

    CinemachineStateDrivenCamera _stateCamera;
    PlayerAttack _playerAttack;
    HitDetect _hitDetect;
    Animator _animator;

    #endregion

    #region PrivateMethods

    void Awake()
    {
        _stateCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _hitDetect = GetComponentInChildren<HitDetect>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _playerAttack.SetWeapon(this, _hitDetect);

        SetCameraByWeapon();
    }

    private void SetCameraByWeapon()
    {
        _stateCamera.m_AnimatedTarget = _animator;

        //int stateHash = Animator.StringToHash("Idle");

        //AddStateInstruction(stateHash, "Idle Camera", 0.5f, 0f);

        //stateHash = Animator.StringToHash("Attack");

        //AddStateInstruction(stateHash, "Attack Camera", 0.5f, 0f);
    }

    //private void AddStateInstruction(int stateHash, string virtualCameraName, float activateAfter, float minDuration)
    //{
    //    // 기존 지침을 복사하여 새로운 지침 배열 생성
    //    var instructions = new CinemachineStateDrivenCamera.Instruction[_stateCamera.m_Instructions.Length + 1];
    //    _stateCamera.m_Instructions.CopyTo(instructions, 0);

    //    // 새로운 상태 지침 설정
    //    var newInstruction = new CinemachineStateDrivenCamera.Instruction();
    //    newInstruction.m_FullHash = stateHash;
    //    newInstruction.m_VirtualCamera = FindVirtualCamera(virtualCameraName);
    //    newInstruction.m_ActivateAfter = activateAfter;
    //    newInstruction.m_MinDuration = minDuration;

    //    // 새로운 지침을 배열에 추가
    //    instructions[instructions.Length - 1] = newInstruction;
    //    _stateCamera.m_Instructions = instructions;
    //}

    //// 이름으로 가상 카메라를 찾는 함수
    //private CinemachineVirtualCamera FindVirtualCamera(string cameraName)
    //{
    //    CinemachineVirtualCamera[] virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
    //    foreach (var vcam in virtualCameras)
    //    {
    //        if (vcam.name == cameraName)
    //        {
    //            return vcam;
    //        }
    //    }

    //    Debug.LogError("Virtual Camera not found.");
    //    return null;
    //}

    void Update()
    {
    }
    #endregion


    #region PublicMethods
    public int GetPower() { return _power; }
    public ParticleSystem GetDamagedFX() { return _damagedFX; }
    public Transform GetHitPoint() { return _hitPoint.transform; }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    #endregion
}
