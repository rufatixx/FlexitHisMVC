using System;
using System.Net.NetworkInformation;
using FlexitHisMVC.Models;
using MySql.Data.MySqlClient;

namespace FlexitHisMVC.Data
{
    public class UserRepo
    {
        private readonly string ConnectionString;

        public UserRepo(string conString)
        {
            ConnectionString = conString;
        }
        public List<Personal> GetPersonalList()
        {
            List<Personal> personalList = new List<Personal>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();
         
                using (MySqlCommand com = new MySqlCommand("SELECT *,(select name from speciality where a.specialityID = id )as specialityName FROM users a ", connection))
                {

                    MySqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {

                            Personal personal = new Personal();
                            personal.ID = Convert.ToInt32(reader["id"]);
                            personal.depID = reader["departmentID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["departmentID"]);  
                            personal.name = reader["name"] == DBNull.Value ? "" : reader["name"].ToString();
                            personal.surname = reader["surname"] == DBNull.Value ? "" : reader["surname"].ToString();
                            personal.father = reader["father"] == DBNull.Value ? "" : reader["father"].ToString();
                            personal.mobile = reader["mobile"] == DBNull.Value ? "" : reader["mobile"].ToString(); 
                            personal.username = reader["username"] == DBNull.Value ? "" : reader["username"].ToString(); 
                            personal.email = reader["email"] == DBNull.Value ? "" : reader["email"].ToString();
                            personal.passportSerialNum = reader["passportSerialNum"] == DBNull.Value ? "" : reader["passportSerialNum"].ToString();
                            personal.fin = reader["fin"] == DBNull.Value ? "" : reader["fin"].ToString();

                            personal.bDate = reader["bDate"] == DBNull.Value ? DateTime.Now.Date.ToString("yyyy-MM-dd") : Convert.ToDateTime(reader["bDate"]).Date.ToString("yyyy-MM-dd"); 
                            personal.speciality = reader["specialityName"] == DBNull.Value ? "" : reader["specialityName"].ToString();
                            personal.isActive = reader["isActive"] == DBNull.Value ? false : Convert.ToBoolean(reader["isActive"]);
                            personal.isUser = reader["isUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["isUser"]);
                            personal.isDr = reader["isDr"] == DBNull.Value ? false : Convert.ToBoolean(reader["isDr"]);
                            personal.isAdmin = reader["isAdmin"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAdmin"]);

                            personalList.Add(personal);


                        }



                    }

                }
                connection.Close();
            }
            return personalList;
        }

        public List<Personal> GetRefererList()
        {
            List<Personal> refererList = new List<Personal>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();
                using (MySqlCommand com = new MySqlCommand("SELECT * FROM users where referral = 1;", connection))
                {

                    MySqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {

                            Personal referralPersonal = new Personal();
                            referralPersonal.ID = Convert.ToInt32(reader["id"]);
                            referralPersonal.name = reader["name"].ToString();
                            referralPersonal.surname = reader["surname"].ToString();
                            referralPersonal.father = reader["father"].ToString();
                            refererList.Add(referralPersonal);


                        }



                    }

                }

                connection.Close();
            }
            return refererList;
        }

