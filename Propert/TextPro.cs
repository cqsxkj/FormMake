using System;
using System.Windows.Forms;
using WindowMake.Device;

namespace WindowMake.Propert
{
    public partial class TextPro : Form
    {
        public TextPropert m_textpro;
        public TextPro()
        {
            InitializeComponent();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.btn_color.BackColor = colorDialog.Color;
            }
        }

        private void btn_font_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                this.btn_font.Font = fontDialog.Font;
                this.btn_font.Text = this.btn_font.Font.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_textpro.m_text = this.textBox1.Text;
            m_textpro.m_font = this.btn_font.Font;
            m_textpro.m_color = this.btn_color.BackColor;
            DialogResult = DialogResult.OK;
        }

        private void TextPro_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = m_textpro.m_text;
            this.btn_font.Font = m_textpro.m_font;
            this.btn_font.Text = this.btn_font.Font.ToString();
            this.btn_color.BackColor = m_textpro.m_color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
