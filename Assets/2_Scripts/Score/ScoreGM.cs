using UnityEngine;

public class ScoreGM : MonoBehaviour {

    // Scores
    public int score_blue;
    public int score_red;

    public void TeamScored(PlayerStats.Team team)
    {
        Debug.Log(team + " scored!");
        switch (team)
        {
            case PlayerStats.Team.BLU:
                score_blue++;
                break;

            case PlayerStats.Team.RED:
                score_red++;
                break;

            default:
                Debug.Log("ScoreGM error : Scoring player team not expected.");
                break;

        }
    }
}
