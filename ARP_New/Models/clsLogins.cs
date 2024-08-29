using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ARP_New.Models
{
    public class clsLogins
    {
        public int id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public clsLogins Login(clsLogins cls)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);

                clsLogins lstobj = new clsLogins();
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_checkLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", cls.Username);
                cmd.Parameters.AddWithValue("@password", cls.Password);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                       
                            lstobj.id = Convert.ToInt32(dt.Rows[0]["Id"] == null || dt.Rows[0]["Id"].ToString().Trim() == "" ? null : dt.Rows[0]["Id"].ToString());
                            lstobj.Firstname = dt.Rows[0]["Firstname"].ToString();
                            lstobj.Lastname = dt.Rows[0]["Lastname"].ToString();
                        
                    }
                    dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                    return lstobj;
                }
                return lstobj;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}