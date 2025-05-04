using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicPlaylist; // Playlist âm nhạc (gán trong Inspector)
    private AudioSource audioSource; // AudioSource để phát nhạc

    void Start()
    {
        // Lấy AudioSource gắn vào đối tượng này
        audioSource = GetComponent<AudioSource>();

        // Kiểm tra xem có playlist không và phát nhạc ngẫu nhiên
        if (musicPlaylist.Length > 0)
        {
            PlayRandomMusic();
        }
        else
        {
            
        }
    }

    // Chức năng để phát nhạc ngẫu nhiên từ playlist
    void PlayRandomMusic()
    {
        // Chọn một bài nhạc ngẫu nhiên từ playlist
        int randomIndex = Random.Range(0, musicPlaylist.Length);

        // Đặt bài nhạc ngẫu nhiên vào AudioSource
        audioSource.clip = musicPlaylist[randomIndex];

        // Phát âm thanh
        audioSource.Play();

        // Lặp lại âm thanh (nếu bạn muốn nhạc phát liên tục)
        audioSource.loop = true;
    }
}
