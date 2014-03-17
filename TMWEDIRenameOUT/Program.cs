using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace TMWEDIRenameOUT
{
    class EmptyTest
    {
        //Method to test if directory is empty or has files
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }


    class TMWRename
    {
        static void Main(string[] args)
        {
            bool bEmpty = false;
            string sMessage = null;
            string sDir = null;
            string sFname = null;
            string sRName = null;
            string sDate = null;
            var lOldNames = new List<string>();

            //Set directory and test if empty.  If not empty test the file to see if previously renamed, if yes, then skip, if not, rename.
            sDir = "F:/EDI/IP_IPTRANS/214";
            EmptyTest ET = new EmptyTest();
            bEmpty = ET.IsDirectoryEmpty(sDir);

            if (bEmpty == false)
            {
                DirectoryInfo d4 = new DirectoryInfo(sDir);
                FileInfo[] infos4 = d4.GetFiles("*.*");

                foreach (FileInfo f4 in infos4)
                {
                    DateTime dtCreateDate = f4.CreationTime;
                    sFname = f4.Name;
                    sFname = Path.GetFileNameWithoutExtension(sFname);
                    sRName = sFname.Substring(sFname.Length - 4);

                    if (f4.CreationTime.AddHours(1) < DateTime.Now)
                    {
                        lOldNames.Add(f4.FullName);
                    }

                    if (sRName == "4-IP")
                    {
                        continue;
                    }

                    File.Move(f4.FullName, Path.Combine(f4.DirectoryName, f4.Name + "4-IP.txt"));
                }
            }

            //Set directory and test if empty.  If not empty test the file to see if previously renamed, if yes, then skip, if not, rename.
            sDir = "F:/EDI/IP_IPTRANS/210";
            bEmpty = ET.IsDirectoryEmpty(sDir);

            if (bEmpty == false)
            {

                DirectoryInfo d0 = new DirectoryInfo(sDir);
                FileInfo[] infos0 = d0.GetFiles("*.*");

                foreach (FileInfo f0 in infos0)
                {
                    DateTime dtCreateDate = f0.CreationTime;
                    sFname = f0.Name;
                    sFname = Path.GetFileNameWithoutExtension(sFname);
                    sRName = sFname.Substring(sFname.Length - 4);
                    
                    if (f0.CreationTime.AddHours(1) < DateTime.Now)
                    {
                        lOldNames.Add(f0.FullName);
                    }

                    if (sRName == "0-IP")
                    {
                        continue;
                    }

                    File.Move(f0.FullName, Path.Combine(f0.DirectoryName, f0.Name + "0-IP.txt"));
                }
            }

            //Set directory and test if empty.  If not empty test the file to see if previously renamed, if yes, then skip, if not, rename.
            sDir = "F:/EDI/IP_IPTRANS/990";
            bEmpty = ET.IsDirectoryEmpty(sDir);

            if (bEmpty == false)
            {

                DirectoryInfo d9 = new DirectoryInfo(sDir);
                FileInfo[] infos9 = d9.GetFiles("*.*");

                foreach (FileInfo f9 in infos9)
                {
                    DateTime dtCreateDate = f9.CreationTime;
                    sFname = f9.Name;
                    sFname = Path.GetFileNameWithoutExtension(sFname);
                    sRName = sFname.Substring(sFname.Length - 4);

                    if (f9.CreationTime.AddHours(1) < DateTime.Now)
                    {
                        lOldNames.Add(f9.FullName);
                    }
                    
                    if (sRName == "9-IP")
                    {
                        continue;
                    }
                    sDate = DateTime.Now.ToString("HHmmss");
                    File.Move(f9.FullName, Path.Combine(f9.DirectoryName, f9.Name + sDate + "9-IP.txt"));

                }
            }
//Console.WriteLine("Old Files Count: " + lOldNames.Count);
//Console.WriteLine("Press ENTER key to close...");
//Console.ReadLine();


            //If list has any data, send email.
            if (lOldNames.Count > 0)
            {
                foreach (var s in lOldNames)
                {
                    sMessage = sMessage + s + "\r\n";
                }

                    MailMessage mail = new MailMessage();

                    string sServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
                    string sFrom = System.Configuration.ConfigurationManager.AppSettings["SMTPFrom"];
                    string sTo = System.Configuration.ConfigurationManager.AppSettings["SMTPTo"];
                    string sUname = System.Configuration.ConfigurationManager.AppSettings["SMTPUname"];
                    string sPwd = System.Configuration.ConfigurationManager.AppSettings["SMTPPwd"];

                    SmtpClient SmtpServer = new SmtpClient(sServer);
                    mail.From = new MailAddress(sFrom);
                    mail.To.Add(sTo);
                    mail.Subject = "Old TMW EDI Files";
                    mail.Body = sMessage;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(sUname, sPwd);
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

            }
        }
    }
}
