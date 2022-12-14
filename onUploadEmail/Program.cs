using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using Microsoft.VisualBasic;


using System.Net.Mail;
using System.Net;


namespace onUploadEmail
{
    class Program
    {




        static void Main(string[] args)
        {

            //when a new file gets uplaoded to \\VISONAS\Prepress\SFTP
            //have user type in folder name ex) "aspen" or "rittal"
            //email kristine and dan


            List<string> list = new List<string>();
            bool yes = false;

            //read in the file and compare to what the directory has
            using (StreamReader sr = new StreamReader("//visonas/public/Kyle/onUploadProgram/Debug/TextFile1.txt"))
            {
                while (sr.Peek() >= 0)
                {
                     list.Add(sr.ReadLine());
                }

                sr.Close();
            }
           

            //get the files in the directory

            DirectoryInfo d = new DirectoryInfo("//VISONAS/Prepress/SFTP/aspen");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*"); //Getting files

            //compare the two lists find different files
            List<string> filesD = new List<string>();

            foreach (FileInfo file in Files)
            {

                string filename = file.Name.ToString();
                filesD.Add(filename);

            }

            List<string> diff = filesD.Except(list).ToList();


                //differeent legnths then new files must be added send email
                if (list.Count != Files.Length)
            {

                yes = true;

            }//end if 

            else //if same counts chekc the file names
            {
                int y = 0;
               
                foreach (FileInfo file in Files)
                {
              
                    string filename = file.Name.ToString();
                    if (list[y].ToString() != filename)
                    {

                        yes = true;

                    }
                    y++;
                }


            }//end else

                //folders

            var sw = new StreamWriter("//visonas/public/Kyle/onUploadProgram/Debug/TextFile1.txt");
            //write to file
            foreach (FileInfo file in Files)
            {
                string s = file.Name;
                sw.WriteLine(file.Name);

            }


            sw.Close();


            //when a new file gets uplaoded to \\VISONAS\Prepress\SFTP
            //have user type in folder name ex) "aspen" or "rittal"
            //email kristine and dan



            List<string> listFolders = new List<string>();
  

            //read in the file and compare to what the directory has
            using (StreamReader sr = new StreamReader("//visonas/public/Kyle/onUploadProgram/Debug/TextFile2.txt"))
            {
                while (sr.Peek() >= 0)
                {
                    listFolders.Add(sr.ReadLine());
                }

                sr.Close();
            }


            DirectoryInfo di = new DirectoryInfo("//VISONAS/Prepress/SFTP/aspen");
            DirectoryInfo[] diArr = di.GetDirectories();

            List<string> folders = new List<string>();

            foreach (DirectoryInfo folder in diArr)
            {

                string filename = folder.Name.ToString();
                folders.Add(filename);
            }

            List<string> foldersDiff = folders.Except(listFolders).ToList();

            //differeent legnths then new files must be added send email
            if (listFolders.Count != diArr.Length)
            {
                
                yes = true;

            }//end if 

            else //if same counts chekc the file names
            {
                int y = 0;

                foreach (DirectoryInfo folder in diArr)
                {

                    string filename = folder.Name.ToString();
                    if (listFolders[y].ToString() != filename)
                    {
                        
                        
                        yes = true;

                    }
                    y++;
                }


            }//end else

  

            var swFolder = new StreamWriter("//visonas/public/Kyle/onUploadProgram/Debug/TextFile2.txt");
            //write to file

            foreach (DirectoryInfo folder in diArr)
            {
                string s = folder.Name;
                swFolder.WriteLine(folder.Name);

            }


            swFolder.Close();







            if (yes == true)
            {


                try
                {

                    string mailBuilder = "";

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("webmail.visographic.com ");

                    mail.From = new MailAddress("kjacobsen@visographic.com");

                   mail.To.Add("JCardelli@visographic.com");
                    mail.To.Add("dnavigato@visographic.com");
                     mail.To.Add("mailing@visographic.com");
                    mail.To.Add("kjacobsen@visographic.com");
                    //add mailing dan and kristine

                    //build th ebody of email using string builder^
                    //mail.Subject = "Aspen SFTP Update on Visonas";

                    mailBuilder += "Aspen has SFTP updated:\n";


                    for (int x = 0; x<list.Count;  x++) {
                       mailBuilder += list[x].ToString() + "\n";
                    }


                    mailBuilder += "\n"+@"Please see path: \\VISONAS\Prepress\SFTP\aspen\";


                    
                    mailBuilder += "\n\nNew files/folders:";

                    //add the strings
                    foldersDiff.AddRange(diff);

                    foreach (string s in foldersDiff) {

                        mailBuilder += "\n" + s;

                    
                    }



                    mail.Body = mailBuilder;

                  

                    SmtpServer.Port = 587;

                    SmtpServer.Credentials = new System.Net.NetworkCredential("kjacobsen@visographic.com", "wood234StockA2**");
                    // SmtpServer.EnableSsl = true;

                    mail.Subject = "Aspen upload";

                    SmtpServer.Send(mail);
           
                }
                catch (Exception ex)
                {

                    string s = ex.ToString();

                }

            }//end yes
          
            Environment.Exit(0);

        }//end main
    }//end class
}//end namespace
