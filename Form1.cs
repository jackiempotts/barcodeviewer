using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public async void showButton_Click(object sender, EventArgs e)
        {
            string barcode = textBox1.Text;
            using var httpClient = new HttpClient();
            string myStringResource;
            string remoteurl = "https://barcode.tec-it.com/barcode.ashx?data=";
            string upc = "&code=UPCA";
            int n = 2;
            barcode = barcode.Remove(0, n);
            myStringResource = remoteurl + barcode + upc;
            byte[] imageBytes = await httpClient.GetByteArrayAsync(myStringResource);
            MemoryStream memoryStream = new MemoryStream(imageBytes);
            pictureBox1.Image = Image.FromStream(memoryStream);

            //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //string fileName = "testUPC.png";
            //string localPath = Path.Combine(documentsPath, fileName);
            //File.WriteAllBytes(localPath, imageBytes);
            //pictureBox1.Load(localPath);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = null;
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public async void saveButton_Click(object sender, EventArgs e)
        {
            string barcode = textBox1.Text;
            using var httpClient = new HttpClient();
            string myStringResource;
            string remoteurl = "https://barcode.tec-it.com/barcode.ashx?data=";
            string upc = "&code=UPCA";
            int n = 2;
            barcode = barcode.Remove(0, n);
            myStringResource = remoteurl + barcode + upc;
            byte[] imageBytes = await httpClient.GetByteArrayAsync(myStringResource);
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string fileName = "testUPC.png";
            string localPath = Path.Combine(documentsPath, fileName);
            bool fileExist = File.Exists(localPath);
            if (fileExist)
            {
                MessageBox.Show("A file with the name testUPC.png already exists");
            }
            else
            {
                File.WriteAllBytes(localPath, imageBytes);
                MessageBox.Show("File has been saved in MyDocuments Folder - testUPC.png");
            }
        }
    }
}
