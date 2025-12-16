using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> scores = new List<TextMeshProUGUI>();

    private string publicLeaderboardKey =
        "baf914f1e5eaff199633e5929ebb7d3d88a73a6bf59068830528a67e01051633";

    private void Start()
    {
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) => 
        {
            GetLeaderBoard();
        }));
    }
}
