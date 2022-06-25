using UnityEngine;
using TMPro;

public class MessageBox: MonoBehaviour
{
    [SerializeField] private Animator errorAnimator;
    [SerializeField] private TextMeshProUGUI errorTextField;
    [SerializeField] private Animator warningAnimator;
    [SerializeField] private TextMeshProUGUI warningNameField;
    [SerializeField] private TextMeshProUGUI warningDescriptionField;

    public void ShowErrorMessage(string text)
    {
        errorTextField.text = text;
        errorAnimator.SetTrigger("ShowError");
    }

    public void ShowWarningMessage(string name, string description)
    {
        warningNameField.text = name;
        warningDescriptionField.text = description;
        warningAnimator.SetTrigger("ShowWarning");
    }
}
