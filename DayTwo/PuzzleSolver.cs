namespace AoC2018.DayTwo;

public class PuzzleSolver
{
   private int _twoLetters;
   private int _threeLetters;
   private int _checksum;

   public void Solve()
   {
      string[] ids = File.ReadAllLines("DayTwo/puzzle.txt");

      foreach (var item in ids)
      {
         ReadID(item);
      }

      _checksum = _twoLetters * _threeLetters;
   }

   private void ReadID(string id)
   {
      var letterCounts = new Dictionary<char, int>();
      bool twoAppeared = false;
      bool threeAppeared = false;

      foreach (var item in id)
      {
         bool firstAppearance = !letterCounts.ContainsKey(item);
         letterCounts[item] = firstAppearance ? 1 : letterCounts[item] + 1;
      }

      foreach (var item in letterCounts)
      {
         twoAppeared = twoAppeared == true ? true : item.Value == 2;
         threeAppeared = threeAppeared == true ? true : item.Value == 3;
      }

      _twoLetters += twoAppeared ? 1 : 0;
      _threeLetters += threeAppeared ? 1 : 0;
   }
}