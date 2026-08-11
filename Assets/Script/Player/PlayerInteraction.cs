using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("설정")]
    [SerializeField]    private Camera _mainCamera;
    [SerializeField]    private float _interactDistance;

    private void Awake()
    {
        if(_mainCamera == null)     _mainCamera = Camera.main;
        if(_interactDistance < 0f)   _interactDistance = 5f;
    }

    /// <summary>
    /// 나중에 New Input System 로직으로 바꿔야 함
    /// </summary>
    private void Update()
    {
        if(Input.GetMouseButton(0))     TryClickUIAtScreenCenter();
    }

    private void TryClickUIAtScreenCenter()
    {
        // 화면 중앙 좌표
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenCenter
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach(RaycastResult rs in results)
        {
            if(rs.distance > _interactDistance)     continue;

            Button b = rs.gameObject.GetComponentInParent<Button>();
            if(b != null && b.interactable)
            {
                b.onClick.Invoke();
                Debug.Log($"클릭 성공 | 버튼 이름 : {b.name}");
                break;
            }
        }
    }
}