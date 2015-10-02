using System;
using System.Drawing;
using System.Collections.Generic;

namespace LynnaLab
{
    public abstract class FileComponent {
        protected List<int> spacing;


        public bool EndsLine {get; set;} // True if a newline comes after this data

        // True if it's an internally-made object, not to be written back to
        // the file
        public bool Fake {get; set;}


        public FileComponent(IList<int> spacing) {
            EndsLine = true;
            Fake = false;
            if (spacing != null)
                this.spacing = new List<int>(spacing);
        }

        // Returns a string representing the given space value
        protected string GetSpacing(int spaces) {
            string s = "";
            while (spaces > 0) {
                s += ' ';
                spaces--;
            }
            while (spaces < 0) {
                s += '\t';
                spaces++;
            }
            return s;
        }

        public abstract string GetString();
    }

    public class StringFileComponent : FileComponent {
        string str;

        public StringFileComponent(string s, IList<int> spacing) : base(spacing) {
            str = s;
            if (this.spacing == null)
                this.spacing = new List<int>();
            while (this.spacing.Count < 2)
                this.spacing.Add(0);
        }

        public override string GetString() {
            return GetSpacing(spacing[0]) + str + GetSpacing(spacing[1]);
        }
    }
}