using System;
using System.Windows.Forms;

namespace NLSE
{
    public partial class Friend : Form
    {
        public Friend()
        {
            InitializeComponent();
            Save = new FriendData(Main.SaveData);

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

        // Friend Save Editing
        private FriendData Save;
        class FriendData
        {
            public byte[] Data;
            public FriendData(byte[] data)
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
