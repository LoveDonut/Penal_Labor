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
    //    // ���� ��ħ�� �����Ͽ� ���ο� ��ħ �迭 ����
    //    var instructions = new CinemachineStateDrivenCamera.Instruction[_stateCamera.m_Instructions.Length + 1];
    //    _stateCamera.m_Instructions.CopyTo(instructions, 0);

    //    // ���ο� ���� ��ħ ����
    //    var newInstruction = new CinemachineStateDrivenCamera.Instruction();
    //    newInstruction.m_FullHash = stateHash;
    //    newInstruction.m_VirtualCamera = FindVirtualCamera(virtualCameraName);
    //    newInstruction.m_ActivateAfter = activateAfter;
    //    newInstruction.m_MinDuration = minDuration;

    //    // ���ο� ��ħ�� �迭�� �߰�
    //    instructions[instructions.Length - 1] = newInstruction;
    //    _stateCamera.m_Instructions = instructions;
    //}

    //// �̸����� ���� ī�޶� ã�� �Լ�
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
