using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public partial class frmMain : Form
    {
        SignedCms signer = new SignedCms();
        public frmMain()
        {
            InitializeComponent();

        }
        private void Decrypt()
        {
            try
            {
                string filename = txtFileName.Text;
                string encryptedStuff = txtEncrypted.Text;
                Byte[] datatodecrypt = Convert.FromBase64String(encryptedStuff);
                EnvelopedCms envelopedCms = new EnvelopedCms();
                envelopedCms.Decode(datatodecrypt);
                Console.WriteLine(envelopedCms.ContentInfo.Content.Length);
                envelopedCms.Decrypt(envelopedCms.RecipientInfos[0]);
                Convert.ToString(envelopedCms.ContentInfo.Content);

                string filepart = "Decrypted_" + DateTime.Now.ToString() + ".txt";
                filepart = filepart.Replace("/", "_");
                filepart = filepart.Replace(" ", "_");
                filepart = filepart.Replace(":", "_");
                filename = txtFileName.Text + filepart;

                System.IO.File.WriteAllBytes(filename, envelopedCms.ContentInfo.Content);
                MessageBox.Show("Decrypted! Contents written to : " + filename);
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Decrypt();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtEncrypted.Text = "";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtFileName.Text = Environment.ExpandEnvironmentVariables("%USERPROFILE%") + "\\Desktop\\ProfileDec_";

            fldBrosweDialog.RootFolder = Environment.SpecialFolder.Desktop;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            fldBrosweDialog.ShowDialog();
            txtFileName.Text = fldBrosweDialog.SelectedPath + "\\ProfileDec_";
        }

        private void fldBrosweDialog_HelpRequest(object sender, EventArgs e)
        {

        }


    }

}
