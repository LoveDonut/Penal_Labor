using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManagement : MonoBehaviour
{

    #region PrivateVariables
    #endregion

    #region PublicVariables
    [SerializeField] GameObject[] _indecators;
    [SerializeField] GameObject[] _notPoccesses;

    public GameObject[] _weapons;
    PlayerAttack _playerAttack;
    CinemachineStateDrivenCamera _stateCamera;
    #endregion

    #region PrivateMethods
    private int currentWeaponIndex = 0; // ���� ���õ� ������ �ε���
    private bool[] weaponExists = new bool[4]; // ���� ���� ���� �迭

    void Awake()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        _stateCamera = FindObjectOfType<CinemachineStateDrivenCamera>();
    }

    void Start()
    {
        weaponExists[currentWeaponIndex] = true;
        SelectWeapon(currentWeaponIndex);
        UpdateWeaponUI();
    }

    void Update()
    {
        HandleWeaponSelection();
    }

    void HandleWeaponSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectWeapon(3);
    }

    void SelectWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length) return;
        if (!weaponExists[index]) return;

        // ��� ���⸦ ��Ȱ��ȭ
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].SetActive(false);
            _indecators[i].SetActive(false);
        }

        // activate weapon
        _weapons[index].SetActive(true);

        // activate ui
        _indecators[index].SetActive(true);

        // set weapon to player
        _playerAttack.SetWeapon(_weapons[index].GetComponent<Weapon>(), _weapons[index].GetComponentInChildren<HitDetect>());

        // change object of camera
        _stateCamera.m_AnimatedTarget = _weapons[index].GetComponent<Animator>();

        currentWeaponIndex = index;
    }
    void UpdateWeaponUI()
    {
        for (int i = 0; i < _notPoccesses.Length; i++)
        {
            if (weaponExists[i])
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

        weaponExists[index] = true;
        UpdateWeaponUI();
    }

    public void LoseWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Length) return;

        weaponExists[index] = false;

        // ���� ���õ� ���Ⱑ �Ҿ���� ������, �ٸ� ���⸦ �����ϰų� -1�� �ʱ�ȭ
        if (currentWeaponIndex == index)
        {
            currentWeaponIndex = -1;
            SelectWeapon(0);
        }

        UpdateWeaponUI();
    }
    #endregion

}
