using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManagement : MonoBehaviour
{
    public enum EWeaponType
    {
        Hands = 0,
        Axe = 1,
        Pickax = 2,
        Drill = 3,
        King = 4
    }

    #region PrivateVariables
    [SerializeField] GameObject[] _indecators;
    [SerializeField] GameObject[] _notPoccesses;
    [SerializeField] GameObject _kingPictureStatus;

    PlayerAttack _playerAttack;
    CinemachineStateDrivenCamera _stateCamera;
    CinemachineVirtualCamera _attackCamera;

    EWeaponType _currentWeapon = EWeaponType.Hands; // 현재 선택된 무기의 인덱스


    #endregion

    #region PublicVariables
    public GameObject[] _weapons;
    public Dictionary<EWeaponType, bool> _weaponExists = new Dictionary<EWeaponType, bool>();
    #endregion

    #region PrivateMethods

    void Awake()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _stateCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
        _attackCamera = FindObjectOfType<FlipAttackCamera>().GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        for(EWeaponType type = EWeaponType.Hands; type <= EWeaponType.King; type++)
        {
            _weaponExists.Add(type, false);
        }
        _weaponExists[EWeaponType.Hands] = true;
        SelectWeapon(EWeaponType.Hands);
        UpdateWeaponUI();
    }

    void Update()
    {
        HandleWeaponSelection();
    }

    void HandleWeaponSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(EWeaponType.Hands);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(EWeaponType.Axe);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(EWeaponType.Pickax);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectWeapon(EWeaponType.Drill);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectWeapon(EWeaponType.King);
    }

    void SelectWeapon(EWeaponType weaponType)
    {
        if (!_weaponExists[weaponType]) return;

        // 모든 무기를 비활성화
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].SetActive(false);
            _indecators[i].SetActive(false);
        }

        // activate weapon
        _weapons[(int)weaponType].SetActive(true);

        // activate ui
        _indecators[(int)weaponType].SetActive(true);

        // set weapon to player
        _playerAttack.SetWeapon(_weapons[(int)weaponType].GetComponent<Weapon>(), _weapons[(int)weaponType].GetComponentInChildren<HitDetect>());

        // change object of camera
        _stateCamera.m_AnimatedTarget = _weapons[(int)weaponType].GetComponent<Animator>();

        // drill add shaking effect
        CinemachineBasicMultiChannelPerlin perlin = _attackCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (perlin != null)
        {
            if (weaponType == EWeaponType.Drill)
            {
                perlin.m_AmplitudeGain = 3.0f;
            }
            else
            {
                perlin.m_AmplitudeGain = 0f;
            }
        }
        else
        {
            Debug.Log("doesn't exist perlin!");
        }

        _currentWeapon = weaponType;
    }
    void UpdateWeaponUI()
    {
        for (int i = 0; i < _notPoccesses.Length; i++)
        {
            if (_weaponExists[(EWeaponType)i])
            {
                _notPoccesses[i].SetActive(false);
            }
            else
            {
                _notPoccesses[i].SetActive(true);
            }
        }
    }
    #endregion

    #region PublicMethods
    public void AcquireWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length) return;

        _weaponExists[(EWeaponType)index] = true;
        if(index == 4)
        {
            _kingPictureStatus.SetActive(true);
        }
        UpdateWeaponUI();
    }

    public void LoseWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length) return;

        _weaponExists[(EWeaponType)index] = false;

        // 현재 선택된 무기가 잃어버린 무기라면, 다른 무기를 선택하거나 -1로 초기화
        if (_currentWeapon == (EWeaponType)index)
        {
            _currentWeapon = 0;
            SelectWeapon(0);
        }

        UpdateWeaponUI();
    }
    #endregion

}
