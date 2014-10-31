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
                txtEncrypted.Text = Convert.ToString(envelopedCms.ContentInfo.Content);
                System.IO.File.WriteAllBytes(txtFileName.Text + "decrypted.txt", envelopedCms.ContentInfo.Content);
                MessageBox.Show("Decrypted! Contents written to " + txtFileName.Text + "decrypted.txt");
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


    }

}
