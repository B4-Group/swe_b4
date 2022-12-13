using UnityEngine;

[System.Serializable]
public class PlayerData {
    
    public int currentLevel;
    public int[] stars, hearts;
    public float[] time;

    private int numberOfLevels = 3;

    public PlayerData(int stars, float time, int hearts, int level)
    {
        this.stars = new int[numberOfLevels];
        this.time = new float[numberOfLevels];
        this.hearts = new int[numberOfLevels];

        this.currentLevel = level;
        this.stars[currentLevel] = stars;
        this.time[currentLevel] = time;
        this.hearts[currentLevel] = hearts;
    }


    public string SaveToString() {
        return JsonUtility.ToJson(this, true);
    }

}