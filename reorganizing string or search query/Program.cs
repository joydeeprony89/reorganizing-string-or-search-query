using System;
using System.Collections.Generic;
using System.Linq;

namespace reorganizing_string_or_search_query
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "aaaabbbccd"; // aaaabbbccccdd // aaaabbbccd // "bbbbbbaab"
            Console.WriteLine(ReorganizeString(s));

            // input aaaabbbccd
            // first pass
            // a _ a _ a _ a _ _ _ , end of this pass, idx = 8
            // second pass
            // a b a b a _ a _ b _ , when idx > length, will check the first char != 'b', idx will reset to 1.  end of this pass idx = 5
            // third pass
            // a b a b a c a c b _ , end of this pass, idx = 9
            // fourth pass
            // a b a b a c a c b d
        }

        static string ReorganizeString(string s)
        {
            int length = s.Length;
            SortedDictionary<char, int> hash = new SortedDictionary<char, int>();
            foreach(char c in s)
            {
                if (!hash.ContainsKey(c))
                    hash.Add(c, 1);
                else
                    hash[c]++;
            }
            var newHash = hash.OrderByDescending(x => x.Value);
            if (newHash.First().Value > (s.Length + 1) / 2) return "";

            char[] answer = new char[length];
            int idx = 0;
            foreach (var kvp in newHash)
            {
                int max = kvp.Value;
                for (int i = 0; i < max; i++)
                {
                    if (idx < length)
                    {
                        answer[idx] = kvp.Key;
                        idx += 2;
                    }
                    else if (answer[0] != kvp.Key)
                    {
                        idx = 1;
                        answer[idx] = kvp.Key;
                        idx += 2;
                    }
                    else return "";
                }
            }

            return new string(answer);
        }
    }
}
