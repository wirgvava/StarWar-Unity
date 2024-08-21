using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.SceneManagement;


public class TopScore
{
    public string Name { get; set; }
    public int Score { get; set; }
}

public class Firestore
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    public static List<TopScore> topScores = new List<TopScore>();
    private static readonly object lockObject = new object();  

    // Get top scores
    public async Task GetTopScores()
    {
        try
        {
            QuerySnapshot snapshot = await db.Collection("TopScores")
                                             .OrderByDescending("score")
                                             .Limit(100) 
                                             .GetSnapshotAsync();

            lock (lockObject)
            {
                topScores.Clear();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    topScores.Add(new TopScore
                    {
                        Name = document.Id,
                        Score = document.GetValue<int>("score")
                    });
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("ERROR at getting top scores: " + e);
        }
    }

    // Add score to leaderboard
    public async Task AddTopScore(string name, int score)
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "score", score }
        };

        try
        {
            await db.Collection("TopScores").Document(name).SetAsync(data);

            // Check if we need to remove scores after the 100th
            if (topScores.Count >= 100)
            {
                await RemoveScoresAfterHundred();
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("ERROR at adding top score: " + e);
        }
    }

    // Remove scores after the 100th
    private async Task RemoveScoresAfterHundred()
    {
        try
        {
            QuerySnapshot snapshot = await db.Collection("TopScores")
                                             .OrderByDescending("score")
                                             .GetSnapshotAsync();

            List<DocumentSnapshot> documents = snapshot.Documents.ToList();

            if (documents.Count > 100)
            {
                // Delete documents after the 100th
                for (int i = 100; i < documents.Count; i++)
                {
                    await db.Collection("TopScores")
                            .Document(documents[i].Id)
                            .DeleteAsync();
                }

                lock (lockObject)
                {
                    RewriteTopScores(documents.GetRange(0, 100));
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("ERROR at removing useless scores: " + e);
        }
    }

    // Rewrite the top scores
    private void RewriteTopScores(List<DocumentSnapshot> documents)
    {
        lock (lockObject)
        {
            topScores.Clear();

            foreach (DocumentSnapshot document in documents)
            {
                topScores.Add(new TopScore
                {
                    Name = document.Id,
                    Score = document.GetValue<int>("score")
                });
            }
        }
    }
}