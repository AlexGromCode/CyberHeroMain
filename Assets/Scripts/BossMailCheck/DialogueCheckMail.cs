using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueCheckMail : MonoBehaviour
{
    public static bool _isDialogueFinised;

    [SerializeField] private string[] _linesText;
    [SerializeField] private float _speedText;
    [SerializeField] private Image[] _images;
    [SerializeField] private AudioClip[] _voiceLines;
    [SerializeField] private Button _skipButton;
    [SerializeField] private GameObject _dialogObject;
    [SerializeField] private Text _dialogText;
    [SerializeField] private Fade _fade;
    [SerializeField] private AudioSource _audioSource;

    private Image _backGroundDialogue;

    private int _index;
    private bool _isSkipTextButton;

    public void Awake()
    {
        _dialogObject = GameObject.FindGameObjectWithTag("DialogEvent");
        _backGroundDialogue = GameObject.FindGameObjectWithTag("BackGroundDialogue").GetComponent<Image>();
        _fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        _dialogText = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<Text>();
        _skipButton = GameObject.FindGameObjectWithTag("SkipText").GetComponent<Button>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _isDialogueFinised = false;
        _isSkipTextButton = true;
        _skipButton.gameObject.SetActive(true);
        _dialogText.text = string.Empty;
        StartDialog();
    }
    private void StartDialog()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }
    private void NextLine()
    {
        if (_index < _linesText.Length - 1)
        {
            _index++;
            _dialogText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            TimerBar.isStart = true;
            HideDialogue();
        }
    }
    public void SkipText()
    {
        if (_isSkipTextButton)
        {
            if (_dialogText.text == _linesText[_index])
            {
                _images[_index].gameObject.SetActive(false);
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                _dialogText.text = _linesText[_index];
                PlayVoiceLine();
            }
        }
    }
    IEnumerator TypeLine()
    {
        if (_index < _images.Length)
            _images[_index].gameObject.SetActive(true);

        PlayVoiceLine();

        foreach (var c in _linesText[_index].ToCharArray())
        {
            _dialogText.text += c;
            yield return new WaitForSeconds(_speedText);
        }
    }
    private void PlayVoiceLine()
    {
        if (_index < _voiceLines.Length && _voiceLines[_index] != null)
        {
            _audioSource.clip = _voiceLines[_index];
            _audioSource.Play();
        }
    }
    public void ShowLineExtra(int scene)
    {
        _isSkipTextButton = false;
        GM.IsPlayingRoomBossCheckMail = false;

        _dialogObject.SetActive(true);
        _skipButton.gameObject.SetActive(false);
        _backGroundDialogue.gameObject.SetActive(false);
        _dialogText.gameObject.SetActive(false);

        StartCoroutine(WaitAndFadeAndLoadScene(2f, scene));
    }
    private IEnumerator WaitAndFadeAndLoadScene(float waitTime, int scene)
    {
        yield return new WaitForSeconds(waitTime);
        _fade.FadeIn();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
    }
    public void HideDialogue()
    {
        GM.IsPlayingRoomBossCheckMail = true;
        _isDialogueFinised = true;

        foreach (var image in _images)
            image.gameObject.SetActive(false);
        _dialogObject.SetActive(false);
    }
    public void OnDialogue() => _dialogObject.SetActive(true);
}
