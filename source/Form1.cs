using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leaf.xNet;
using Newtonsoft.Json.Linq;

namespace BlurBid
{
    public partial class Form1 : Form
    {
        static bool isConnected = false;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns.Add("contract", "Contract");
            dataGridView1.Columns.Add("collection_items", "Collection items");
            dataGridView1.Columns.Add("price", "Price");

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.Columns.Add("link", "Link");
            dataGridView2.Columns.Add("name_code", "Name and code");
            dataGridView2.Columns.Add("price", "Price");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                if (isConnected)
                {
                    label4.Text = "connected";
                    button2.Text = "DISCONNECT";
                }
                else
                {
                    label4.Text = "no connect";
                    button2.Text = "CONNECT";

                }
                isConnected = !isConnected;

            }
            else
            {
                MessageBox.Show("Check alchemy key and private key for valid!", "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                parseByLink(textBox3.Text);
                if (addByLink(textBox3.Text))
                {
                    dataGridView2.Rows.Add();
                }
            }
        }

        static bool addByLink(string contract)
        {
            // nethereum code here
            return true;
        }

        static bool placBid(NFT item)
        {
            // nethereum code here
            return true;
        }

        static NFT parseByLink(string lnk)
        {
            NFT item = new NFT();

            try
            {
                string addr = new System.Text.RegularExpressions.Regex("0x[a-fA-F0-9]{40}").Match(lnk).Value;
                string number = new System.Text.RegularExpressions.Regex("/[0-9]{1,10}$").Match(lnk).Value.Trim('/');

                HttpRequest http = new HttpRequest();
                http.KeepAlive = true;
                http.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/jxl,image/webp,*/*;q=0.8");
                http.AddHeader("Accept-Language", "en-US,en;q=0.5");
                http.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:102.0) Gecko/20100101 Firefox/102.0");
                http.AddHeader("Upgrade-Insecure-Requests", "1");
                http.AddHeader("Sec-Fetch-Dest", "document");
                http.AddHeader("Sec-Fetch-Mode", "navigate");
                http.AddHeader("Sec-Fetch-Site", "none");
                http.AddHeader("Sec-Fetch-User", "?1");
                http.AddHeader("TE", "trailers");
                string result = http.Get($"https://core-api.prod.blur.io/v1/collections/{addr}/tokens/{number}").ToString();

                // fix 403 error


                item.name = number;
                item.link = lnk;
                JObject o = JObject.Parse(result);
            }
            catch
            { }
            

            return item;

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }

    class NFT
    {
        public string name;
        public double price;
        public string contract;
        public string link;
    }
}
