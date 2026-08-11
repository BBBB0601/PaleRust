using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelterResourceUI : MonoBehaviour
{
    [Header("Shelter Reference")]
    [SerializeField] private ShelterZone _shelterZone;

    [Header("UI Component Settings")]
    [Tooltip("TextMeshProUGUI UI element to display resource list (Prioritized)")]
    [SerializeField] private TextMeshProUGUI _resourceTextTMP;

    [Tooltip("Legacy UnityEngine UI Text element fallback if TMP is not used")]
    [SerializeField] private Text _resourceTextLegacy;

    [Header("Display Settings")]
    [SerializeField] private string _headerText = "[ 쉘터 보관 자원 목록 ]\n";
    [SerializeField] private string _emptyStorageText = "보관된 자원이 없습니다.";

    private void Awake()
    {
        if (_shelterZone == null)
        {
            _shelterZone = FindFirstObjectByType<ShelterZone>();
        }

        // Try getting text components attached to the same GameObject if not assigned
        if (_resourceTextTMP == null && _resourceTextLegacy == null)
        {
            _resourceTextTMP = GetComponent<TextMeshProUGUI>();
            if (_resourceTextTMP == null)
            {
                _resourceTextLegacy = GetComponent<Text>();
            }
        }
    }

    private void OnEnable()
    {
        if (_shelterZone != null)
        {
            _shelterZone.OnStorageChanged += UpdateResourceUI;
        }
        UpdateResourceUI();
    }

    private void OnDisable()
    {
        if (_shelterZone != null)
        {
            _shelterZone.OnStorageChanged -= UpdateResourceUI;
        }
    }

    public void UpdateResourceUI()
    {
        if (_shelterZone == null) return;

        var storage = _shelterZone.ShelterStorage;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(_headerText);

        if (storage == null || storage.Count == 0)
        {
            sb.AppendLine(_emptyStorageText);
        }
        else
        {
            foreach (var pair in storage)
            {
                int mineralId = pair.Key;
                int amount = pair.Value;
                sb.AppendLine($"ID {mineralId}: {amount}");
            }
        }

        string resultText = sb.ToString();

        if (_resourceTextTMP != null)
        {
            _resourceTextTMP.text = resultText;
        }
        else if (_resourceTextLegacy != null)
        {
            _resourceTextLegacy.text = resultText;
        }
    }
}
