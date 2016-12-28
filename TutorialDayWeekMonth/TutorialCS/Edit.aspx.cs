/* Copyright © 2013 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/

   If you have purchased a DayPilot Pro license, you are allowed to use this 
   code under the conditions of DayPilot Pro License Agreement:

   http://www.daypilot.org/files/LicenseAgreement.pdf

   Otherwise, you are allowed to use it for evaluation purposes only under 
   the conditions of DayPilot Pro Trial License Agreement:
   
   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
   
*/

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class Edit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (!IsPostBack)
        {
            DataRow dr = dbGetEvent(Request.QueryString["id"]);

            if (dr == null)
            {
                throw new Exception("The event was not found");
            }

            TextBoxStart.Text = Convert.ToDateTime(dr["eventstart"]).ToString("M/d/yyyy HH:mm");
            TextBoxEnd.Text = Convert.ToDateTime(dr["eventend"]).ToString("M/d/yyyy HH:mm");
            TextBoxName.Text = (string) dr["name"];

        }
    }
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        DateTime start = Convert.ToDateTime(TextBoxStart.Text);
        DateTime end = Convert.ToDateTime(TextBoxEnd.Text);
        string name = TextBoxName.Text;
        string id = Request.QueryString["id"];

        dbUpdateEvent(id, start, end, name);
        Modal.Close(this, "OK");
    }

    private DataRow dbGetEvent(string id)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [event] WHERE id = @id", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("id", id);
        DataTable dt = new DataTable();
        da.Fill(dt);
        
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        return null;
    }

    private void dbUpdateEvent(string id,DateTime start, DateTime end, string name)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE [event] SET eventstart = @start, eventend = @end, name = @name WHERE id = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            cmd.Parameters.AddWithValue("name", name);
            cmd.ExecuteNonQuery();
        }
    }

    private void dbDeleteEvent(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM [event] WHERE id = @id", con);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
    protected void LinkButtonDelete_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        dbDeleteEvent(id);
        Modal.Close(this, "OK");
    }
}
