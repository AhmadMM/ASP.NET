using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using ASPSnippets.FaceBookAPI;
using System.Web.Script.Serialization;

namespace BCCBookStore
{
    public partial class frmCheckout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                FaceBookConnect.API_Key = "168177840465292";
                FaceBookConnect.API_Secret = "7cef6ad91cc64d1863727e1988d9bc0a";
                if (!IsPostBack)
                {
                    if (Request.QueryString["error"] == "access_denied")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                        return;
                    }

                    string code = Request.QueryString["code"];
                    if (!string.IsNullOrEmpty(code))
                    {
                        string data = FaceBookConnect.Fetch(code, "me?fields=id,name,email");
                        FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                        faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
                        string UserID = "";
                        string username = "";
                        string Name = "";
                        string email = "";
                        string PPURL = "";
                        UserID = faceBookUser.Id;
                        username = faceBookUser.UserName;
                        Name = faceBookUser.Name;
                        email = faceBookUser.Email;
                        PPURL = faceBookUser.PictureUrl;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
        }

        public class FaceBookUser
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string UserName { get; set; }
            public string PictureUrl { get; set; }
            public string Email { get; set; }
        }
    }

}
