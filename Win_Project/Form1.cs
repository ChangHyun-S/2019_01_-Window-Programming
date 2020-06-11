using MetroFramework.Forms; // 메트로폼 UI
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Project
{

    public partial class Form1 : MetroForm
    {
        Crawling crawling;
        String[] parsing;
        List<PictureBox> pictureBoxes;

        // 바탕화면 저장 위한 dll import
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni); // 바탕화면 설정을 위한 파라미터

        private void AddToList() // List 컬렉션에 picturebox 객체를 추가시킨다
        {
            pictureBoxes.Add(pictureBox1);
            pictureBoxes.Add(pictureBox2);
            pictureBoxes.Add(pictureBox3);
            pictureBoxes.Add(pictureBox4);
            pictureBoxes.Add(pictureBox5);
            pictureBoxes.Add(pictureBox6);
            pictureBoxes.Add(pictureBox7);
            pictureBoxes.Add(pictureBox8);
            pictureBoxes.Add(pictureBox9);
        }

        private void ParsingToPictureBox() // 파싱해서 picturebox 미리보기
        {
            int idx = 1;
            crawling = new Crawling();
            parsing = crawling.ParsingLink(); // crawling 클래스에서 파싱한 링크를 parsing string 배열에 저장한다

            foreach (PictureBox p in pictureBoxes) // 모든 picturebox 객체 반복
            {
                p.Image = null; // 이미지 모두 빈공간으로 만든 후
                p.LoadAsync(parsing[idx]); // 크롤링한 이미지 로딩시킨다
                idx++;
            }
        }

        private void SetWallpaper(int picbox) // 저장 후 바탕화면 변경하는 함수. picbox 매개변수는 몇번째 바탕화면인지 가져온다
        {
            string folderPath = "C:/BG"; // 기본 디렉토리를 지정한다
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);

            
            if (MessageBox.Show("바탕화면을 변경하시겠습니까?", "바탕화면 변경", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // 바탕화면 설정 여부 Yes, No 메세지박스
            {
                string filename = folderPath + "/BG.png";
                MessageBox.Show(filename + " 경로에 저장 후 설정됩니다.", "바탕화면 변경", MessageBoxButtons.OK , MessageBoxIcon.Information); // 해당 경로에 저장된다는 정보 메세지박스

                if (!directoryInfo.Exists) // 디렉토리(C:/temp)가 존재하지 않으면 생성시킨다
                {
                    MessageBox.Show("해당경로가 존재하지 않습니다.\n" + folderPath + " 경로를 생성 후 설정합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning); // 경로 생성한다는 경고 메세지박스
                    directoryInfo.Create(); // 디렉토리 생성
                }

                if (!crawling.DownloadRemoteImageFile(parsing[picbox], filename)) // 다운로드 실패 메시지
                    MessageBox.Show("실패");

                else
                    SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_UPDATEINIFILE); // 다운받은 경로에 대한 월페이퍼 지정
            }
        }

        private void ClickToPictureBox(object sender) // picturebox switch 이용해서 월페이퍼 지정
        {
            string BoxType = ((PictureBox)sender).Name; // 클릭한 picturebox의 이름을 BoxType 변수에 저장한다

            switch(BoxType) // picturebox 이름에 따라 월페이퍼를 지정한다. 몇번째 바탕화면인지 SetWallpaper 매개변수로 넘겨준다
            {
                case "pictureBox1":
                    SetWallpaper(1);
                    break;
                case "pictureBox2":
                    SetWallpaper(2);
                    break;
                case "pictureBox3":
                    SetWallpaper(3);
                    break;
                case "pictureBox4":
                    SetWallpaper(4);
                    break;
                case "pictureBox5":
                    SetWallpaper(5);
                    break;
                case "pictureBox6":
                    SetWallpaper(6);
                    break;
                case "pictureBox7":
                    SetWallpaper(7);
                    break;
                case "pictureBox8":
                    SetWallpaper(8);
                    break;
                case "pictureBox9":
                    SetWallpaper(9);
                    break;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        // 폼 로딩시 PictureBox List 컬렉션 생성
        // 그 후에 AddToList 함수를 호출하여 컬렉션에 각 picturebox를 추가한다
        // 그 후에 ParsingToPictureBox 함수를 호출하여 파싱한 이미지를 picturebox에 로딩시킨다
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxes = new List<PictureBox>();
            AddToList();
            ParsingToPictureBox();
        }

        private void MetroButton1_Click(object sender, EventArgs e) // 새로고침 
        {
            ParsingToPictureBox();
        }

        // 이하 picturebox 클릭시 배경화면 지정
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox7_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox8_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

        private void PictureBox9_Click(object sender, EventArgs e)
        {
            ClickToPictureBox(sender);
        }

    }
}