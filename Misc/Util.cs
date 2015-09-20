using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NLSE
{
    class Util
    {
        internal static string GetSDFLocation()
        {
            try
            {
                // Start by checking if the 3DS file path exists or not.
                string path_SDF = null;
                string[] DriveList = Environment.GetLogicalDrives();
                for (int i = 1; i < DriveList.Length; i++) // Skip first drive (some users still have floppy drives and would chew up time!)
                {
                    string potentialPath_SDF = NormalizePath(Path.Combine(DriveList[i], "filer" + Path.DirectorySeparatorChar + "UserSaveData"));
                    if (!Directory.Exists(potentialPath_SDF)) continue;

                    path_SDF = potentialPath_SDF; break;
                }
                if (path_SDF == null)
                    return null;
                // 3DS data found in SD card reader. Let's get the title folder location!
                string[] folders = Directory.GetDirectories(path_SDF, "*", SearchOption.TopDirectoryOnly);
                Array.Sort(folders); // Don't need Modified Date, sort by path names just in case.

                // Loop through all the folders in the Nintendo 3DS folder to see if any of them contain 'title'.
                for (int i = folders.Length - 1; i >= 0; i--)
                {
                    if (File.Exists(Path.Combine(folders[i], "00000862" + Path.DirectorySeparatorChar + "garden.dat"))) return Path.Combine(folders[i], "00000862"); // JP
                    if (File.Exists(Path.Combine(folders[i], "00000863" + Path.DirectorySeparatorChar + "garden.dat"))) return Path.Combine(folders[i], "00000863"); // NA
                    if (File.Exists(Path.Combine(folders[i], "00000864" + Path.DirectorySeparatorChar + "garden.dat"))) return Path.Combine(folders[i], "00000864"); // EU
                }
                return null; // Fallthrough
            }
            catch { return null; }
        }
        internal static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
        internal static DialogResult Error(params string[] lines)
        {
            System.Media.SystemSounds.Exclamation.Play();
            string msg = String.Join(Environment.NewLine + Environment.NewLine, lines);
            return MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal static DialogResult Alert(params string[] lines)
        {
            System.Media.SystemSounds.Asterisk.Play();
            string msg = String.Join(Environment.NewLine + Environment.NewLine, lines);
            return MessageBox.Show(msg, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        internal static DialogResult Prompt(MessageBoxButtons btn, params string[] lines)
        {
            System.Media.SystemSounds.Question.Play();
            string msg = String.Join(Environment.NewLine + Environment.NewLine, lines);
            return MessageBox.Show(msg, "Prompt", btn, MessageBoxIcon.Asterisk);
        }
        internal static int getIndex(ComboBox cb)
        {
            int val;
            if (cb.SelectedValue == null) return 0;

            try { val = int.Parse(cb.SelectedValue.ToString()); }
            catch { val = cb.SelectedIndex; if (val < 0) val = 0; }

            return val;
        }
        // Find Code off of Reference
        internal static int IndexOfBytes(byte[] array, byte[] pattern, int startIndex, int count)
        {
            int i = startIndex;
            int endIndex = count > 0 ? startIndex + count : array.Length;
            int fidx = 0;

            while (i++ != endIndex - 1)
            {
                if (array[i] != pattern[fidx]) i -= fidx;
                fidx = (array[i] == pattern[fidx]) ? ++fidx : 0;
                if (fidx == pattern.Length)
                    return i - fidx + 1;
            }
            return -1;
        }
        internal static void ReplaceAllBytes(byte[] array, byte[] oldPattern, byte[] newPattern)
        {
            if (oldPattern.SequenceEqual(newPattern))
                return;
            int offset; // Loop until no instances of oldPattern are found
            while ((offset = IndexOfBytes(array, oldPattern, 0, 0)) != -1)
                Array.Copy(newPattern, 0, array, offset, newPattern.Length);
        }
    }
}
