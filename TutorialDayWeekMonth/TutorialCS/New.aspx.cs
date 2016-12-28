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
using System.Web.UI;

public partial class New : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxStart.Text = Convert.ToDateTime(Request.QueryString["start"]).ToString("M/d/yyyy HH:mm");
            TextBoxEnd.Text = Convert.ToDateTime(Request.QueryString["end"]).ToString("M/d/yyyy HH:mm");
        }
    }
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        DateTime start = Convert.ToDateTime(TextBoxStart.Text);
        DateTime end = Convert.ToDateTime(TextBoxEnd.Text);

        dbInsertEvent(start, end, TextBoxName.Text);
        Modal.Close(this, "OK");
    }

    private void dbInsertEvent(DateTime start, DateTime end, string name)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO [event] (eventstart, eventend, name) VALUES(@start, @end, @name)", con);
            //cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("start", start);
            cmd.Parameters.AddWithValue("end", end);
            cmd.Parameters.AddWithValue("name", name);
            cmd.ExecuteNonQuery();
        }
    }

    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Modal.Close(this);
    }
}
