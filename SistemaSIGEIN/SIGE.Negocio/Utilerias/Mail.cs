using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Utilerias
{
    public class Mail
    {
        #region variables
        private MailAddressCollection to = new MailAddressCollection();
        private List<Attachment> attachments = new List<Attachment>();
        private SmtpClient client;
        private string server = string.Empty;
        private string senderMail = null;
        private string senderName = null;
        private string password = null;
        private string user = null;
        private int port = 25;
        private Boolean useSSL = true;
        private Boolean htmlMail = true;
        private Boolean useAuthentication = true;

        public Dictionary<string, string> MIMETypesDictionary = new Dictionary<string, string>
  {
    {"ai", "application/postscript"},
    {"aif", "audio/x-aiff"},
    {"aifc", "audio/x-aiff"},
    {"aiff", "audio/x-aiff"},
    {"asc", "text/plain"},
    {"atom", "application/atom+xml"},
    {"au", "audio/basic"},
    {"avi", "video/x-msvideo"},
    {"bcpio", "application/x-bcpio"},
    {"bin", "application/octet-stream"},
    {"bmp", "image/bmp"},
    {"cdf", "application/x-netcdf"},
    {"cgm", "image/cgm"},
    {"class", "application/octet-stream"},
    {"cpio", "application/x-cpio"},
    {"cpt", "application/mac-compactpro"},
    {"csh", "application/x-csh"},
    {"css", "text/css"},
    {"dcr", "application/x-director"},
    {"dif", "video/x-dv"},
    {"dir", "application/x-director"},
    {"djv", "image/vnd.djvu"},
    {"djvu", "image/vnd.djvu"},
    {"dll", "application/octet-stream"},
    {"dmg", "application/octet-stream"},
    {"dms", "application/octet-stream"},
    {"doc", "application/msword"},
    {"docx","application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
    {"dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
    {"docm","application/vnd.ms-word.document.macroEnabled.12"},
    {"dotm","application/vnd.ms-word.template.macroEnabled.12"},
    {"dtd", "application/xml-dtd"},
    {"dv", "video/x-dv"},
    {"dvi", "application/x-dvi"},
    {"dxr", "application/x-director"},
    {"eps", "application/postscript"},
    {"etx", "text/x-setext"},
    {"exe", "application/octet-stream"},
    {"ez", "application/andrew-inset"},
    {"gif", "image/gif"},
    {"gram", "application/srgs"},
    {"grxml", "application/srgs+xml"},
    {"gtar", "application/x-gtar"},
    {"hdf", "application/x-hdf"},
    {"hqx", "application/mac-binhex40"},
    {"htm", "text/html"},
    {"html", "text/html"},
    {"ice", "x-conference/x-cooltalk"},
    {"ico", "image/x-icon"},
    {"ics", "text/calendar"},
    {"ief", "image/ief"},
    {"ifb", "text/calendar"},
    {"iges", "model/iges"},
    {"igs", "model/iges"},
    {"jnlp", "application/x-java-jnlp-file"},
    {"jp2", "image/jp2"},
    {"jpe", "image/jpeg"},
    {"jpeg", "image/jpeg"},
    {"jpg", "image/jpeg"},
    {"js", "application/x-javascript"},
    {"kar", "audio/midi"},
    {"latex", "application/x-latex"},
    {"lha", "application/octet-stream"},
    {"lzh", "application/octet-stream"},
    {"m3u", "audio/x-mpegurl"},
    {"m4a", "audio/mp4a-latm"},
    {"m4b", "audio/mp4a-latm"},
    {"m4p", "audio/mp4a-latm"},
    {"m4u", "video/vnd.mpegurl"},
    {"m4v", "video/x-m4v"},
    {"mac", "image/x-macpaint"},
    {"man", "application/x-troff-man"},
    {"mathml", "application/mathml+xml"},
    {"me", "application/x-troff-me"},
    {"mesh", "model/mesh"},
    {"mid", "audio/midi"},
    {"midi", "audio/midi"},
    {"mif", "application/vnd.mif"},
    {"mov", "video/quicktime"},
    {"movie", "video/x-sgi-movie"},
    {"mp2", "audio/mpeg"},
    {"mp3", "audio/mpeg"},
    {"mp4", "video/mp4"},
    {"mpe", "video/mpeg"},
    {"mpeg", "video/mpeg"},
    {"mpg", "video/mpeg"},
    {"mpga", "audio/mpeg"},
    {"ms", "application/x-troff-ms"},
    {"msh", "model/mesh"},
    {"mxu", "video/vnd.mpegurl"},
    {"nc", "application/x-netcdf"},
    {"oda", "application/oda"},
    {"ogg", "application/ogg"},
    {"pbm", "image/x-portable-bitmap"},
    {"pct", "image/pict"},
    {"pdb", "chemical/x-pdb"},
    {"pdf", "application/pdf"},
    {"pgm", "image/x-portable-graymap"},
    {"pgn", "application/x-chess-pgn"},
    {"pic", "image/pict"},
    {"pict", "image/pict"},
    {"png", "image/png"}, 
    {"pnm", "image/x-portable-anymap"},
    {"pnt", "image/x-macpaint"},
    {"pntg", "image/x-macpaint"},
    {"ppm", "image/x-portable-pixmap"},
    {"ppt", "application/vnd.ms-powerpoint"},
    {"pptx","application/vnd.openxmlformats-officedocument.presentationml.presentation"},
    {"potx","application/vnd.openxmlformats-officedocument.presentationml.template"},
    {"ppsx","application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
    {"ppam","application/vnd.ms-powerpoint.addin.macroEnabled.12"},
    {"pptm","application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
    {"potm","application/vnd.ms-powerpoint.template.macroEnabled.12"},
    {"ppsm","application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
    {"ps", "application/postscript"},
    {"qt", "video/quicktime"},
    {"qti", "image/x-quicktime"},
    {"qtif", "image/x-quicktime"},
    {"ra", "audio/x-pn-realaudio"},
    {"ram", "audio/x-pn-realaudio"},
    {"ras", "image/x-cmu-raster"},
    {"rdf", "application/rdf+xml"},
    {"rgb", "image/x-rgb"},
    {"rm", "application/vnd.rn-realmedia"},
    {"roff", "application/x-troff"},
    {"rtf", "text/rtf"},
    {"rtx", "text/richtext"},
    {"sgm", "text/sgml"},
    {"sgml", "text/sgml"},
    {"sh", "application/x-sh"},
    {"shar", "application/x-shar"},
    {"silo", "model/mesh"},
    {"sit", "application/x-stuffit"},
    {"skd", "application/x-koan"},
    {"skm", "application/x-koan"},
    {"skp", "application/x-koan"},
    {"skt", "application/x-koan"},
    {"smi", "application/smil"},
    {"smil", "application/smil"},
    {"snd", "audio/basic"},
    {"so", "application/octet-stream"},
    {"spl", "application/x-futuresplash"},
    {"src", "application/x-wais-source"},
    {"sv4cpio", "application/x-sv4cpio"},
    {"sv4crc", "application/x-sv4crc"},
    {"svg", "image/svg+xml"},
    {"swf", "application/x-shockwave-flash"},
    {"t", "application/x-troff"},
    {"tar", "application/x-tar"},
    {"tcl", "application/x-tcl"},
    {"tex", "application/x-tex"},
    {"texi", "application/x-texinfo"},
    {"texinfo", "application/x-texinfo"},
    {"tif", "image/tiff"},
    {"tiff", "image/tiff"},
    {"tr", "application/x-troff"},
    {"tsv", "text/tab-separated-values"},
    {"txt", "text/plain"},
    {"ustar", "application/x-ustar"},
    {"vcd", "application/x-cdlink"},
    {"vrml", "model/vrml"},
    {"vxml", "application/voicexml+xml"},
    {"wav", "audio/x-wav"},
    {"wbmp", "image/vnd.wap.wbmp"},
    {"wbmxl", "application/vnd.wap.wbxml"},
    {"wml", "text/vnd.wap.wml"},
    {"wmlc", "application/vnd.wap.wmlc"},
    {"wmls", "text/vnd.wap.wmlscript"},
    {"wmlsc", "application/vnd.wap.wmlscriptc"},
    {"wrl", "model/vrml"},
    {"xbm", "image/x-xbitmap"},
    {"xht", "application/xhtml+xml"},
    {"xhtml", "application/xhtml+xml"},
    {"xls", "application/vnd.ms-excel"},                        
    {"xml", "application/xml"},
    {"xpm", "image/x-xpixmap"},
    {"xsl", "application/xml"},
    {"xlsx","application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
    {"xltx","application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
    {"xlsm","application/vnd.ms-excel.sheet.macroEnabled.12"},
    {"xltm","application/vnd.ms-excel.template.macroEnabled.12"},
    {"xlam","application/vnd.ms-excel.addin.macroEnabled.12"},
    {"xlsb","application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
    {"xslt", "application/xslt+xml"},
    {"xul", "application/vnd.mozilla.xul+xml"},
    {"xwd", "image/x-xwindowdump"},
    {"xyz", "chemical/x-xyz"},
    {"zip", "application/zip"}
  };
        #endregion

        #region propiedades
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        public string SenderMail
        {
            get { return senderMail; }
            set { senderMail = value; }
        }

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public Boolean UseSSL
        {
            get { return useSSL; }
            set { useSSL = value; }
        }

        public Boolean HtmlMail
        {
            get { return htmlMail; }
            set { htmlMail = value; }
        }

        public Boolean UseAuthentication
        {
            get { return useAuthentication; }
            set { useAuthentication = value; }
        }
        #endregion

        #region metodos
        public Mail()
        {

        }

        public Mail(string server, int port, string user, string password, Boolean useSSL)
        {
            this.Server = server;
            this.Port = port;
            this.User = user;
            this.Password = password;
            this.UseSSL = useSSL;
        }

        public Mail(MailConf pMailConf)
        {
            this.Server = pMailConf.Server;
            this.Port = pMailConf.Port;
            this.User = pMailConf.User;
            this.Password = pMailConf.Password;
            this.UseSSL = pMailConf.UseSSL;
            this.SenderMail = pMailConf.SenderMail;
            this.SenderName = pMailConf.SenderName;
            this.useAuthentication = pMailConf.UseAuthentication;
        }

        public void addAttachment(byte[] data, String name, string tipo_archivo)
        {
            MemoryStream ms = new MemoryStream(data);
            Attachment attachment = new Attachment(ms, name, tipo_archivo);

            this.attachments.Add(attachment);
        }

        private void Connect()
        {
            if (this.useAuthentication == true)
            {
                NetworkCredential loginInfo = null;
                client = new SmtpClient(this.Server, this.Port);
                client.Host = ContextoApp.mailConfiguration.Server;
                if (this.User != null && this.user.Length != 0)
                {
                    loginInfo = new NetworkCredential(this.User, this.Password);
                    client.Credentials = loginInfo;
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = this.useSSL;
            }
            else {
                client = new SmtpClient(this.Server, this.Port);
                client.Host = ContextoApp.mailConfiguration.Server;
                client.Port = this.Port;
            }
        }

        public void addToAddress(String mail)
        {
            this.addToAddress(mail, null);
        }

        public void setFromAddress(String mail, String name)
        {
            this.SenderMail = mail;
            this.SenderName = name;
        }

        public void setFromAddress(String mail)
        {
            this.setFromAddress(mail, null);
        }

        public void addToAddress(String mail, String name)
        {
            char[] delimitador = { ';' };
            string text = mail;
            string[] correos = text.Split(delimitador);

            foreach (string s in correos)
            {
                if (name != null && name.Length != 0 && s.Trim() != "")
                {
                    this.to.Add(new MailAddress(s, name));
                }
                else if (s.Trim() != "")
                {
                    this.to.Add(new MailAddress(s));
                }
            }     
            //if (name != null && name.Length != 0)
            //{
            //    this.to.Add(new MailAddress(mail, name));
            //}
            //else
            //{
            //    this.to.Add(new MailAddress(mail));
            //}
        }

        public void clearToAddress()
        {
            this.to.Clear();
        }

        public void clearAttachments()
        {
            this.attachments.Clear();
        }

        public string Send(String subject, String message)
        {
            try
            {
                Connect();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(this.SenderMail, this.SenderName);
                mailMessage.To.Clear();

                foreach (MailAddress address in to)
                    mailMessage.To.Add(address);

#if DEBUG
                //mailMessage.Bcc.Add(new MailAddress("julio.diaz@acrux.mx", "Julio Díaz"));
                // mailMessage.Bcc.Add(new MailAddress("gabriel.vazquez@acrux.mx", "Gabriel Vázquez"));
#else
                mailMessage.Bcc.Add(new MailAddress("sistemas@sapaf.gob.mx", "SAPAF facturación"));
#endif
                mailMessage.Attachments.Clear();
                foreach (Attachment attach in this.attachments)
                {
                    mailMessage.Attachments.Add(attach);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = this.HtmlMail;
                //client.Timeout = 500000;
                client.Send(mailMessage);
                return "0";
            }
            catch (Exception e)
            {
                //traducir posibles errores
                return e.Message.ToString();
            }
        }

        

        public string SendCopia(String subject, String message)
        {
            try
            {
                Connect();
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(this.SenderMail, this.SenderName);
                mailMessage.To.Clear();

                foreach (MailAddress address in to)
                    mailMessage.Bcc.Add(address);

#if DEBUG
                //mailMessage.Bcc.Add(new MailAddress("julio.diaz@acrux.mx", "Julio Díaz"));
                // mailMessage.Bcc.Add(new MailAddress("gabriel.vazquez@acrux.mx", "Gabriel Vázquez"));
#else
                mailMessage.Bcc.Add(new MailAddress("sistemas@sapaf.gob.mx", "SAPAF facturación"));
#endif
                mailMessage.Attachments.Clear();
                foreach (Attachment attach in this.attachments)
                {
                    mailMessage.Attachments.Add(attach);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = this.HtmlMail;
                //client.Timeout = 500000;
                client.Send(mailMessage);
                return "0";
            }
            catch (Exception e)
            {
                //traducir posibles errores
                return e.Message.ToString();
            }
        }

        #endregion
    }

    public class MailConf
    {
        public string SenderName { get; set; }
        public string SenderMail { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool IsHtmlMail { get; set; }
        public bool UseAuthentication { get; set; }
    }
}
