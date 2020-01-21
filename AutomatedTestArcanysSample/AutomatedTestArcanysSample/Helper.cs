﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTestArcanysSample.HelperClass
{
    class Helper
    {


        public static string GetRandomString(int minLen, int maxLen)
        {
            char[] Alphabet = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcefghijklmnopqrstuvwxyz0123456789").ToCharArray();
            Random m_randomInstance = new Random();
            Object m_randLock = new object();

            int alphabetLength = Alphabet.Length;
            int stringLength;
            lock (m_randLock) { stringLength = m_randomInstance.Next(minLen, maxLen); }
            char[] str = new char[stringLength];

            // max length of the randomizer array is 5
            int randomizerLength = (stringLength > 5) ? 5 : stringLength;

            int[] rndInts = new int[randomizerLength];
            int[] rndIncrements = new int[randomizerLength];

            // Prepare a "randomizing" array
            for (int i = 0; i < randomizerLength; i++)
            {
                int rnd = m_randomInstance.Next(alphabetLength);
                rndInts[i] = rnd;
                rndIncrements[i] = rnd;
            }

            // Generate "random" string out of the alphabet used
            for (int i = 0; i < stringLength; i++)
            {
                int indexRnd = i % randomizerLength;
                int indexAlphabet = rndInts[indexRnd] % alphabetLength;
                str[i] = Alphabet[indexAlphabet];

                // Each rndInt "cycles" characters from the array,
                // so we have more or less random string as a result
                rndInts[indexRnd] += rndIncrements[indexRnd];
            }
            return (new string(str));
        }





    }
}
