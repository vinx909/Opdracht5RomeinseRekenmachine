using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opdracht5RomeinseRekenmachine
{
    class RomanNumberConverter
    {
        private static List<RomanNumberInstance> instances;

        private static void CreateInstances()
        {
            if (instances == null)
            {
                instances = new List<RomanNumberInstance>();
                RomanNumberInstance one = new RomanNumberInstance(1, 'I', null, null);
                instances.Add(one);
                RomanNumberInstance five = new RomanNumberInstance(5, 'V', one, one);
                instances.Add(five);
                RomanNumberInstance ten = new RomanNumberInstance(10, 'X', one, null);
                instances.Add(ten);
                RomanNumberInstance fifty = new RomanNumberInstance(50, 'L', ten, ten);
                instances.Add(fifty);
                RomanNumberInstance hundered = new RomanNumberInstance(100, 'C', ten, null);
                instances.Add(hundered);
                RomanNumberInstance fivehundered = new RomanNumberInstance(500, 'D', hundered, hundered);
                instances.Add(fivehundered);
                RomanNumberInstance thousand = new RomanNumberInstance(1000, 'M', hundered, null);
                instances.Add(thousand);
            }
        }

        internal static int ConvertToNumber(string RomanNumber)
        {
            CreateInstances();
            char[] characters = RomanNumber.ToCharArray();
            RomanNumberInstance highestInstance = null;
            int totalNumber=0;
            for (int i = characters.Length - 1; i >= 0; i--)
            {
                bool found = false;
                foreach (RomanNumberInstance instance in instances)
                {
                    if (instance.GetLetter().Equals(characters[i]))
                    {
                        found = true;
                        if (highestInstance == null)
                        {
                            highestInstance = instance;
                        }
                        else if (highestInstance.GetNumber() < instance.GetNumber())
                        {
                            highestInstance = instance;
                        }
                        if (highestInstance.GetNumber() > instance.GetNumber())
                        {
                            totalNumber -= instance.GetNumber();
                        }
                        else
                        {
                            totalNumber += instance.GetNumber();
                        }
                    }
                }
                if (found == false)
                {
                    throw new Exception("Character gevonden wat geen bekend romeins nummer is");
                }
            }
            return totalNumber;
        }

        internal static string ConvertToRoman(int numberToConvert)
        {
            List<int> splitInTens = new List<int>();
            int tensStep = 0;
            while (true)
            {
                int numberLeft = numberToConvert;
                foreach(int splitPart in splitInTens)
                {
                    numberLeft -= splitPart;
                }
                if (numberLeft <= 0)
                {
                    break;
                }
                splitInTens.Add(numberLeft % (int)Math.Pow(10, tensStep + 1));
                tensStep++;
            }
            string toReturn = "";
            for(int i = splitInTens.Count - 1; i >= 0; i--)
            {
                toReturn+= ConvertToRomanSingularNumber(splitInTens[i]);
            }
            return toReturn;
        }
        internal static string ConvertToRomanSingularNumber(int numberToConvert)
        {
            if (numberToConvert == 0)
            {
                return "";
            }
            CreateInstances();
            RomanNumberInstance before=null;
            RomanNumberInstance after=null;
            foreach(RomanNumberInstance instance in instances)
            {
                int instanceNumber = instance.GetNumber();
                if (numberToConvert == instanceNumber)
                {
                    return ""+instance.GetLetter();
                }
                if (numberToConvert > instanceNumber)
                {
                    if (before == null || before.GetNumber() < instanceNumber)
                    {
                        before = instance;
                    }
                }
                if (numberToConvert < instanceNumber)
                {
                    if (after == null || after.GetNumber() > instanceNumber)
                    {
                        after = instance;
                    }
                }
            }
            if (after != null) {
                foreach (RomanNumberInstance instance in instances)
                {
                    if (after.AllowedBefore(instance))
                    {
                        if (after.GetNumber() - instance.GetNumber() == numberToConvert)
                        {
                            return "" + instance.GetLetter() + after.GetLetter();
                        }
                    }
                }
            }
            if (before != null) {
                int amountOfBefores = 2;
                while (true)
                {
                    if (before.GetNumber() * amountOfBefores == numberToConvert)
                    {
                        string toReturn = "";
                        for(int i=0;i< amountOfBefores; i++)
                        {
                            toReturn += before.GetLetter();
                        }
                        return toReturn;
                    }
                    else if(before.GetNumber() * amountOfBefores > numberToConvert)
                    {
                        foreach (RomanNumberInstance instance in instances)
                        {
                            if (before.AllowedAfter(instance))
                            {
                                int amountOfAfters = 1;
                                while (true)
                                {
                                    if (before.GetNumber() + instance.GetNumber() * amountOfAfters == numberToConvert)
                                    {
                                        string toReturn = ""+ before.GetLetter();
                                        for (int i = 0; i < amountOfAfters; i++)
                                        {
                                            toReturn += instance.GetLetter();
                                        }
                                        return toReturn;
                                    }
                                    else if (before.GetNumber() + instance.GetNumber() * amountOfAfters > numberToConvert)
                                    {
                                        throw new NotImplementedException();
                                    }
                                    else
                                    {
                                        amountOfAfters++;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        amountOfBefores++;
                    }
                }
            }
            throw new NotImplementedException();
        }

        internal static char[] GetNumericals()
        {
            CreateInstances();
            char[] toReturn = new char[instances.Count];
            for(int i=0;i<toReturn.Length;i++)
            {
                toReturn[i] = instances[i].GetLetter();
            }
            return toReturn;
        }
        internal static bool IsRomanNumerical(char numerical)
        {
            CreateInstances();
            foreach(RomanNumberInstance instance in instances)
            {
                if (numerical == instance.GetLetter())
                {
                    return true;
                }
            }
            return false;
        }

        private class RomanNumberInstance
        {
            private int representesNumber;
            private char representedLetter;
            private RomanNumberInstance allowedBefore;
            private RomanNumberInstance allowedAfter;

            internal RomanNumberInstance(int number, char letter, RomanNumberInstance allowedBeforeNumber, RomanNumberInstance allowedAfterNumber)
            {
                this.representesNumber = number;
                this.representedLetter = letter;
                this.allowedBefore = allowedBeforeNumber;
                this.allowedAfter = allowedAfterNumber;
            }

            internal int GetNumber()
            {
                return representesNumber;
            }
            internal char GetLetter()
            {
                return representedLetter;
            }
            internal bool AllowedBefore(RomanNumberInstance number)
            {
                if (number == allowedBefore)
                {
                    return true;
                }
                return false;
            }
            internal bool AllowedAfter(RomanNumberInstance number)
            {
                if (number == allowedAfter)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
