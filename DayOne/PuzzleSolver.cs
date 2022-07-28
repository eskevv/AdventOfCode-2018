namespace AoC2018.DayOne;

/* --Chronal Calibration: 
// PART 1:
// Starting with a frequency of zero, what is the resulting frequency after all of the changes in frequency have been applied?
//
// PART 2:
// What is the first frequency your device reaches twice?
*/

public class PuzzleSolver : ISolver
{
   private int _frequencyTotal;
   private int? _firstReachedTwice;

   public void Solve()
   {
      string[] parsedData = File.ReadAllLines("DayOne/puzzle.txt");
      int[] frequencies = parsedData.ToList().Select(x => int.Parse(x)).ToArray();

      _frequencyTotal = FindTotalFrequency(frequencies);
      _firstReachedTwice = FindFrequencyDuplicate(frequencies, 999999999);
   }

   // Part One

   private int FindTotalFrequency(int[] frequencies)
   {
      int total = 0;
      Array.ForEach(frequencies, x => { total += x; });
      return total;
   }

   // Part Two

   private int? FindFrequencyDuplicate(int[] frequencies, int maxIterations)
   {
      var results = new HashSet<int>() { 0 };
      int total = 0;

      int currentIterations = 0;
      while (true)
      {
         foreach (var item in frequencies)
         {
            total += item;
            if (!results.Add(total)) return total;
         }

         if (++currentIterations >= maxIterations) return null;
      }
   }
}