using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace TransCode
{
    public partial class Form1 : Form
    {
        XmlDocument xmldoc;

        public Form1()
        {
            InitializeComponent();
            xmldoc = new XmlDocument();
            xmldoc.Load("codepages.xml");
            foreach (XmlNode nd in xmldoc)
            {
                if (nd.Name == "codepage")
                {
                    comboBox1.Items.Add(nd.Attributes[0].Value);
                }
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName) == true)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    StreamWriter sw = new StreamWriter("a.htm");

                    while (!sr.EndOfStream)
                    {
                        string Line = sr.ReadLine();
                        foreach (XmlNode nd in xmldoc.ChildNodes[1])
                        {
                            if (nd.Name == "rule")
                            {
                                string Reg = nd.Attributes[0].Value.ToString();
                                Reg = Reg.Replace ("\\u", "\\u");
                                Regex r = new Regex(Reg);
                                Line = r.Replace(Line, nd.InnerText);
                                sw.WriteLine(Line);
                            }
                        }
                    }
                    sr.Close();
                    sw.Close();
                }
            }
        }
    }
}

