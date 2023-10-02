using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UiManager : MonoBehaviour
{
    [SerializeField] private Text _deadText;
    [SerializeField] private GameObject _blackPanel;
    private Text _bulletAmountText;


    void Start()
    {
        _bulletAmountText = GameObject.Find("Bullet Amount").GetComponent<Text>();

    }

    public void SetBulletsText(int value)
    {
        _bulletAmountText.text = value.ToString();
    }

    public IEnumerator DeadScreen()
    {
        _blackPanel.SetActive(true);
        _blackPanel.GetComponent<Image>().DOFade(1, 2);
        yield return new WaitForSeconds(1);
        _deadText.enabled = true;
        _deadText.DOFade(1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
