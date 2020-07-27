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
        private static List<RomanNumberInstances> instances;

        private static void CreateInstances()
        {
            if (instances == null)
            {
                instances = new List<RomanNumberInstances>();
                RomanNumberInstances one = new RomanNumberInstances(1, 'I', null, null);
                instances.Add(one);
                RomanNumberInstances five = new RomanNumberInstances(5, 'V', one, one);
                instances.Add(five);
                RomanNumberInstances ten = new RomanNumberInstances(10, 'X', one, null);
                instances.Add(ten);
                RomanNumberInstances fifty = new RomanNumberInstances(50, 'L', ten, ten);
                instances.Add(fifty);
                RomanNumberInstances hundered = new RomanNumberInstances(100, 'C', ten, null);
                instances.Add(hundered);
                RomanNumberInstances fivehundered = new RomanNumberInstances(500, 'D', hundered, hundered);
                instances.Add(hundered);
                RomanNumberInstances thousand = new RomanNumberInstances(1000, 'M', hundered, null);
                instances.Add(thousand);
            }
        }

        internal static int ConvertToNumber(string RomanNumber)
        {
            if (instances == null)
            {
                CreateInstances();
            }
            char[] characters = RomanNumber.ToCharArray();
            RomanNumberInstances highestInstance = null;
            int totalNumber=0;
            for (int i = characters.Length - 1; i >= 0; i--)
            {
                bool found = false;
                foreach (RomanNumberInstances instance in instances)
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
            if (instances == null)
            {
                CreateInstances();
            }
            RomanNumberInstances before=null;
            RomanNumberInstances after=null;
            foreach(RomanNumberInstances instance in instances)
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
                foreach (RomanNumberInstances instance in instances)
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
                        foreach (RomanNumberInstances instance in instances)
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

        private class RomanNumberInstances
        {
            private int representesNumber;
            private char representedLetter;
            private RomanNumberInstances allowedBefore;
            private RomanNumberInstances allowedAfter;

            internal RomanNumberInstances(int number, char letter, RomanNumberInstances allowedBeforeNumber, RomanNumberInstances allowedAfterNumber)
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
            internal bool AllowedBefore(RomanNumberInstances number)
            {
                if (number == allowedBefore)
                {
                    return true;
                }
                return false;
            }
            internal bool AllowedAfter(RomanNumberInstances number)
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
