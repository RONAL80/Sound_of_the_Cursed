using UnityEngine;
using UnityEngine.UI;

public class sharon : MonoBehaviour
{
    public Text Nama_Player;
    public Text combo;

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerData();
    }

    // Fungsi yang dipanggil saat tombol Sharon diklik
    public void SharonButtonClicked()
    {
        Debug.Log("Tombol Sharon diklik!");

        string playerName = Nama_Player.text;
        // Mengambil nilai combo dari teks input dan mengonversinya ke integer
        int comboValue = int.Parse(combo.text);

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("ComboValue", comboValue);
        PlayerPrefs.Save(); // Simpan perubahan

        Debug.Log("Nama Pemain: " + playerName);
        Debug.Log("Combo: " + comboValue);
    }

    // Fungsi yang dipanggil saat tombol Full Combo Clear diklik
    public void FullComboButtonClicked()
    {
        Debug.Log("Tombol Full Combo Clear diklik!");

    }

    // Fungsi untuk memuat kembali data pemain dari PlayerPrefs
    private void LoadPlayerData()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        int comboValue = PlayerPrefs.GetInt("ComboValue", 4);

        // Menetapkan nilai yang dimuat kembali ke UI
        Nama_Player.text = playerName;
        combo.text = comboValue.ToString();
    }
}
