using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Text _deadText;
    [SerializeField] private GameObject _blackPanel;
    void Start()
    {
        StartCoroutine(StartSceneUi());
    }
    private IEnumerator StartSceneUi()
    {
        _blackPanel.SetActive(true);
        _blackPanel.GetComponent<Image>().DOFade(0, 2);
        yield return new WaitForSeconds(2);
        _blackPanel.SetActive(false);
    }
}
