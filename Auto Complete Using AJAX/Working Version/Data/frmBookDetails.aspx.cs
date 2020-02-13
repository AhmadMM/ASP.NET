using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BCCBookStore
{
    public partial class frmBookDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (((Convert.ToString(Session["BookName"]) == String.Empty)
                                && ((Convert.ToString(Session["BookPublisher"]) == String.Empty)
                                && ((Convert.ToString(Session["BookAuthor"]) == String.Empty)
                                && ((Convert.ToString(Session["GradeName"]) == String.Empty)
                                && ((Convert.ToString(Session["CategroyName"]) == String.Empty)
                                && ((Convert.ToString(Session["SchoolName"]) == String.Empty))))))))
                    {
                        Global.LoadTableRepeater(dsFillBooks, "ShopGETFeaturedBooks");
                    }
                    else
                    {
                        Global.AdvancedSearch(dsFillBooks, Convert.ToString(Session["BookName"]), Convert.ToString(Session["BookPublisher"]), Convert.ToString(Session["BookAuthor"]), Convert.ToString(Session["GradeName"]), Convert.ToString(Session["CategroyName"]), Convert.ToString(Session["SchoolName"]));

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}