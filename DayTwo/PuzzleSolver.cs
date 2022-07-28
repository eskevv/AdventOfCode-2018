namespace AoC2018.DayTwo;

/* --Inventory Management System: 
// PART 1:
// Count the number that an ID contains exactly two of any letter and then separately counting those with
// exactly three of any letter. Multiplying these together produces a checksum.
//
// PART 2:
// The boxes will have IDs which differ by exactly one character at the same position in both strings. 
// What letters are common between the two correct box IDs? 
*/

public class PuzzleSolver
{
   private int _checksum;
   private string? _commonBoxes;

   public void Solve()
   {
      // Parse puzzle
      string[] ids = File.ReadAllLines("DayTwo/puzzle.txt");

      // Part One
      _checksum = FindCheckSum(ids);

      // Part Two
      _commonBoxes = FindCommonCode(ids);
   }

   public int FindCheckSum(string[] ids)
   {
      int twoLetters = 0;
      int threeLetters = 0;

      foreach (var item in ids)
      {
         ReadID(item, ref twoLetters, ref threeLetters);
      }

      return twoLetters * threeLetters;
   }

   private void ReadID(string id, ref int twoLetters, ref int threeLetters)
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
         if (twoAppeared && threeAppeared) return;
      }

      twoLetters += twoAppeared ? 1 : 0;
      threeLetters += threeAppeared ? 1 : 0;
   }

   public string? FindCommonCode(string[] ids)
   {
      for (int x = 0; x < ids.Length; x++)
      {
         for (int y = x + 1; y < ids.Length; y++)
         {
            _commonBoxes = CompareIds(ids[x], ids[y]);
            if (_commonBoxes != null )return _commonBoxes;
         }
      }

      return null;
   }

   private string? CompareIds(string first, string second)
   {
      int length = Math.Max(first.Length, second.Length);
      string common = String.Empty;

      for (int x = 0; x < length; x++)
      {
         if (first[x] == second[x])
         {
            common += first[x];
         }
      }
      return common.Length == length - 1 ? common : null;
   }
}
