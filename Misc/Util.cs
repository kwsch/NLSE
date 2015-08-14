using System;
using System.IO;
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
    }
}
