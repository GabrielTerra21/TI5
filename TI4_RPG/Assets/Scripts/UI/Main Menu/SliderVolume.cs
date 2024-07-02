using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Audio")]
    public Slider volumeSlider;
    public AudioSource audioSource;

    [Header("XML Settings")] 
    public string folderName = "./saves/";
    public string fileName = "settings";

    void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        LoadSettings();
    }

    void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }

    private void OnEnable(){
        LoadSettings();
    }
    
    private void OnDisable() {
        SaveSettings();
    }

    private void SaveSettings() {
        if (!Directory.Exists(folderName)) { Directory.CreateDirectory(folderName); }

        DirectoryInfo dir = new DirectoryInfo(folderName);
        StreamWriter writer = new StreamWriter(folderName + fileName + ".xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        Settings settings = new Settings(volumeSlider.value);
        serializer.Serialize(writer.BaseStream, settings);
        writer.Close();
        Debug.Log("Settings have been saved");
    }

    private void LoadSettings() {
        if (!File.Exists(folderName + fileName + ".xml")) return;
        StreamReader reader = new StreamReader(folderName + fileName + ".xml");
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
        Settings loaded = (Settings)serializer.Deserialize(reader.BaseStream);
        audioSource.volume = loaded.volume;
        volumeSlider.value = loaded.volume;
        reader.Close();
        Debug.Log("Loaded Player Settings");
    }
    
    public struct Settings {
        public float volume;

        public Settings(float audioVolume) {
            volume = audioVolume;
        }
    }
    
}
