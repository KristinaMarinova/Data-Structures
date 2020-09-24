using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
           
            var prioritySweetness = new OrderedBag<int>();

            foreach (var sweetness in cookies)
            {
                prioritySweetness.Add(sweetness);
            }

            int currentCookie = prioritySweetness.GetFirst();
            int operations = 0;

            while (currentCookie < k && prioritySweetness.Count > 1)
            {
                int firstCookie = prioritySweetness.RemoveFirst();
                int secondCookie = prioritySweetness.RemoveFirst();
                int combinedCookie = firstCookie + (2 * secondCookie);
                prioritySweetness.Add(combinedCookie);
                currentCookie = prioritySweetness.GetFirst();
                operations++;
            }
            return currentCookie >= k ? operations : -1;
        }      
    }
}
