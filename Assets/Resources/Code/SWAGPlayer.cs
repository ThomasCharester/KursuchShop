using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Code
{
    public class SWAGPlayer: MonoBehaviour
{
    [Header("Настройки")]
    public List<AudioClip> tracks; // Список треков для воспроизведения
    public int repeatsPerTrack = 1; // Количество повторений каждого трека

    [Header("UI Elements")]
    public TMP_Text trackNameText;
    public Slider progressSlider;
    public Button playButton;
    public Button pauseButton;
    public Button nextButton;
    public Button prevButton;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private int currentRepeatCount = 0;
    private bool isPaused = false;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        
        audioSource.volume = 0.3f;
        
        // Настройка кнопок
        playButton.onClick.AddListener(Play);
        pauseButton.onClick.AddListener(Pause);
        nextButton.onClick.AddListener(NextTrack);
        prevButton.onClick.AddListener(PreviousTrack);
        
        // Инициализация интерфейса
        UpdateUI();
            
        Play();
        
    }

    void Update()
    {
        // Обновление прогресса воспроизведения
        if (audioSource.clip != null && audioSource.isPlaying)
        {
            progressSlider.value = audioSource.time / audioSource.clip.length;
        }

        // Проверка окончания трека
        if (!audioSource.isPlaying && !isPaused && audioSource.clip != null)
        {
            HandleTrackEnd();
        }
    }

    void HandleTrackEnd()
    {
        currentRepeatCount++;
        
        if (currentRepeatCount < repeatsPerTrack)
        {
            // Повтор трека
            audioSource.Play();
        }
        else
        {
            // Переход к следующему треку
            NextTrack();
        }
    }

    void PlayTrack()
    {
        if (tracks.Count == 0) return;
        
        audioSource.clip = tracks[currentTrackIndex];
        audioSource.Play();
        isPaused = false;
        currentRepeatCount = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (tracks.Count == 0) return;
        
        trackNameText.text = $"{(currentTrackIndex + 1)}/{tracks.Count}: {tracks[currentTrackIndex].name}";
        progressSlider.value = 0;
    }

    public void Play()
    {
        if (audioSource.clip == null)
        {
            PlayTrack();
        }
        else
        {
            audioSource.UnPause();
            isPaused = false;
        }
    }

    public void Pause()
    {
        audioSource.Pause();
        isPaused = true;
    }

    public void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        PlayTrack();
    }

    public void PreviousTrack()
    {
        currentTrackIndex--;
        if (currentTrackIndex < 0) currentTrackIndex = tracks.Count - 1;
        PlayTrack();
    }
}
}