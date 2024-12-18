using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] public Button _browserONButton;
    [SerializeField] public Button _buyBuuton;
    [SerializeField] private GameObject _menuCard;
    [SerializeField] private GameObject _menyBuing;
    [SerializeField] private AudioClip clickSound; 

    private AudioSource audioSource; 

    private void Awake()
    {
        _browserONButton.onClick.AddListener(ShowMenuBuing);
        _buyBuuton.onClick.AddListener(ShowMenuCard);
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = clickSound; 
    }
    public void ShowMenuBuing()
    {
        audioSource.Play(); 
        if (GM.IsPlayingRoomCheckCard)
            _menyBuing.gameObject.SetActive(true);
    }
    private void ShowMenuCard()
    {
        audioSource.Play();
        if (GM.IsPlayingRoomCheckCard)
        {
            _menuCard.gameObject.SetActive(true);
            _menyBuing.gameObject.SetActive(false);
        }
    }
}