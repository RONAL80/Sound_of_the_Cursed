using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsManager : MonoBehaviour
{
    public Text Nama_Player;
    public Text combo;

    // Fungsi untuk menghapus semua data PlayerPrefs dan menampilkan pesan
    public static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs telah dihapus.");
    }
}

public class ClearDataButton : MonoBehaviour
{
    public Text statusText;

    public void ClearPlayerPrefs()
    {
        PlayerPrefsManager.DeleteAllPlayerPrefs();
        statusText.text = "Tidak ada nama";
    }
}
