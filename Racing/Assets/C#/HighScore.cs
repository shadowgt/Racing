using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

public class HighScore  {

    public string Score { get; set; }
    public string Name { get; set; }
    public string Time { get; set; }
    public bool isNew { get; set; }
    public int Rank { get; set; }

    public HighScore(string i_Score, string i_Name , string i_Time , int i_Rank =0, bool i_isNew = false)
    {
        Score = i_Score;
        Name = i_Name;
        Time = i_Time;
        Rank = i_Rank;
        isNew = i_isNew;
    }
}
