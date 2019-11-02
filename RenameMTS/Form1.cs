using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RenameMTS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        static readonly Guid MTSGUID = new Guid(new byte[] { 0x17, 0xee, 0x8c, 0x60, 0xf8, 0x4d, 0x11, 0xd9, 0x8c, 0xd6, 0x08, 0x00, 0x20, 0x0c, 0x9a, 0x66 });
        static readonly int FIRSTGUIDBYTE = 0x17;

        private string GetNewMTSFileName(string oldName)
        {
            //Opens .MTS file (read only), finds date/time stamp, and returns new file name in 
            //the form of OriginalDirectory\yyyymmddhhmmss.MTS
            //oldName must be the full path name of an existing .MTS file.
            //If date/time stamp cannot be located, returns null.

            string newName = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(oldName, FileMode.Open, FileAccess.Read);
                int nextByte;
                nextByte = fs.ReadByte();
                while (nextByte != -1 && fs.Position < 100000)
                {
                    long startPos = fs.Position; //This will be new starting position for any subsequent search for MTSGUID

                    if (nextByte != FIRSTGUIDBYTE)
                    {
                        nextByte = fs.ReadByte();
                        continue;
                    } 

                    fs.Seek(-1, SeekOrigin.Current); //Go back 1 byte in order to read the whole GUID
                    byte[] guidBytes;
                    guidBytes = new BinaryReader(fs).ReadBytes(16);
                    if (guidBytes.Length < 16) break;
                    if (new Guid(guidBytes) != MTSGUID)  //Are these 16 bytes the correct GUID?
                    {
                        fs.Position = startPos;
                        nextByte = fs.ReadByte();
                        continue;
                    } 


                    byte[] fBB = new BinaryReader(fs).ReadBytes(4);  //four Byte Block after the GUID
                    if (fBB.Length != 4) break;
                    if (fBB[0] != 0x4d || fBB[1] != 0x44 || fBB[2] != 0x50 || fBB[3] != 0x4d)
                    {
                        fs.Position = startPos;
                        nextByte = fs.ReadByte();
                        continue;
                    }

                    if (fs.ReadByte() == -1) break; //This byte specifies the number of tags to follow

                    nextByte = fs.ReadByte();  //This byte should be 0x18, indicating the tag for year and month
                    if (nextByte == -1) break;
                    if (nextByte != 0x18)
                    {
                        fs.Position = startPos;
                        nextByte = fs.ReadByte();
                        continue;
                    }

                    if (fs.ReadByte() == -1) break;  //First data byte in the year/month tag is an unknown
                    byte[] monthYearBytes = new BinaryReader(fs).ReadBytes(3);
                    if (monthYearBytes.Length < 3) break;
                
                    nextByte = fs.ReadByte();  //This byte should be 0x19, indicating the tag for day, hr, min, sec
                    if (nextByte == -1) break;
                    if (nextByte != 0x19)
                    {
                        fs.Position = startPos;
                        nextByte = fs.ReadByte();
                        continue;
                    }
                    byte[] dayHrMinSecBytes = new BinaryReader(fs).ReadBytes(4);

                    fs.Close();

                    //At this point, we have succeeded. Generate new file name, and return
                    if (dayHrMinSecBytes.Length < 4) break;
                    newName = Path.GetDirectoryName(oldName) + "\\"
                        +monthYearBytes[0].ToString("X2") + monthYearBytes[1].ToString("X2") + monthYearBytes[2].ToString("X2")
                        + dayHrMinSecBytes[0].ToString("X2") + dayHrMinSecBytes[1].ToString("X2") + dayHrMinSecBytes[2].ToString("X2")
                        + dayHrMinSecBytes[3].ToString("X2") + Path.GetExtension(oldName);
                    return newName;
                }

                //if the while loop exits via break, end-of-file, or Position>=100000, then we have failed
                return null;
            }
            catch
            {

                //in the case of any exception, we have failed
                return null;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            lbxSuccess.Items.Clear();
            lbxFailure.Items.Clear();
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "MTS Files (*.MTS,*.mts)|*.MTS;*.mts";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() != DialogResult.OK || dlg.FileNames == null || dlg.FileNames.Length == 0)
                {
                    MessageBox.Show("No files selected.");
                    return;
                }

                if (MessageBox.Show(dlg.FileNames.Length.ToString() + " files selected for renaming. Proceed?",
                    "Rename Files", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    MessageBox.Show("No files renamed.");
                    return;
                }

                foreach (string fname in dlg.FileNames)
                {
                    if (!File.Exists(fname))
                    {
                        lbxFailure.Items.Add(fname);
                        continue;
                    }
                    string ext = Path.GetExtension(fname);
                    if (string.IsNullOrEmpty(ext) || (ext != ".MTS" && ext != ".mts"))
                    {
                        lbxFailure.Items.Add(fname);
                        continue;
                    }
                    string newName = GetNewMTSFileName(fname);
                    try
                    {
                        File.Move(fname, newName);
                        lbxSuccess.Items.Add(Path.GetFileName(fname) + " --> " + Path.GetFileName(newName));
                    }
                    catch
                    {
                        lbxFailure.Items.Add(fname);
                        continue;
                    }
                }
            }
        }
    }
}
