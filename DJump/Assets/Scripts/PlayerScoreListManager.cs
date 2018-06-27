using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreListManager : MonoBehaviour
{
    public GameObject _playerScoreEntryPrefab;

    void Start()
    {
        //var scores = new List<PlayerScore>()
        //{
        //    new PlayerScore("TESTE1", 30),
        //    new PlayerScore("TESTE2", 20),
        //    new PlayerScore("TESTE3", 10)
        //};

        var scores = SaveManager.Instance.PlayerScores;

        foreach (var playerScore in scores)
        {
            var playerScoreEntry = Instantiate(_playerScoreEntryPrefab);
            playerScoreEntry.transform.SetParent(transform);

            var playerNameText = playerScoreEntry.transform.Find("PlayerNameText").GetComponent<Text>();
            playerNameText.text = playerScore.Name;

            var playerScoreText = playerScoreEntry.transform.Find("PlayerScoreText").GetComponent<Text>();
            playerScoreText.text = playerScore.Score.ToString();
        }
    }
}
