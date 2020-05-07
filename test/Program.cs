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




        static void Main(string[] args)
        {
            Random r = new Random();
            List<int> maximumlijst = new List<int>();
            List<List<string>> personenpergroep = new List<List<string>>();
            List<int> cloneablelist = new List<int>();
            List<string> Cloneablestringlist = new List<string>();
            SortedDictionary<string, int> hostsperpersoon = new SortedDictionary<string, int>();
            List<string> hostpergroep = new List<string>();
            SortedDictionary<string, int> cloneablestringkeydictionary = new SortedDictionary<string, int>();
            List<string> keuzegerechten = new List<string> {"Aperitief met amuse","Nagerecht" };
            List<string> keuzegerechten2 = new List<string> { "Koud voorgerecht", "Warm voorgerecht" };
             // create maximum number for group
         

            if (!personenpergroep.Any())
            {
                personenpergroep.Add(new List<string> { "name1" });
                maximumlijst.Add(r.Next(2, 9));
            }
            else
            {


                var temporarylist = personenpergroep.DeepClone();
                var storenamestocheck = Cloneablestringlist.DeepClone();
                var groupswhichareeligable = cloneablelist.DeepClone();
                var hostnumberpersonsinsidegroup = Cloneablestringlist.DeepClone();
                var temporaryhostpergroup = hostpergroep.DeepClone();

                temporaryhostpergroup.Clear();
                int i = 0;

                //get index of lists with person + remove person in clone
                foreach (var index2 in temporarylist)
                {
                    if (index2.Contains("person2"))
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
                foreach (int index in maximumlijst)
                {
                    if (maximumlijst[index] > personenpergroep[index].Count)
                    {
                        hostpergroep.Add("null");
                        //check if persons have met eachother already

                        if (personenpergroep[index].Intersect(storenamestocheck).Any())
                        {
                      
                        }
                        else
                        {
                            groupswhichareeligable.Add(index);
                        }

                    }
            
                    else
                    {
                        if (!temporaryhostpergroup.Contains("null"))
                        {

                        }
                        else
                        {
                            foreach (var index5 in personenpergroep[index])
                            {
                                hostnumberpersonsinsidegroup.Add(index5);
                            }

                            var temporarydictionary = cloneablestringkeydictionary.DeepClone();

                            foreach (var index6 in hostnumberpersonsinsidegroup)
                            {
                                if (!hostsperpersoon.ContainsKey(index6))
                                {
                                    hostsperpersoon.Add(index6, 0);
                                    temporarydictionary.Add(index6, 0);
                                }
                                else
                                {
                                    temporarydictionary.Add(index6, hostsperpersoon[index6]);
                                }
                            
                            }

                            var indexhost = temporarydictionary.Aggregate((l, a) => l.Value < a.Value ? l : a).Key;

                            var clonedkeuzegerechten = keuzegerechten.DeepClone();
                            var clonedkeuzegerechten2 = keuzegerechten2.DeepClone();

                            DateTime today = DateTime.Today;
                            int daysUntilTuesday = (((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7) + 1;
                            DateTime nextTuesday = today.AddDays(daysUntilTuesday);
                            

                            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("host.pdf", FileMode.Create));
                            doc.Open();
                            // dieetwensen toevoegen!!!!!!!!!
                            Paragraph paragraph = new Paragraph("Beste Meneer/Mevrouw /n hierbij bent u gekozen als de gastheer voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00, /n de gerechten welk u moet voorbereiden bestaan uit het volgende: /n Hoofdgerecht. /n"+ clonedkeuzegerechten[r.Next(2)] + clonedkeuzegerechten2[r.Next(2)]);
                            doc.Add(paragraph);
                            doc.Close();

                            Document doc2 = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
                            PdfWriter wri2 = PdfWriter.GetInstance(doc2, new FileStream("bezoeker.pdf", FileMode.Create));
                            doc2.Open();
                            // locatie toevoegen!!!!!!!!!
                            Paragraph paragraph2 = new Paragraph("Beste Meneer/Mevrouw /n hierbij bent u gekozen als bezoeker voor de walking dinner op " + nextTuesday.ToString("dd/MM/yyyy") + " om 18:00 op" );
                            doc2.Add(paragraph);
                            doc2.Close();
                            var pathpdffile = Path.GetFullPath("bezoeker.pdf");

                            using (MailMessage mail = new MailMessage())
                            {
                            mail.From = new MailAddress("testmailopdracht@gmail.com");
                            mail.To.Add("emailadress");
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

                       


                     

                            if (hostsperpersoon.ContainsKey(temporaryhostpergroup.Last()))
                            {
                                var storenumber = hostsperpersoon[temporaryhostpergroup.Last()];
                                storenumber = storenumber + 1;
                                hostsperpersoon[temporaryhostpergroup.Last()] = storenumber;
                            }
                            else
                            {
                                hostsperpersoon.Add(temporaryhostpergroup.Last(), 1);
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

