using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opdracht5RomeinseRekenmachine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string numbers = "";
            
            numbers += "I" + ": " + RomanNumberConverter.ConvertToNumber("I") + "\r\n";
            numbers += "II" + ": " + RomanNumberConverter.ConvertToNumber("II") + "\r\n";
            numbers += "III" + ": " + RomanNumberConverter.ConvertToNumber("III") + "\r\n";
            numbers += "IV" + ": " + RomanNumberConverter.ConvertToNumber("IV") + "\r\n";
            numbers += "V" + ": " + RomanNumberConverter.ConvertToNumber("V") + "\r\n";
            numbers += "VI" + ": " + RomanNumberConverter.ConvertToNumber("VI") + "\r\n";
            numbers += "VII" + ": " + RomanNumberConverter.ConvertToNumber("VII") + "\r\n";
            numbers += "VIII" + ": " + RomanNumberConverter.ConvertToNumber("VIII") + "\r\n";
            numbers += "IX" + ": " + RomanNumberConverter.ConvertToNumber("IX") + "\r\n";
            numbers += "X" + ": " + RomanNumberConverter.ConvertToNumber("X") + "\r\n";
            numbers += "XI" + ": " + RomanNumberConverter.ConvertToNumber("XI") + "\r\n";
            numbers += "XII" + ": " + RomanNumberConverter.ConvertToNumber("XII") + "\r\n";


            label1.Text = numbers;
        }
    }
}
