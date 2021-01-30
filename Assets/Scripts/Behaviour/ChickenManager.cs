using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenManager : MonoBehaviour
{
    bool active;
    public Transform CurrentPlayerPosition;
    public List<ChickenAIBase> Chickens { get; private set; } = new List<ChickenAIBase>();
    public int ChickensCount => Chickens.Count;

    public System.Action<ChickenAIBase> ChickenRemovedEvent;
    public System.Action<ChickenAIBase> ChickenRegisteredEvent;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void RegisterChicken(ChickenAIBase chicken)
    {
        Chickens.Add(chicken);
        ChickenRegisteredEvent?.Invoke(chicken);
    }

    public void RemoveChicken(ChickenAIBase chicken)
    {
        Chickens.Remove(chicken);
        ChickenRemovedEvent?.Invoke(chicken);

        if (ChickensCount == 0)
            _gameController.EndGame(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !active;
            Time.timeScale = (active) ? 1f : 0;
            savePosition();

            SceneManager.LoadScene("MenuScene");
                }
        else { loadPosition();
            { }
        }

    }
    public void savePosition()
    {

        Transform CurrentPlayerPosition = this.gameObject.transform;

        PlayerPrefs.SetFloat("PosX", CurrentPlayerPosition.position.x); // т.к. автоматической работы
        PlayerPrefs.SetFloat("PosY", CurrentPlayerPosition.position.y); // с массивами нет, разбиваем на
        PlayerPrefs.SetFloat("PosZ", CurrentPlayerPosition.position.z);  // отдельные float и записываем

        PlayerPrefs.SetFloat("AngX", CurrentPlayerPosition.eulerAngles.x);
        PlayerPrefs.SetFloat("AngY", CurrentPlayerPosition.eulerAngles.y);

        PlayerPrefs.SetString("level", Application.loadedLevelName); // ещё можно писать/читать строки
        PlayerPrefs.SetInt("level_id", Application.loadedLevel); // и целые
    }

    public void loadPosition()
    {

        Transform CurrentPlayerPosition = this.gameObject.transform;

        Vector3 PlayerPosition = new Vector3(PlayerPrefs.GetFloat("PosX"),
                    PlayerPrefs.GetFloat("PosY"), PlayerPrefs.GetFloat("PosZ"));
        Vector3 PlayerDirection = new Vector3(PlayerPrefs.GetFloat("AngX"), // генерируем новые вектора
                    PlayerPrefs.GetFloat("AngY"), 0);  // на основе загруженных данных

        CurrentPlayerPosition.position = PlayerPosition; // и применяем их
        CurrentPlayerPosition.eulerAngles = PlayerDirection;
    }
}