        public Personal GetUser(string username, string pass)
        {
            Personal personal = new Personal();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {

                connection.Open();
                using (MySqlCommand com = new MySqlCommand("select * from users where pwd=SHA2(@pass,256) and username = @username and isActive=1 and isUser=1", connection))
                {

                    com.Parameters.AddWithValue("@pass", pass);
                    com.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {


                            while (reader.Read())
                            {
                                personal.ID = Convert.ToInt32(reader["id"]);
                                personal.isActive = Convert.ToBoolean(reader["isActive"]);
                                personal.isUser = Convert.ToBoolean(reader["isUser"]);
                                personal.name = reader["name"].ToString();
                                personal.surname = reader["surname"].ToString();
                                personal.isAdmin = reader["isAdmin"] == DBNull.Value ? false : Convert.ToBoolean(reader["isAdmin"]);
                                personal.isUser = reader["isUser"] == DBNull.Value ? false : Convert.ToBoolean(reader["isUser"]);
                                personal.isDr = reader["isDr"] == DBNull.Value ? false : Convert.ToBoolean(reader["isDr"]);
                            }

                            connection.Close();





                        }

                    }

                }
                connection.Close();
            }
            return personal;
        }
        public int InsertPersonal(string name, string surname, string father,int specialityID, string passportSerialNum,string fin, string phone, string email, string bDate, string username, string pwd, int isUser,int isDr,int isAdmin)
        {
            int lastID = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {


                    var sql = "";

                    connection.Open();
                    if (isUser == 1 && !string.IsNullOrEmpty(username))
                    {
                        sql = @"Insert INTO users (name,surname,father,specialityID, mobile,email,bdate,username,pwd,isUser,isDr,isAdmin)
SELECT @name,@surname,@father,@specialityID,@mobile,@email,@bDate,@username,SHA2(@pwd,256),@isUser,@isDr,@isAdmin FROM DUAL
WHERE NOT EXISTS 
  (SELECT * FROM users WHERE name=@name and surname=@surname and father= @father or username = @username)";

                    }
                    else {
                        sql = @"Insert INTO users (name,surname,father,specialityID, mobile,email,bdate,isUser,isDr,isAdmin)
SELECT @name,@surname,@father,@specialityID,@mobile,@email,@bDate,@isUser,@isDr,@isAdmin FROM DUAL
WHERE NOT EXISTS 
  (SELECT * FROM users WHERE name=@name and surname=@surname and father= @father)";
                      
                    }
                   
                    using (MySqlCommand com = new MySqlCommand(sql, connection))
                    {
                        com.Parameters.AddWithValue("@name", name);
                        com.Parameters.AddWithValue("@surname", surname);
                        com.Parameters.AddWithValue("@father", father);
                        com.Parameters.AddWithValue("@specialityID", specialityID);
                        com.Parameters.AddWithValue("@passportSerialNum", passportSerialNum);
                        com.Parameters.AddWithValue("@fin", fin);
                        com.Parameters.AddWithValue("@mobile", phone);
                        com.Parameters.AddWithValue("@email", email);
                        com.Parameters.AddWithValue("@bDate", bDate);
                        com.Parameters.AddWithValue("@username", username);
                        com.Parameters.AddWithValue("@pwd", pwd);
                        com.Parameters.AddWithValue("@isUser", isUser);
                        com.Parameters.AddWithValue("@isDr", isDr);
                        com.Parameters.AddWithValue("@admin", isAdmin);

                        lastID = com.ExecuteNonQuery();


                    }
                    connection.Close();



                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);


            }
            return lastID;
        }

        public int UpdateUser(int userID,string name, string surname, string father, int specialityID, string passportSerialNum, string fin, string mobile, string email, string bDate, string username, int isUser, int isDr, int isActive,int isAdmin)
        {
            int updated = 0;
            List<Personal> personalList = new List<Personal>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {




                    connection.Open();
                    using (MySqlCommand com = new MySqlCommand("update users set name = @name, surname= @surname, father = @father, email = @email, bDate = @bDate,passportSerialNum = @passportSerialNum,fin=@fin, mobile=@mobile, specialityID=@specialityID, isDr=@isDr,username= @username,isActive=@isActive,isUser=@isUser,isAdmin=@isAdmin where id = @userID", connection))
                    {
                        com.Parameters.AddWithValue("@userID",userID);
                        com.Parameters.AddWithValue("@name", name ?? "");
                        com.Parameters.AddWithValue("@surname", surname ?? "");
                        com.Parameters.AddWithValue("@father", father ?? "");
                        com.Parameters.AddWithValue("@mobile", mobile?? "");
                        com.Parameters.AddWithValue("@email", email ?? "");
                        com.Parameters.AddWithValue("@bDate", bDate);
                        com.Parameters.AddWithValue("@username", username?? "");
                        com.Parameters.AddWithValue("@passportSerialNum", passportSerialNum ?? "");
                        com.Parameters.AddWithValue("@fin", fin ?? "");
                        com.Parameters.AddWithValue("@isUser", isUser);
                        com.Parameters.AddWithValue("@isActive", isActive);
                        com.Parameters.AddWithValue("@specialityID", specialityID);
                        com.Parameters.AddWithValue("@isDr", isDr); 
                        com.Parameters.AddWithValue("@isAdmin", isAdmin); 

                      updated = com.ExecuteNonQuery();

                    }
                    connection.Close();



                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);


            }
            return updated;
        }
        public int UpdateUserPwd(int userID, string pwd)
        {
            int updated = 0;
            List<Personal> personalList = new List<Personal>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {




                    connection.Open();
                    using (MySqlCommand com = new MySqlCommand("update users set pwd = SHA2(@pwd,256) where id = @userID", connection))
                    {
                        com.Parameters.AddWithValue("@userID", userID);
                        com.Parameters.AddWithValue("@pwd", pwd ?? "");
                      

                        updated = com.ExecuteNonQuery();

                    }
                    connection.Close();



                }


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);


            }
            return updated;
        }

    }
}

