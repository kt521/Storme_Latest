using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ARP_New.Models
{
    public class clsPlantDetailsdata
    {
        public string strName { get; set; }
        public string dttime { get; set; }
        public string strDateandTime { get; set; }
        public string strTagname { get; set; }
        public string strVal { get; set; }
        public string decLevel { get; set; }
        public string decFlowRate { get; set; }
        public string decCurrentDayQty { get; set; }
        public string decPreviousDayQty { get; set; }
        public string decTillTodayQty { get; set; }
        public string decTotal { get; set; }
        public string Zonename { get; set; }
        public List<clsPlantDetailsdata> GetPlantList(clsPlantDetailsdata cls)
        {

            List<clsPlantDetailsdata> ContentList = new List<clsPlantDetailsdata>();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getStromePlantDetailWithPiovotForWeb25062024", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strDate", cls.strDateandTime);
                cmd.Parameters.AddWithValue("@strplant", cls.strName);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            clsPlantDetailsdata user = new clsPlantDetailsdata();
                            user.strDateandTime = row[0].ToString();
                            user.decTillTodayQty = row[1].ToString() == "" ? "0.00" : (Convert.ToDecimal(row[1]) / 1000).ToString("0.00");
                            user.decPreviousDayQty = row[2].ToString();
                            user.decCurrentDayQty = row[3].ToString() == "" ? "0.00" : (Convert.ToDecimal(row[3]) / 1000).ToString("0.00"); ;
                            user.decLevel = row[4].ToString();
                            user.decFlowRate = row[5].ToString();
                            //user.stroutTotalMDR = row[5].ToString();
                            //user.stroutPH = row[6].ToString();
                            //user.stroutTss = row[7].ToString();
                            //user.stroutcl = row[8].ToString();
                            //user.strOutBod = row[9].ToString();
                            //user.strOutCod = row[10].ToString();
                            ContentList.Add(user);
                        }
                          ;
                    }
                    dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                    return ContentList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ContentList;
        }


        public List<clsPlantDetailsdata> GetPlantListMonth(clsPlantDetailsdata cls)
        {

            List<clsPlantDetailsdata> ContentList = new List<clsPlantDetailsdata>();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getStromeMonthlyPlantWisePumpDetailWithPiovot2662024", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strDate", cls.strDateandTime);
                cmd.Parameters.AddWithValue("@strplant", cls.strName);
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            clsPlantDetailsdata user = new clsPlantDetailsdata();
                            user.strDateandTime = row[0].ToString();
                            user.decTotal = row[1].ToString() == "" ? "0.00" : (Convert.ToDecimal(row[1]) / 1000).ToString("0.00");
                            user.decTillTodayQty = row[2].ToString() == "" ? "0.00" : (Convert.ToDecimal(row[2]) / 1000).ToString("0.00");
                            //user.stroutTotalMDR = row[5].ToString();
                            //user.stroutPH = row[6].ToString();
                            //user.stroutTss = row[7].ToString();
                            //user.stroutcl = row[8].ToString();
                            //user.strOutBod = row[9].ToString();
                            //user.strOutCod = row[10].ToString();
                            ContentList.Add(user);
                        }
                          ;
                    }
                    dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                    return ContentList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ContentList;
        }

        public class clsPumpDetailsdata
        {
            public string strName { get; set; }
            public string dttime { get; set; }
            public string strDateandTime { get; set; }
            public string strTagname { get; set; }
            public string pump1 { get; set; }
            public string pump2 { get; set; }
            public string pump3 { get; set; }
            public string pump4 { get; set; }
            public string pump5 { get; set; }



            public List<clsPumpDetailsdata> GetPumpDetail(clsPlantDetailsdata cls)
            {

                List<clsPumpDetailsdata> ContentList = new List<clsPumpDetailsdata>();
                try
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_getStromePlantWisePumpDetailWithPiovot", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strDate", cls.dttime);
                    cmd.Parameters.AddWithValue("@strplant", cls.strName);
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                clsPumpDetailsdata user = new clsPumpDetailsdata();
                                user.strDateandTime = row[0].ToString();
                                for (var i = 0; i < (dt.Columns.Count - 1); i++)
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            user.pump1 = row[i + 1].ToString();
                                            break;
                                        case 1:
                                            user.pump2 = row[i + 1].ToString();
                                            break;
                                        case 2:
                                            user.pump3 = row[i + 1].ToString();
                                            break;
                                        case 3:
                                            user.pump4 = row[i + 1].ToString();
                                            break;
                                        case 4:
                                            user.pump5 = row[i + 1].ToString();
                                            break;
                                    }

                                }

                                ContentList.Add(user);
                            }
                              ;
                        }
                        dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                        return ContentList;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ContentList;
            }
        }

        public class clsContactDetailsDetailsdata
        {
            public string strName { get; set; }
            public string dttime { get; set; }
            public string strDateandTime { get; set; }
            public string strTagname { get; set; }
            public string OperatorName { get; set; }
            public string TorrentServiceNo1 { get; set; }
            public string TorrentServiceNo2 { get; set; }
            public string TorrentContactNo { get; set; }
            public string OperatorNo { get; set; }
            public string SupervisorNo { get; set; }
            public string TSNo { get; set; }
            public string AENo { get; set; }



            public List<clsContactDetailsDetailsdata> GetContactDetail(clsPlantDetailsdata cls)
            {

                List<clsContactDetailsDetailsdata> ContentList = new List<clsContactDetailsDetailsdata>();
                try
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_getContactDetailsBySiteName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@strSiteName", cls.strName);
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                clsContactDetailsDetailsdata user = new clsContactDetailsDetailsdata();
                                user.strDateandTime = row[0].ToString();

                                user.OperatorName = row["OperatorName"].ToString();
                                user.TorrentServiceNo1 = row["TorrentServiceNo1"].ToString();
                                user.TorrentServiceNo2 = row["TorrentServiceNo2"].ToString();
                                user.TorrentContactNo = row["TorrentContactNo"].ToString();
                                user.OperatorNo = row["OperatorNo1"].ToString();
                                user.SupervisorNo = row["SupervisorNo"].ToString();
                                user.OperatorName = row["TSNo"].ToString();
                                user.OperatorName = row["OperatorName"].ToString();
                                ContentList.Add(user);
                            }


                        }
                    }
                    dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                    return ContentList;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public class PlantDetailsdataOutput
        {
            public List<clsPlantDetailsdata> data { get; set; }
            public List<clsPumpDetailsdata> pumpData { get; set; }
            public List<clsContactDetailsDetailsdata> contactData { get; set; }
            public string status { get; set; }
            public string msg { get; set; }
            public string err { get; set; }
        }
       


    }
    public class clsmenu
    {
     
    }
    public class clsMenuList 
    {
        public string Mainmenu { get; set; }
        public string manu { get; set; }
        public int intZoneId { get; set; }


        public List<clsMenuList> GeMenuList(clsMenuList cls)
        {

            List<clsMenuList> ContentList = new List<clsMenuList>();
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AMCConn"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_getZoneDataforWeb", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                conn.Close();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            clsMenuList user = new clsMenuList();
                            user.Mainmenu = row["Mainmenu"].ToString();
                            user.manu = row["manu"].ToString();
                            user.intZoneId = Convert.ToInt32(row["intZoneId"]);
                            ContentList.Add(user);
                        }
                          ;
                    }
                    dt.Dispose(); adp.Dispose(); cmd.Dispose(); conn.Dispose();
                    return ContentList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ContentList;
        }
    }
}