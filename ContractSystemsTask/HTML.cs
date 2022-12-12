using System.Collections.Generic;
using System.IO;


namespace ContractSystemsTask
{
    static class HTMLFile
    {
        public static void Save(string filePath, IEnumerable<Directory> directories, IEnumerable<MimeTypeInfo> mimes)
        {
            if (directories == null)
                return;

            int columns = 1;

            // create file (delete old file)
            FileStream fileStream = System.IO.File.Open(filePath, FileMode.Create);
            using (StreamWriter output = new StreamWriter(fileStream))
            {
                // create table
                output.WriteLine("<table bgcolor=\"#EEF9FD\">");

                // create header
                output.WriteLine("<tr >");
                output.WriteLine($"<th bgcolor=\"#D3EDF6\" width=1220><big > DIRECTORY STRUCTURE</big ></th>");
                output.WriteLine("</tr >");

                //  rows
                foreach (Directory dir in directories)
                {
                    List<string> value = new();
                    output.WriteLine("<tr >");
                    for (int i = 0; i < columns; i++)
                    {
                        string s = new string('_', dir.Level) + " Directory " + dir.Name + "     Size = " + dir.FolderSize;
                        value.Add(s);
                        foreach (var file in dir.Files)
                        {
                            var name = file.Name;
                            var level = file.Level;
                            string ss = new string('_', file.Level) + " File " + file.Name + "     Size =  " + file.FileSize + "   Mime Type " + file.MimeType;
                            value.Add(ss);
                        }

                        foreach (var t in value)
                        {
                            if (t.Contains("Directory"))
                            {
                                output.WriteLine("<tr>");
                                output.WriteLine($"<td><font size=\"3.5\" color=\"#073763\" face=\"Arial\"><b>{t}</b></font></td>");
                                output.WriteLine("<tr>");
                            }
                            else
                            {
                                output.WriteLine("<tr>");
                                output.WriteLine($"<td><font size=\"3.5\" color=\"#176097\" face=\"Arial\"><i>{t}</i></font></td>");
                                output.WriteLine("<tr>");
                            }
                        }
                    }
                    output.WriteLine("</tr>");
                }

                // end table
                output.WriteLine("</table>");

                output.WriteLine("</br>");

                // create table
                output.WriteLine("<table border='1' bgcolor=\"#EEF9FD\">");

                // create header
                output.WriteLine("<tr >");
                string[] columnMimeNames = { "Mime Type", "Quantity", "Percentage", "Average File Size" };
                for (int i = 0; i < columnMimeNames.Length; i++)
                {
                    output.WriteLine($"<th bgcolor=\"#D3EDF6\" width=300><big > {columnMimeNames[i]}</big ></th>");
                }
                output.WriteLine("</tr >");

                //  rows
                foreach (MimeTypeInfo mime in mimes)
                {
                    output.WriteLine("<tr align = \"center\">");
                    List<string> values = new() { mime.MimeType, mime.Quantity.ToString(), mime.Percentage.ToString(), mime.AverageFileSize.ToString() };
                    for (int i = 0; i < columnMimeNames.Length; i++)
                    {
                        output.WriteLine($"<td><font size=\"3.5\" color=\"#073763\" face=\"Arial\">{values[i]}</font></td>");

                    }
                    output.WriteLine("</tr>");

                }
                // end table
                output.WriteLine("</table>");

                // close file
                output.Close();
            }
        }
    }
}
