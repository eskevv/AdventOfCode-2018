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

      // Solve
      _checksum = FindCheckSum(ids);
      _commonBoxes = FindCommonCode(ids);
   }

   // Part One

   private int FindCheckSum(string[] ids)
   {
      int twoLetters = 0;
      int threeLetters = 0;

      foreach (var item in ids)
      {
         Dictionary<char, int> letterCounts = ReadID(item);
         twoLetters += letterCounts.ContainsValue(2) ? 1 : 0;
         threeLetters += letterCounts.ContainsValue(3) ? 1 : 0;
      }
      return twoLetters * threeLetters;
   }

   private Dictionary<char, int> ReadID(string id)
   {
      var letterCounts = new Dictionary<char, int>();
      foreach (var item in id)
      {
         bool firstAppearance = !letterCounts.ContainsKey(item);
         letterCounts[item] = firstAppearance ? 1 : letterCounts[item] + 1;
      }
      return letterCounts;
   }

   // Part Two

   private string? FindCommonCode(string[] ids)
   {
      for (int x = 0; x < ids.Length; x++)
      {
         for (int y = x + 1; y < ids.Length; y++)
         {
            _commonBoxes = CompareIds(ids[x], ids[y]);
            if (_commonBoxes != null) return _commonBoxes;
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