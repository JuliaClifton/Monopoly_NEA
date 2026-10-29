using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
	public partial class Form3 : Form
	{
		private String[] rulesList;
		int n = 0;

		public Form3()
		{
			InitializeComponent();

			rulesList = new string[]
			{
			"Page1", "Page2",  "Page3", "Page4", "Page5", "Page6"
			};

			this.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(rulesList[n]);
		}

        private void Previous_Click(object sender, EventArgs e)
        {
			if (n > 0)
			{
				n = n - 1;
                this.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(rulesList[n]);
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
			if (n < 5)
			{
				n = n + 1;
                this.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(rulesList[n]);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
			this.Close();
        }
    }


}
