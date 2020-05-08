using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Linq;
using System.Security;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mail;

namespace test
{


    public static class DeepCopy
    {
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }



    public class Program
    {


        // Deep clone

        public static Random r = new Random();
        public static List<int> maximumlijst = new List<int>();
        public static List<List<string>> personenpergroep = new List<List<string>>();
        public static List<int> cloneablelist = new List<int>();
        public static List<string> Cloneablestringlist = new List<string>();
        public static SortedDictionary<string, int> hostsperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> rondesperpersoon = new SortedDictionary<string, int>();
        public static SortedDictionary<string, int> aantalkeermeegedaanpersoon = new SortedDictionary<string, int>();
        public static List<string> hostpergroep = new List<string>();
        public static SortedDictionary<string, int> cloneablestringkeydictionary = new SortedDictionary<string, int>();
        public static List<string> keuzegerechten = new List<string> { "Aperitief met amuse", "Nagerecht" };
        public static List<string> keuzegerechten2 = new List<string> { "Koud voorgerecht", "Warm voorgerecht" };

        public class subprogram
        {
            public static class gatherdata
            { 
             public static void executegathering()
                {
         

                    int x = 0;
                    if (!aantalkeermeegedaanpersoon.ContainsKey("persoon2"))
                    {
                        aantalkeermeegedaanpersoon.Add("persoon2", 1);
                    }
                    else
                    {
                        aantalkeermeegedaanpersoon["persoon2"] = aantalkeermeegedaanpersoon["persoon2"] + 1;
                    }

                    if (!personenpergroep.Any())
                    {
                        List<List<string>> temporarylist = personenpergroep.DeepClone();
                        List<string> storenamestocheck = Cloneablestringlist.DeepClone();
                        List<int> groupswhichareeligable = cloneablelist.DeepClone();
                        List<string> hostnumberpersonsinsidegroup = Cloneablestringlist.DeepClone();
                        List<string> temporaryhostpergroup = hostpergroep.DeepClone();
                        List<string> clonedkeuzegerechten = keuzegerechten.DeepClone();
                        List<string> clonedkeuzegerechten2 = keuzegerechten2.DeepClone();
                        SortedDictionary<string, int> temporarydictionary = cloneablestringkeydictionary.DeepClone();

                        x++;
                        personenpergroep.Add(new List<string> { "name1" });
                        maximumlijst.Add(r.Next(2, 9));
                        rondesperpersoon.Add("name2", r.Next(2, 6));

                    }
                    else
                    {
                        List<List<string>> temporarylist = personenpergroep.DeepClone();
                        List<string> storenamestocheck = Cloneablestringlist.DeepClone();
                        List<int> groupswhichareeligable = cloneablelist.DeepClone();
                        List<string> hostnumberpersonsinsidegroup = Cloneablestringlist.DeepClone();
                        List<string> temporaryhostpergroup = hostpergroep.DeepClone();
                        List<string> clonedkeuzegerechten = keuzegerechten.DeepClone();
                        List<string> clonedkeuzegerechten2 = keuzegerechten2.DeepClone();
                        SortedDictionary<string, int> temporarydictionary = cloneablestringkeydictionary.DeepClone();

                        if (!rondesperpersoon.ContainsKey("name2"))
                        {
                            rondesperpersoon.Add("name2", r.Next(2, 9));
                        }
                        while (x < rondesperpersoon["name2"])
                        {
                            x++;


                            temporaryhostpergroup.Clear();
                            int i = 0;

                            //get index of lists with person + remove person in clone
                            foreach (var index in temporarylist)
                            {
                                if (index.Contains("person2"))
                                {
                                    var indexoflist = temporarylist[i].IndexOf("person2");
                                    temporarylist[i].RemoveAt(indexoflist);
                                    foreach (var item in temporarylist[i])
                                    {

                                        storenamestocheck.Add(item);
                                    }
                                }
                                i++;

                            }
                            //chechk limit is reached of group
                            foreach (int index2 in maximumlijst)
                            {
                                if (maximumlijst[index2] > personenpergroep[index2].Count)
                                {
                                    hostpergroep.Add("null");
                                    //check if persons have met eachother already

                                    if (personenpergroep[index2].Intersect(storenamestocheck).Any())
                                    {

                                    }
                                    else
                                    {
                                        groupswhichareeligable.Add(index2);
                                    }

                                }

                                else
                                {
                                    if (!temporaryhostpergroup.Contains("null"))
                                    {

                                    }
                                    else
                                    {
                                        foreach (var index3 in personenpergroep[index2])
                                        {
                                            hostnumberpersonsinsidegroup.Add(index3);
                                        }


                                        foreach (var index4 in hostnumberpersonsinsidegroup)
                                        {
                                            if (!hostsperpersoon.ContainsKey(index4))
                                            {
                                                hostsperpersoon.Add(index4, 0);
                                                temporarydictionary.Add(index4, 0);
                                            }
                                            else
                                            {
                                                temporarydictionary.Add(index4, hostsperpersoon[index4]);
                                            }

                                        }
                                        if (x == rondesperpersoon["person2"] - 1 && (hostsperpersoon["person2"] < aantalkeermeegedaanpersoon["person2"]))
                                        {
                                            hostpergroep.Add("person2");
                                        }
                                        else
                                        {
                                            var indexhost = temporarydictionary.Aggregate((l, a) => l.Value < a.Value ? l : a).Key;
                                            hostpergroep.Add(indexhost);
                                        }





                                        DateTime today = DateTime.Today;
                                        int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7) + 1;
                                        DateTime nextTuesday = today.AddDays(daysUntilTuesday);


                                        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                        PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("host.pdf", FileMode.Create));
                                        doc.Open();
                                        // dieetwensen toevoegen!!!!!!!!!
                                        string storestring1 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als de gastheer voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00, @ de gerechten welk u moet voorbereiden bestaan uit het volgende: @ Hoofdgerecht. @" + clonedkeuzegerechten[r.Next(2)] + "@" + clonedkeuzegerechten2[r.Next(2)];
                                        string addnewlines1 = storestring1.Replace("@", Environment.NewLine);
                                        Paragraph paragraph = new Paragraph(addnewlines1);
                                        paragraph.IndentationRight = 100;
                                        paragraph.IndentationLeft = 100;
                                        doc.Add(paragraph);
                                        doc.Close();
                                        var pathpdffile = Path.GetFullPath("host.pdf");

                                        Document doc2 = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                                        PdfWriter wri2 = PdfWriter.GetInstance(doc2, new FileStream("bezoeker.pdf", FileMode.Create));
                                        doc2.Open();
                                        // locatie toevoegen!!!!!!!!!
                                        string storestring2 = "Beste Meneer/Mevrouw @ hierbij bent u gekozen als bezoeker voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00 op";
                                        string addnewlines2 = storestring2.Replace("@", Environment.NewLine);
                                        Paragraph paragraph2 = new Paragraph(addnewlines2);
                                        paragraph2.IndentationRight = 100;
                                        paragraph2.IndentationLeft = 100;
                                        doc2.Add(paragraph2);
                                        doc2.Close();
                                        var pathpdffile2 = Path.GetFullPath("bezoeker.pdf");

                                        using (MailMessage mail = new MailMessage())
                                        {
                                            mail.From = new MailAddress("testmailopdracht@gmail.com");
                                            mail.To.Add("hiwadrashad1@gmail.com");
                                            mail.Subject = "Walking Dinner";
                                            mail.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                            mail.IsBodyHtml = true;
                                            System.Net.Mail.Attachment attachment;
                                            attachment = new System.Net.Mail.Attachment(pathpdffile);
                                            mail.Attachments.Add(attachment);

                                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                            {
                                                smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                smtp.EnableSsl = true;
                                                smtp.Send(mail);
                                            }
                                        }


                                        using (MailMessage mail2 = new MailMessage())
                                        {
                                            mail2.From = new MailAddress("testmailopdracht@gmail.com");
                                            mail2.To.Add("hiwadrashad1@gmail.com");
                                            mail2.Subject = "Walking Dinner";
                                            mail2.Body = "Beste Meneer/Mevrouw, in de bijlages bevindt zich de data voor de aankomende Walking Dinner.";
                                            mail2.IsBodyHtml = true;
                                            System.Net.Mail.Attachment attachment;
                                            attachment = new System.Net.Mail.Attachment(pathpdffile2);
                                            mail2.Attachments.Add(attachment);

                                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                            {
                                                smtp.Credentials = new System.Net.NetworkCredential("testmailopdracht@gmail.com", "testmail");
                                                smtp.EnableSsl = true;
                                                smtp.Send(mail2);
                                            }
                                        }

                                    }

                                }
                            }

                            if (!groupswhichareeligable.Any())
                            {
                                personenpergroep.Add(new List<string> { "name2" });
                                maximumlijst.Add(r.Next(2, 9));

                            }
                            else
                            {
                                personenpergroep[groupswhichareeligable.First()].Add("person2");
                            }
                        }
                    }
                }
            }
            static void Main(string[] args)
            {


                // create maximum number for group
             

            }





            
        }    
    }
}

