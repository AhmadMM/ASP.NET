using System;
using System.Configuration;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace BCCBookStore
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string imgID;
                if (context.Request.QueryString["FileName"] != null)
                    imgID = Convert.ToString(context.Request.QueryString["FileName"]);
                else
                    throw new ArgumentException("No parameter specified");

                context.Response.ContentType = "image/jpeg";
                Stream strm = ShowEmpImage(imgID);
                byte[] buffer = new byte[4096];
                int byteSeq = strm.Read(buffer, 0, 4096);

                while (byteSeq > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, byteSeq);
                    byteSeq = strm.Read(buffer, 0, 4096);
                }
                //context.Response.BinaryWrite(buffer);
            }
            catch (Exception ex) { }
            }   

        public Stream ShowEmpImage(string imgID)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("GetIMG", Global.con);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FileName", imgID);
                Global.Connect(); 
                object bytes = sqlCommand.ExecuteScalar();
                try
                {
                    return new MemoryStream((byte[])bytes);
                }
                catch
                {
                    return null;
                }

            }
            catch(Exception ex) {

                return null;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}