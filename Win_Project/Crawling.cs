using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Project
{
    class Crawling
    {
        Random random = new Random(); // 새로고침시 랜덤으로 카테고리를 불러오기 위해 생성

        // 여러 종류의 카테고리를 생성시킨다
        string[] path = { "https://unsplash.com/t/wallpapers" , "https://unsplash.com/t/textures-patterns" , "https://unsplash.com/t/nature", "https://unsplash.com/t/current-events", "https://unsplash.com/t/current-events"
        , "https://unsplash.com/t/business-work", "https://unsplash.com/t/film", "https://unsplash.com/t/animals", "https://unsplash.com/t/travel", "https://unsplash.com/t/fashion", "https://unsplash.com/t/food-drink",
        "https://unsplash.com/t/spirituality", "https://unsplash.com/t/experimental", "https://unsplash.com/t/people", "https://unsplash.com/t/health", "https://unsplash.com/t/arts-culture"};

        public string[] ParsingLink() // path에서 이미지파일 파싱
        {
            string[] URL; // 파싱된 URL을 담기 위한 변수
            string link = path[random.Next(0, path.Length)]; // 카테고리에서 무작위로 1개 지정

            using (WebClient client = new WebClient()) // 파싱을위한 Webclient 클래스 사용
            {
                string html = client.DownloadString(link); // 무작위로 지정한 링크 소스 가져오기
                // 파싱할 부분의 시작부분 검색. href=\"https://unsplash.com/photos/ 뒤에부분
                string[] Code = html.Split(new string[] { "href=\"https://unsplash.com/photos/" }, StringSplitOptions.None);

                URL = new string[Code.Length]; // 가져온 길이만큼 URL 크기 지정

                for (int i = 0; i < 10; i++)
                {
                    string LinkCode = Code[i].Substring(0, 11); // /photos/ 링크 뒤에 11자리 가져오기
                    if (LinkCode != "<!doctype h" || LinkCode != null) // 잘못 가져온 경우가 아닐때
                    {
                        URL[i] = "https://unsplash.com/photos/" + LinkCode + "/download?force=true"; // 이미지 링크로 복구
                    }
                }
            }
            return URL;
        }


        public bool DownloadRemoteImageFile(string url, string fileName) // 이미지 파일 다운로드
        {
            // 다운로드를 위한 HttpWebRequest, HttpWebResponse를 사용한다.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url); // 매개변수로 받은 url을 전달한다
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            bool bImage = response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase); // 이미지일 경우에

            //파일이 있는지 확인 및 이미지일 경우 (bImage) true를 리턴한다
            if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.Redirect) && bImage)
            {
                // 파일이 확인되면 stream, buffer 이용하여 다운로드한다, 파일 이름을 매개변수로 받아 저장한다
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096]; // 4096바이트 버퍼를 생성한다
                    int bytesRead;

                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0); // 버퍼 크기만큼 EOF까지 읽고 저장
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
//) 프로젝트를 구성하는 소스 프로그램의 작업내용을 간단히 정리할 것. 
//● ‘구현 결과’ 란에는 실행결과를 처리과정에 따라 정리할 것