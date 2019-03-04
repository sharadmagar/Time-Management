using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

namespace Bussiness
{
    class DAL
    {
        //static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\sm62725\Desktop\Time_Management\Time_Management\TIMEDB.mdf;Integrated Security=True;Connection Timeout=0");
        //C:\UFT\TIME-MANAGEMENT-MASTER\TIME_MANAGEMENT\TIME_MANAGEMENT\TIMEDB.MDF
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\Tables.accdb;Persist Security Info=True

        static string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\TimeDB.accdb;Persist Security Info=True";
        //string sql  = "SELECT Clients  FROM Tables";
        static OleDbConnection con = new OleDbConnection(connection);
        static OleDbCommand cmd = null;
        static OleDbDataAdapter da = null;
        static OleDbDataReader dr = null;
        //con.ConnectionString = connection;
        //static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\TIMEDB.mdf;Integrated Security=True");
        //static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;Initial Catalog=TIMEDB;Integrated Security=True");
        //static SqlCommand cmd = null;
        //static SqlDataAdapter da = null;
        //static SqlDataReader dr = null;
        static DataTable dt = null;
        public static string GlobalUserID = null;
        public static string GlobalUserRights = null;
        public static string GlobalDateValue = null;
        

        public static bool insert(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new OleDbCommand(s,con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }
        
        public static bool update(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new OleDbCommand(s,con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }
        
        public static bool delete(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new OleDbCommand(s, con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }

        public static string select(string s)
        {
            string b = "";
            con.Open();
            try
            {
                cmd = new OleDbCommand(s, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    b = dr[0].ToString();
                }
            }
            catch (Exception ee)
            {
                b = "";
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }

        public static DataTable show(string s)
        {
            con.Open();
            try
            {
                da = new OleDbDataAdapter(s,con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static string ID(string query,string s)
        {
            string ReceivedId = string.Empty;
            string displayString = string.Empty;

            //query = "SELECT MAX(catid) FROM product_category";

            con.Open();

            cmd = new OleDbCommand(query, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ReceivedId = dr[0].ToString();
            }

            con.Close();

            if (string.IsNullOrEmpty(ReceivedId))
            {
                ReceivedId = s;//"CUT0000"
            }
            int len = ReceivedId.Length;
            string splitNo = ReceivedId.Substring(3, len - 3); // substrine(startingindex,lengthofstring)
            int num = Convert.ToInt32(splitNo);
            num++;
            displayString = ReceivedId.Substring(0, 3) + num.ToString("0000");// substrine(startingindex,lengthofstring)
            return displayString;
        }
    }
}
