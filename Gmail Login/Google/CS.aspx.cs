using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;

public partial class CS2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GoogleConnect.ClientId = "<Google Client ID>";
        GoogleConnect.ClientSecret = "<Google Client Secret>";

        GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];

        if (!string.IsNullOrEmpty(Request.QueryString["code"]))
        {
            string code = Request.QueryString["code"];
            string json = GoogleConnect.Fetch("me", code);
            GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);
            lblId.Text = profile.Id;
            lblName.Text = profile.DisplayName;
            lblEmail.Text = profile.Emails.Find(email => email.Type == "account").Value;
            lblGender.Text = profile.Gender;
            lblType.Text = profile.ObjectType;
            ProfileImage.ImageUrl = profile.Image.Url;
            pnlProfile.Visible = true;
            btnLogin.Enabled = false;
        }
        if (Request.QueryString["error"] == "access_denied")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
        }
    }

    protected void Login(object sender, EventArgs e)
    {
        GoogleConnect.Authorize("profile", "email");
    }
    protected void Clear(object sender, EventArgs e)
    {
        GoogleConnect.Clear(Request.QueryString["code"]);
    }

    public class GoogleProfile
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public Image Image { get; set; }
        public List<Email> Emails { get; set; }
        public string Gender { get; set; }
        public string ObjectType { get; set; }
    }

    public class Email
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
    }
}