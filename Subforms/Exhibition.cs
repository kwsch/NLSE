using System;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Exhibition : Form
    {
        public Exhibition()
        {
            InitializeComponent();
            Save = new ExhibitionData(Main.SaveData);

            // Load
            loadData();
        }
        private void B_Save_Click(object sender, EventArgs e)
        {
            saveData();
            Close();
        }
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Exhibition Save Editing
        private ExhibitionData Save;
        class ExhibitionData
        {
            public byte[] Data;
            public ExhibitionData(byte[] data)
            {
                Data = data;
            }
            public byte[] Write()
            {
                return Data;
            }
        }
        private void loadData()
        {

        }
        private void saveData()
        {
            Main.SaveData = Save.Write();
        }

        //
    }
}
